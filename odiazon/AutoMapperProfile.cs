using AutoMapper;
using odiazon.dtos.d_categoryDto;
using odiazon.dtos.d_orderDto;
using odiazon.dtos.d_productDto;
using odiazon.dtos.d_productOrderDto;
using odiazon.dtos.d_userDto;
using odiazon.models.m_category;
using odiazon.models.m_order;
using odiazon.models.m_product;
using odiazon.models.m_productOrder;
using odiazon.models.m_user;

namespace odiazon
{
    public class AutoMapperProfile : Profile
    {
        // constructor
        public AutoMapperProfile()
        {
            // Product
            CreateMap<Product, GetProductDto>();
            CreateMap<AddProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // Category
            CreateMap<Category, GetCategoryDto>();
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // Order & utility
            CreateMap<Order, GetOrderDto>();
            CreateMap<User, UserForOrderDto>();
            CreateMap<Product, ProductForOrderDto>();

            // User
            CreateMap<User, GetUserDto>();

            // ProductOrder
            CreateMap<ProductOrder, GetProductOrderDto>();
        }
    }
}
