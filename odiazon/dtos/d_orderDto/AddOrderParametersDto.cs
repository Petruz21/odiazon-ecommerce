namespace odiazon.dtos.d_orderDto
{
    public class AddOrderParametersDto
    {
        public int UserId { get; set; }
        public string? CustomMessage { get; set; }
        public List<AddProductsToOrderDto>? ProductsId { get; set; }
    }
}
