namespace Shopping_Appilication.Models
{
    public class CartItem
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public string Size { get; set; }
        public string? ProductImage { get; set; }

    }
}
