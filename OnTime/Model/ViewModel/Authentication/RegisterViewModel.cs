using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnTime.Model.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [Range(1,10,ErrorMessage ="Please enter value greater than 1 and less than 10")]
        public int ShipmentsPerWeek { get; set; }
        [Required]
        public string CnicNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string WebsiteUrl { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
    }
}
