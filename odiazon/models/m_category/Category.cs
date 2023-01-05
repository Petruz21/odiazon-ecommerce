using odiazon.models.m_product;

namespace odiazon.models.m_category
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
        public bool? Visible { get; set; }

        // connecting categories and products with a one to many relationship
        public List<Product>? Products { get; set; }
    }
}
