using odiazon.models.c_cart;
using odiazon.models.m_order;

namespace odiazon.models.m_user
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public Cart? Cart { get; set; }
        public int? CartId { get; set; }

        // connecting user with orders, every User have a list of orders
        public List<Order>? Orders { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}
