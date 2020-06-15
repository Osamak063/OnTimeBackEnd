using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTime.Model.BusinessEntities
{
    public class Order
    {
        public int Id { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeContactNumber { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public string Address { get; set; }
        public string OrderId { get; set; }
        public int NumberOfProducts { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public DateTime BookingDate { get; set; }
        public string SpecialInstructions { get; set; }
        public int Weight { get; set; }
        public int CashOnDelivery { get; set; }
        public int ClientPersonalId { get; set; }
        public virtual ClientPersonal ClientPersonal { get; set; }
        public string TrackingId { get; set; }
        public int OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StatusChangeDate { get; set; }
    }
}
