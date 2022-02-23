using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductCatalogue.Models.Models
{
   public class OfferModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Price is required field")]
        public double Price { get; set; }

        public bool IsActive { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }
    }
}
