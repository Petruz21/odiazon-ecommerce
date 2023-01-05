using odiazon.data_response;
using odiazon.dtos.d_orderDto;

namespace odiazon.services.s_order
{
    public interface IOrderService
    {
        public Task<Response<List<GetOrderDto>>> GetUserOrders();
        public Task<Response<List<GetOrderDto>>> GetAllOrders();
        public Task<Response<string>> AddNewOrder(AddOrderParametersDto orderParameters);
    }
}
