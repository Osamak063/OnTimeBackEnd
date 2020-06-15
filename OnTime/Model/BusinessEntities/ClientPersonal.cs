using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnTime.Model.BusinessEntities
{
    public class ClientPersonal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public int ShipmentsPerWeek { get; set; }
        public string CnicNumber { get; set; }
        public string Address { get; set; }
        public string AccountNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
