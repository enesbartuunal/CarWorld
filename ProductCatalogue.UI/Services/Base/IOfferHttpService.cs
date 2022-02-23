using ProductCatalogue.Common;
using ProductCatalogue.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogue.UI.Services.Base
{
    public interface IOfferHttpService
    {
        Task<Result<IEnumerable<OfferModel>>> GetOffer();
        Task<Result<OfferModel>> GetOfferById(string id);
        Task<Result<OfferModel>> CreateOffer(OfferModel model);
        Task<Result<OfferModel>> UpdateOffer(OfferModel model);
        Task<Result<OfferModel>> DeleteOffer(string id);
    }
}
