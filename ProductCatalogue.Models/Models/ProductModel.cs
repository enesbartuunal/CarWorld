using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductCatalogue.Models.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required field")]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Description is required field")]
        [MaxLength(500)]
        public string Description { get; set; }

        public string Color { get; set; }

        public string Brand { get; set; }
        [Required(ErrorMessage = "Status is required field")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Image is required field")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Price is required field")]
        public double Price { get; set; }

        public bool IsOfferable { get; set; }
        [Required(ErrorMessage = "Category is required field")]
        public int CategoryId { get; set; }

        public bool IsSold { get; set; }

        public string UserId { get; set; }
    }
}
