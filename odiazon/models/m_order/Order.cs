using odiazon.models.m_productOrder;
using odiazon.models.m_user;

namespace odiazon.models.m_order
{
    public class Order
    {
        public int Id { get; set; }

        // connecting Order with User, every Order have one User when is created
        public User? User { get; set; }
        public int? UserId { get; set; }

        // a string to track orders, this parameter is unique, see DataContext for more about it
        public string? Reference { get; set; }

        // parameter to track the date of every order that a User make
        public DateTime CreatedDate { get; set; }

        public string? CustomMessage { get; set; }

        // a many to many realtionship to connect an order with a list of products
        public List<ProductOrder>? ProductOrder { get; set; }
    }
}
