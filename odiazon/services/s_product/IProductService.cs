using odiazon.data_response;
using odiazon.dtos.d_productDto;

namespace odiazon.services.s_product
{
    public interface IProductService
    {
        public Task<Response<List<GetProductDto>>> GetAllProducts();
        public Task<Response<GetProductDto>> GetProductById(int id);
        public Task<Response<GetProductDto>> AddNewProduct(AddProductDto newProduct);
        public Task<Response<string>> DeleteProduct(int id);
        public Task<Response<GetProductDto>> UpdateProduct(int id, UpdateProductDto updateProduct);
        public Task<Response<List<GetProductDto>>> GetProductByCategory(int categoryId);
    }
}
