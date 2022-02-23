using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductCatalogue.DataAccess.Entities
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CategoryName { get; set; }

        //Relation
        public virtual List<Product> Products { get; set; }

        public int UserId { get; set; }

        public virtual  User User { get; set; }
    }
}
