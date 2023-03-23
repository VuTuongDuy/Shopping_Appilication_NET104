namespace Shopping_Appilication.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int AvailableQuantity { get; set; }
        public int Status { get; set; }
        public string Supplier { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? IdColor { get; set; }
        public Guid? IdSize { get; set; }
        public Guid? IdSole { get; set; }
        public Guid? IdImage { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public Sole Sole { get; set; }
        public Image Image { get; set; }
        public virtual List<BillDetail> BillDetails { get; set; }
        public virtual List<CartDetail> CartDetails { get; set; }
    }
}
