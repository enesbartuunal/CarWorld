using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalogue.DataAccess.Entities
{
    public class User : IdentityUser
    {
        //Relation

        public virtual List<Offer> Offers { get; set; }

        public virtual List<Product> Products { get; set; }

        public virtual List<Category> Categories { get; set; }
    }
}
