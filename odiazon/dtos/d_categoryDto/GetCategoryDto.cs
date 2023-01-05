using odiazon.models.m_product;

namespace odiazon.dtos.d_categoryDto
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
        public bool? Visible { get; set; }
    }
}
