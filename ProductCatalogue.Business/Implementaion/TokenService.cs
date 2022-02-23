using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductCatalogue.Business.Base;
using ProductCatalogue.Common;
using ProductCatalogue.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductCatalogue.Business.Implementaion
{

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }

        public string CreateToken(IdentityUser user,string userRole)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Id",user.Id)
            };
            var role="";
            if (userRole== "Admin")
                 role = ResultConstant.Role_Admin;
            else
                 role = ResultConstant.Role_Member;
            claim.Add(new Claim(ClaimTypes.Role, role));

            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new JwtSecurityToken
            (
                issuer: _config["Token:Issuer"],
                audience: _config["Token:Audience"],
                claims: claim,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: cred
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return token;
        }
    }

}
