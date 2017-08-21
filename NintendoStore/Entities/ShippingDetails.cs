using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NintendoStore.Entities
{
    public class ShippingDetails
    {   
      public string Title { get; set; }
      [Required(ErrorMessage = "Please supply a first name")]
      public string FirstName { get; set; }
      [Required(ErrorMessage = "Please supply a last name.")]
      public string LastName { get; set; }
      [Required(ErrorMessage = "Please supply an address.")]
      public string Address { get; set; }
      [Required(ErrorMessage = "You must supply a country for this address.")]
      public string Country { get; set; }
      [Required(ErrorMessage = "You must supply a city for this address.")]
      public string City { get; set; }
      public string County { get; set; }
      [Required(ErrorMessage = "Please enter the postcode for this address.")]
      public string PostCode { get; set; }
      public bool GiftWrap { get; set; }
       
    }
}