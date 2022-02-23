using AutoMapper;
using ProductCatalogue.Business.Base;
using ProductCatalogue.DataAccess.Context;
using ProductCatalogue.DataAccess.Entities;
using ProductCatalogue.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalogue.Business.Implementaion
{
    public class CategoryService:ServiceAbstractBase<Category,CategoryModel>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CategoryService(ApplicationDbContext db,IMapper mapper):base(db,mapper)
        {
            _db = db;
            _mapper = mapper;
        }
    }
}
