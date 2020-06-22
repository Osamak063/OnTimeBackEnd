using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnTime.Model;
using OnTime.Model.BusinessEntities;
using OnTime.Model.ViewModel.Orders;
using OnTime.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnTime.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public OrdersController(AppDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders = await context.Orders.ToListAsync();
            return orders;
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("GetAllOrdersByClient/{Id}")]
        public async Task<IEnumerable<Order>> GetOrdersByClientId(int Id)
        {
            var orders = await context.Orders.Where(o => o.ClientPersonalId == Id).ToListAsync();
            return orders;
        }

        [HttpGet]
        [Route("GetOrderByTrackingId/{Id}")]
        public async Task<Order> GetOrderByTrackingId(int Id)
        {
            var order = await context.Orders.Where(o => o.TrackingId == Id).FirstOrDefaultAsync();
            return order;
        }

        // POST api/<controller>
        [HttpPost]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody]AddOrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
                throw new Exception("Bad Request");
            var order = mapper.Map<AddOrderViewModel, Order>(orderViewModel);
            try
            {
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();
                order.TrackingId = ApplicationConstants.TRACKING_ID_INITIAL + order.Id;
                context.Orders.Update(order);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred when add the order: { ex.Message }");
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
