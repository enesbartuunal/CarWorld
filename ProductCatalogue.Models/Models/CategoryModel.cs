using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductCatalogue.Models.Models
{
    public  class CategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required field")]
        public string CategoryName { get; set; }
    }
}
