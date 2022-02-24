using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductCatalogue.DataAccess.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public string Brand { get; set; }

        public string Status { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public bool IsOfferable { get; set; }

        public bool IsSold { get; set; }

        //Relation

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<Offer> Offers { get; set; }

        public virtual User User { get; set; }

        public string UserId { get; set; }
    }
}
