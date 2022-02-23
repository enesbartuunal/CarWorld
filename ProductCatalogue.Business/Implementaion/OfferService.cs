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
    public class OfferService: ServiceAbstractBase<Offer,OfferModel>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public OfferService(ApplicationDbContext db, IMapper mapper):base(db,mapper)
        {
            _db = db;
            _mapper = mapper;
        }
    }
}
