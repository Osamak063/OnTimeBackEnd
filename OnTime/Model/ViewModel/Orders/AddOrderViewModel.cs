using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnTime.Model.ViewModel.Orders
{
    public class AddOrderViewModel
    {
        [Required]
        public string ConsigneeName { get; set; }
        [Required]
        public string ConsigneeContactNumber { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string OrderId { get; set; }
        [Required]
        public int NumberOfProducts { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
        public string SpecialInstructions { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int CashOnDelivery { get; set; }
        [Required]
        public int ClientPersonalId { get; set; }
        public int? StatusId { get; set; }
    }
}
