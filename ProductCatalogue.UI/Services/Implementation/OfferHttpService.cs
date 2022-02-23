using Newtonsoft.Json;
using ProductCatalogue.Common;
using ProductCatalogue.Models.Models;
using ProductCatalogue.UI.Services.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.UI.Services.Implementation
{
    public class OfferHttpService : IOfferHttpService
    {
        private readonly HttpClient _client;

        public OfferHttpService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Result<OfferModel>> CreateOffer(OfferModel model)
        {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("offers/", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<OfferModel>(data);
                return new Result<OfferModel>(true, ResultConstant.RecordCreateSuccessfully, result);
            }
            else
            {
                return new Result<OfferModel>(false, ResultConstant.RecordCreateNotSuccessfully);
            }
        }

        public async Task<Result<OfferModel>> DeleteOffer(string id)
        {
            var url = Path.Combine("offers", id.ToString());
            var deleteResult = await _client.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();
            if (deleteResult.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<OfferModel>(deleteContent);
                return new Result<OfferModel>(true, ResultConstant.RecordRemoveSuccessfully, result);
            }
            else
            {
                return new Result<OfferModel>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }

        public async Task<Result<IEnumerable<OfferModel>>> GetOffer()
        {
            var response = await _client.GetAsync("offers/getall");
            var content = await response.Content.ReadAsStringAsync();
            var offers = JsonConvert.DeserializeObject<IEnumerable<OfferModel>>(content);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            return new Result<IEnumerable<OfferModel>>(true, ResultConstant.RecordFound, offers);
        }

        public async Task<Result<OfferModel>> GetOfferById(string id)
        {
            var url = Path.Combine("offers/", id);
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var offer = JsonConvert.DeserializeObject<OfferModel>(content);
            return new Result<OfferModel>(true, ResultConstant.RecordFound, offer);
        }

        public async Task<Result<OfferModel>> UpdateOffer(OfferModel model)
        {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("offers", model.Id.ToString());
            var putResult = await _client.PutAsync(url, bodyContent);
            var putContent = await putResult.Content.ReadAsStringAsync();
            if (putResult.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<OfferModel>(putContent);
                return new Result<OfferModel>(true, ResultConstant.RecordUpdateSuccessfully, result);
            }
            else
            {
                return new Result<OfferModel>(false, ResultConstant.RecordUpdateNotSuccessfully);
            }
        }
    }
}
