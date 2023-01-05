using AutoMapper;
using Microsoft.EntityFrameworkCore;
using odiazon.data;
using odiazon.data_response;
using odiazon.dtos.d_productDto;
using odiazon.models.m_product;

namespace odiazon.services.s_product
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET from db all the products and send it to the requester
        public async Task<Response<List<GetProductDto>>> GetAllProducts()
        {
            Response<List<GetProductDto>> response = new();

            try
            {
                response.Data = await GetProductsFromDb() ?? throw new Exception("No products in database");
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // GET from db a single product by id and send it to the requester
        public async Task<Response<GetProductDto>> GetProductById(int id)
        {
            Response<GetProductDto> response = new();
            try
            {
                response.Data = await GetProductByIdFromDb(id) ?? throw new Exception("product not found in database");
                return response;


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // CREATE a product and add it to the db
        public async Task<Response<GetProductDto>> AddNewProduct(AddProductDto newProduct)
        {
            Response<GetProductDto> response = new();
            try
            {
                Product product = _mapper.Map<Product>(newProduct);
                // Set the product cateogry by taken the CategoryId parameter of newProduct after
                // checking if exists in the db
                product.Category = _context.Categories.FirstOrDefault(c => c.Id == newProduct.CategoryId);

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetProductDto>(product);
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // DELETE a product from the db
        public async Task<Response<string>> DeleteProduct(int id)
        {
            Response<string> response = new();
            try
            {
                Product? dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id)
                    ?? throw new Exception("Product not found in database");

                _context.Products.Remove(dbProduct);
                await _context.SaveChangesAsync();
                response.Data = "Product " + dbProduct.Name + " removed successfully";
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
        }

        // UPDATE a product from the db
        public async Task<Response<GetProductDto>> UpdateProduct(int id, UpdateProductDto updateProduct)
        {
            Response<GetProductDto> response = new();
            try
            {
                Product? dbProduct = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id)
                    ?? throw new Exception("Product not found in database");

                dbProduct.Name = updateProduct.Name;
                dbProduct.Category = _context.Categories.FirstOrDefault(c => c.Id == updateProduct.CategoryId);
                dbProduct.UrlPhoto = updateProduct.UrlPhoto;
                dbProduct.Info = dbProduct.Info;
                dbProduct.Price = updateProduct.Price;
                dbProduct.Visible = updateProduct.Visible;
                dbProduct.StockQuantity = updateProduct.StockQuantity;

                _context.Update(dbProduct);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetProductDto>(dbProduct);
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<Response<List<GetProductDto>>> GetProductByCategory(int categoryId)
        {
            Response<List<GetProductDto>> response = new();
            try
            {
                if (categoryId == 0)
                {
                    response.Data = await GetProductsFromDb();
                    return response;
                }
                List<Product> dbProduct = await _context.Products.Where(p => p.Category.Id == categoryId).ToListAsync();
                response.Data = dbProduct.Select(p => _mapper.Map<GetProductDto>(p)).ToList();
                return response;


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // ---------- this privates methods are used as utility, make the code cleaner and more readable ----------\\


        // return a list of products from database, thats include the category entity
        private async Task<List<GetProductDto>> GetProductsFromDb()
        {
            List<Product> products = await _context.Products.Include(p => p.Category).ToListAsync();
            return products.Select(p => _mapper.Map<GetProductDto>(p)).ToList();

        }

        // return a single product from database
        private async Task<GetProductDto> GetProductByIdFromDb(int id)
        {
            Product? product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<GetProductDto>(product);
        }


        // ------------------------------------------------------------------------------------------------------- \\
    }
}
