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
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductCatalogue.UI.Services.Implementation
{
    public class CategoryHttpService : ICategoryHttpService
    {
        private readonly HttpClient _client;

        public CategoryHttpService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Result<CategoryModel>> CreateCategory(CategoryModel model)
        {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("categories/", bodyContent);
            string res = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<CategoryModel>(data);
                return new Result<CategoryModel>(true, ResultConstant.RecordCreateSuccessfully, result);
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                return new Result<CategoryModel>(false, ResultConstant.RecordCreateNotSuccessfully);
            }
        }

        public async Task<Result<CategoryModel>> DeleteCategory(string id)
        {
            var url = Path.Combine("categories", id.ToString());
            var deleteResult = await _client.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();
            if (deleteResult.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<CategoryModel>(deleteContent);
                return new Result<CategoryModel>(true, ResultConstant.RecordRemoveSuccessfully, result);
            }
            else
            {
                return new Result<CategoryModel>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }

        public async Task<Result<IEnumerable<CategoryModel>>> GetCategory()
        {
            var response = await _client.GetAsync("categories/getall");
            var content = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(content);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            return new Result<IEnumerable<CategoryModel>>(true, ResultConstant.RecordFound, categories);
        }

        public async Task<Result<CategoryModel>> GetCategoryById(string id)
        {
            var url = Path.Combine("categories/", id);
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var category = JsonConvert.DeserializeObject<CategoryModel>(content);
            return new Result<CategoryModel>(true, ResultConstant.RecordFound, category);
        }

        public async Task<Result<CategoryModel>> UpdateCategory(CategoryModel model)
        {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("categories", model.Id.ToString());
            var putResult = await _client.PutAsync(url, bodyContent);
            var putContent = await putResult.Content.ReadAsStringAsync();
            if (putResult.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<CategoryModel>(putContent);
                return new Result<CategoryModel>(true, ResultConstant.RecordUpdateSuccessfully, result);
            }
            else
            {
                return new Result<CategoryModel>(false, ResultConstant.RecordUpdateNotSuccessfully);
            }
        }
    }
}
