using AutoMapper;
using ProductCatalogue.DataAccess.Entities;
using ProductCatalogue.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalogue.Business.Mapper
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
            CreateMap<Offer, OfferModel>();
            CreateMap<OfferModel, Offer>();
        }
    }
}
