using odiazon.dtos.d_categoryDto;

namespace odiazon.dtos.d_productDto
{
    public class AddProductDto
    {
        public string? Name { get; set; }

        // this paramteres take an id of a category, the parameter is inexistent in the Product Entity
        public int? CategoryId { get; set; }

        // track the path or url of an image
        public string? UrlPhoto { get; set; }
        public string? Info { get; set; }
        public float? Price { get; set; }

        // a bool parameter for admin dashboard, control the visibility of an product in the view
        public bool? Visible { get; set; }
        public int? StockQuantity { get; set; }
    }
}
