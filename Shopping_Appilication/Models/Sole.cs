namespace Shopping_Appilication.Models
{
    public class Sole// đế giày
    {
        public Guid IdSole { get; set; }
        public string Name { get; set; }
        public string Fabric { get; set; }//Chất liệu
        public int Height { get; set; }
        public int Status { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
