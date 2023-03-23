namespace Shopping_Appilication.Models
{
    public class Image
    {
        public Guid IdImage { get; set; }
        public string Name { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public int Status { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
