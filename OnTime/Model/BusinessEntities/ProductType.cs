using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTime.Model.BusinessEntities
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ClientPersonal> ClientPersonals { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
