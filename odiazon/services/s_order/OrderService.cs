using AutoMapper;
using Microsoft.EntityFrameworkCore;
using odiazon.data;
using odiazon.data_response;
using odiazon.dtos.d_orderDto;
using odiazon.models.m_order;
using odiazon.models.m_product;
using odiazon.models.m_productOrder;
using odiazon.models.m_user;
using System.Security.Claims;

namespace odiazon.services.s_order
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        // GET all orders of logged user
        public async Task<Response<List<GetOrderDto>>> GetUserOrders() 
        {
            Response<List<GetOrderDto>> response = new();
            try
            {
                List<Order> orders = await _context.Orders.Include(o => o.User).Include(o => o.ProductOrder)
                    .ThenInclude(po => po.Product).Where(o => o.UserId == GetUserId() ).ToListAsync();
                response.Data = orders.Select(o => _mapper.Map<GetOrderDto>(o)).ToList();
                return response;

            } catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // GET from db all the orders and send it to the requester
        public async Task<Response<List<GetOrderDto>>> GetAllOrders()
        {
            Response<List<GetOrderDto>> response = new();
            try
            {
                List<Order> orders = await _context.Orders.Include(o => o.User).Include(o => o.ProductOrder)
                    .ThenInclude(po => po.Product).ThenInclude(p => p.Category).ToListAsync();
                response.Data = orders.Select(o => _mapper.Map<GetOrderDto>(o)).ToList();
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // CREATE a order and add it to the db
        public async Task<Response<string>> AddNewOrder(AddOrderParametersDto orderParameters)
        {
            Response<string> respone = new();
            try
            {

                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
                Order order = new()
                {
                    User = user,
                    Reference = GenerateUniqueReference(),
                    CreatedDate = DateTime.Now,
                    CustomMessage = orderParameters.CustomMessage
                };

                // add order to db and save changes
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // add products to productOrder table in db
                for (int i = 0; i < orderParameters.ProductsId.Count; i++)
                {
                    ProductOrder productOrder = new()
                    {
                        OrderId = order.Id,
                        ProductId = orderParameters.ProductsId[i].ProductId
                    };
                    _context.ProductOrders.Add(productOrder);
                    await _context.SaveChangesAsync();
                }

                // ----------------------------
                respone.Data = "Order successifully created! - Reference ID: " + order.Reference;
                return respone;


            } catch (Exception ex)
            {
                respone.Success = false;
                respone.Message = ex.Message;
                return respone;
            }
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string GenerateUniqueReference() => Guid.NewGuid().ToString();


    }
}
