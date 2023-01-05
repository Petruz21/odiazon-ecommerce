using odiazon.data_response;
using odiazon.dtos.d_categoryDto;

namespace odiazon.services.s_category
{
    public interface ICategoryService
    {
        public Task<Response<List<GetCategoryDto>>> GetAllCategories();
        public Task<Response<GetCategoryDto>> AddNewCategory(AddCategoryDto newCategory);
        public Task<Response<string>> DeleteCategory(int id);
        public Task<Response<GetCategoryDto>> UpdateCategory(int id, UpdateCategoryDto updateCategory);
    }
}
