using Microsoft.AspNetCore.Identity;
using ProductCatalogue.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalogue.Business.Base
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user,string userRole);
    }
}
