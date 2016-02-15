using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;

namespace eDirectory.Common.Dto.Address
{
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// version 0.14 Chapter XIV:  Validation
    /// </summary>
    public class AddressDto
        : ValidatorDtoBase
    {
        public long CustomerId { get; set; }
        public long Id { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [StringLength(50, ErrorMessage = "Street is 50 chars max")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City is 50 chars max")]
        public string City { get; set; }

        [Required(ErrorMessage = "Post Code is required")]
        [StringLength(50, ErrorMessage = "Post Code is 10 chars max")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(50, ErrorMessage = "Country is 50 chars max")]
        public string Country { get; set; }
    }
}
