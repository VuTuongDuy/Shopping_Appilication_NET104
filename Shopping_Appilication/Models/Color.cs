namespace Shopping_Appilication.Models
{
    public class Color
    {
        public Guid IdColor { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
