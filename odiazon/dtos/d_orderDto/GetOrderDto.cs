using odiazon.dtos.d_productOrderDto;
using odiazon.dtos.d_userDto;
using odiazon.models.m_productOrder;
using odiazon.models.m_user;

namespace odiazon.dtos.d_orderDto
{
    public class GetOrderDto
    {

        // connecting Order with User, every Order have one User when is created
        public UserForOrderDto? User { get; set; }

        // a string to track orders, this parameter is unique, see DataContext for more about it
        public string? Reference { get; set; }

        // parameter to track the date of every order that a User make
        public DateTime CreatedDate { get; set; }

        public string? CustomMessage { get; set; }

        // a many to many realtionship to connect an order with a list of products
        public List<GetProductOrderDto>? ProductOrder { get; set; }
    }
}
