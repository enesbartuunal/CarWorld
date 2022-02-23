using ProductCatalogue.Common;
using ProductCatalogue.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogue.UI.Services.Base
{
    public interface ICategoryHttpService
    {
        Task<Result<IEnumerable<CategoryModel>>> GetCategory();
        Task<Result<CategoryModel>> GetCategoryById(string id);
        Task<Result<CategoryModel>> CreateCategory(CategoryModel model);
        Task<Result<CategoryModel>> UpdateCategory(CategoryModel model);
        Task<Result<CategoryModel>> DeleteCategory(string id);
    }
}
