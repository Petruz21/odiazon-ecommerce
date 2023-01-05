using AutoMapper;
using Microsoft.EntityFrameworkCore;
using odiazon.data;
using odiazon.data_response;
using odiazon.dtos.d_categoryDto;
using odiazon.models.m_category;

namespace odiazon.services.s_category
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CategoryService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET from db all the categories and send it to the requester
        public async Task<Response<List<GetCategoryDto>>> GetAllCategories()
        {
            Response<List<GetCategoryDto>> response = new();
            try
            {
                response.Data = await _context.Categories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToListAsync()
                    ?? throw new Exception("No categories found in database");
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // CREATE a category and add it to the db
        public async Task<Response<GetCategoryDto>> AddNewCategory(AddCategoryDto newCategory)
        {
            Response<GetCategoryDto> response = new();
            try
            {
                Category? dbCategory = _mapper.Map<Category?>(newCategory);
                await _context.Categories.AddAsync(_mapper.Map<Category>(newCategory));
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCategoryDto>(dbCategory);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // DELETE a category from the db - ONLY IF NO PRODUCTS INSIDE
        public async Task<Response<string>> DeleteCategory(int id)
        {
            Response<string> response = new();
            try
            {
                Category? dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id)
                    ?? throw new Exception("Category not found in database");

                _context.Categories.Remove(dbCategory);
                await _context.SaveChangesAsync();

                response.Data = "Category " + dbCategory.Name + " removed";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // UPDATE a category from the db
        public async Task<Response<GetCategoryDto>> UpdateCategory(int id, UpdateCategoryDto updateCategory)
        {
            Response<GetCategoryDto> response = new();
            try
            {
                Category? dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id)
                    ?? throw new Exception("Product not found in database");

                dbCategory.Name = updateCategory.Name;
                dbCategory.Info = updateCategory.Info;
                dbCategory.Visible = updateCategory.Visible;

                _context.Categories.Update(dbCategory);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCategoryDto>(dbCategory);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
