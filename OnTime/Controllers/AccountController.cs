using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnTime.Model;
using OnTime.Model.BusinessEntities;
using OnTime.Model.Security;
using OnTime.Model.ViewModel;
using OnTime.Utilities;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnTime.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly JWTSettings jwtSettings;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext context, IMapper mapper,
            IOptions<JWTSettings> jwtSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.mapper = mapper;
            this.jwtSettings = jwtSettings.Value;
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("GetAllRegistrationRequests")]
        //[Authorize(Roles = "DataEntryOperator")]
        public async Task<IActionResult> Get()
        {
            //var clients = await context.ClientPersonal.Where(c => c.UserId == null).ToListAsync();
            var clients = await (from client in context.ClientPersonal
                                 join product in context.ProductTypes on client.ProductTypeId equals product.Id
                                 where client.UserId == null
                                 select new
                                 {
                                     client.Id,
                                     client.Name,
                                     client.CompanyName,
                                     client.ContactNumber,
                                     client.EmailAddress,
                                     client.ShipmentsPerWeek,
                                     client.CnicNumber,
                                     client.Address,
                                     client.AccountNumber,
                                     client.WebsiteUrl,
                                     ProductType = product.Name
                                 }
                                 ).ToListAsync();
            return Ok(clients);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                throw new Exception("Bad Request");
            var ClientPersonal = mapper.Map<RegisterViewModel, ClientPersonal>(model);
            //var ClientPersonal = new ClientPersonal
            //{
            //    Name = model.Name,
            //    CompanyName = model.CompanyName,
            //    ContactNumber = model.ContactNumber,
            //    AccountNumber = model.AccountNumber,
            //    EmailAddress = model.EmailAddress,
            //    ShipmentsPerWeek = model.ShipmentsPerWeek,
            //    CnicNumber = model.CnicNumber,
            //    Address = model.Address,
            //    WebsiteUrl = model.WebsiteUrl,
            //    ProductTypeId = model.ProductTypeId
            //};
            try
            {
                await context.ClientPersonal.AddAsync(ClientPersonal);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                // log error
                return BadRequest($"An error occurred when saving the user: { ex.Message }");
            }
        }

        // Post api/<controller>/5
        [HttpPut]
        [Route("ApproveRegistration/{id}")]
        //[Authorize(Roles = "DataEntryOperator")]
        public async Task<IActionResult> ApproveRegistration(int id)
        {
            var ClientPersonal = await context.ClientPersonal.FirstOrDefaultAsync(x => x.Id == id);
            if (ClientPersonal == null)
                return BadRequest("Invalid Client Id");

            var user = new IdentityUser
            {
                UserName = ApplicationConstants.USER_NAME_PREFIX + id,
                Email = ClientPersonal.EmailAddress,
                PhoneNumber = ClientPersonal.ContactNumber,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true
            };
            var password = ClientPersonal.CompanyName.ToLower() + "-" +
                ApplicationConstants.PasswordString(ApplicationConstants.PASSWORD_STRING_LENGTH) + ClientPersonal.ShipmentsPerWeek;
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                // Email client for successful registration with user id and password
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Osama", "osamak063@gmail.com"));
                message.To.Add(new MailboxAddress("Test", "osama.ansari@systemsltd.com"));
                message.Subject = "Test email";
                message.Body = new TextPart("plain")
                {
                    Text = "Username: hello , pwd : qwer@1234"
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com",587,false);
                    client.Authenticate("osamak063@gmail.com", "hotmail222");
                    client.Send(message);
                    client.Disconnect(true);
                }
                await userManager.AddToRoleAsync(user, "Client");
                ClientPersonal.UserId = user.Id;
                context.ClientPersonal.Update(ClientPersonal);
                await context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                StringBuilder errors = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errors.Append(error.Description);
                }
                throw new Exception(errors.ToString());
            }
        }

        // Post api/<controller>/5
        [HttpPut]
        [Route("RejectRegistration/{id}")]
        [Authorize(Roles = "DataEntryOperator")]
        public async Task<IActionResult> RejectRegistration(int id)
        {
            var ClientPersonal = await context.ClientPersonal.FirstOrDefaultAsync(x => x.Id == id);
            if (ClientPersonal == null)
                return BadRequest("Invalid Client Id");
            context.ClientPersonal.Remove(ClientPersonal);
            await context.SaveChangesAsync();
            return Ok();
        }

        // Post api/<controller>/5
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
                throw new Exception("Bad Request");
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (result.Succeeded)
            {   // sign in token
                var user = await userManager.FindByNameAsync(model.UserName);
                var clientPersonalId = await context.ClientPersonal.Where(c => c.UserId == user.Id).Select(c => c.Id).FirstOrDefaultAsync();
                var roles = await userManager.GetRolesAsync(user);
                user.PasswordHash = null;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role,roles.Count > 0 ? roles[0] : null)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                ExpandoObject userWithToken = new ExpandoObject();
                userWithToken.TryAdd("token", tokenHandler.WriteToken(token));
                userWithToken.TryAdd("expires_at", tokenDescriptor.Expires);
                userWithToken.TryAdd("user", new
                {
                    user,
                    clientPersonalId,
                    role = roles.Count > 0 ? roles[0] : null
                });
                return Ok(userWithToken);
            }
            else
                return Unauthorized();

        }

        //// Post api/<controller>/5
        //[HttpPost]
        //[Route("Logout")]
        //public async IActionResult Logout()
        //{
        //    await signInManager.SignOutAsync();
        //    return Ok();

        //}
    }
}
