using odiazon.dtos.d_categoryDto;
using odiazon.models.m_productOrder;

namespace odiazon.dtos.d_productDto
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // connecting products and categories with a one to many relationship
        public GetCategoryDto? Category { get; set; }

        // track the path or url of an image
        public string? UrlPhoto { get; set; }
        public string? Info { get; set; }
        public float? Price { get; set; }

        // a bool parameter for admin dashboard, control the visibility of an product in the view
        public bool? Visible { get; set; }
        public int? StockQuantity { get; set; }
    }
}
