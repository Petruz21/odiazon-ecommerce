using odiazon.models.c_cart;
using odiazon.models.m_category;
using odiazon.models.m_productOrder;

namespace odiazon.models.m_product
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // connecting products and categories with a one to many relationship
        public Category? Category { get; set; }

        // track the path or url of an image
        public string? UrlPhoto { get; set; }
        public string? Info { get; set; }
        public float? Price { get; set; }

        // a bool parameter for admin dashboard, control the visibility of an product in the view
        public bool? Visible { get; set; }
        public int? StockQuantity { get; set; }

        // a many to many realtionship to connect a list of products with an order
        public List<ProductOrder>? ProductOrder { get; set; }

    }
}
