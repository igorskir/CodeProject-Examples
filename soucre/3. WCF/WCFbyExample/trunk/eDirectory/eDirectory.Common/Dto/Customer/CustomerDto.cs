using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Address;
using eDirectory.Common.Message;

namespace eDirectory.Common.Dto.Customer
{
    /// <remarks>
    /// version 0.03 Chapter III:  The Response
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// version 0.14 Chapter XIV:  Validation
    /// </remarks>
    public class CustomerDto
        : ValidatorDtoBase
    {
        public CustomerDto()
        {
            Addresses = new List<AddressDto>();
        }
        
        public long Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "Last Name is 50 chars max")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required") ]
        [StringLength(50, ErrorMessage = "Last Name is 50 chars max")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        [StringLength(8, ErrorMessage = "Telephone is 8 chars max")]
        [RegularExpression(@"\d\d\d\-\d\d\d\d", ErrorMessage = @"Telephone format must be xxx-xxxx")]
        public string Telephone { get; set; }

        public List<AddressDto> Addresses { get; set; }

        
    }   
}
