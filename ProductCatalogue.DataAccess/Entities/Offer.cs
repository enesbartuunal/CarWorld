using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductCatalogue.DataAccess.Entities
{
    public class Offer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double Price { get; set; }

        public bool IsActive { get; set; }

        //Relation
        public string UserId { get; set; }

        public virtual  User User { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

    }
}
