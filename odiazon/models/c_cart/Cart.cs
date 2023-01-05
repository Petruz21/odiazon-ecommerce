using odiazon.models.m_product;
using odiazon.models.m_user;

namespace odiazon.models.c_cart
{
    public class Cart
    {
        public int Id { get; set; }
        public List<Product>? Products { get; set; }
        public List<User>? Users { get; set; }
    }
}
