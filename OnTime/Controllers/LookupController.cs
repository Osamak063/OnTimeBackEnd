using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnTime.Model;
using OnTime.Model.BusinessEntities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnTime.Controllers
{
    
    [Route("api/[controller]")]
    public class LookupController : Controller
    {

        private readonly AppDbContext context;

        public LookupController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        [Route("GetProductTypeList")]
        public async Task<IEnumerable<ProductType>> GetProductTypes()
        {
            var ProductTypeList = await context.ProductTypes.ToListAsync();
            return ProductTypeList;
        }

        // GET: api/<controller>
        [HttpGet]
        [Route("GetCitiesList")]
        public async Task<IEnumerable<City>> GetCities()
        {
            var CitiesList = await context.Cities.ToListAsync();
            return CitiesList;
        }

    }
}
