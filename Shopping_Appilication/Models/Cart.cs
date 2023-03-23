namespace Shopping_Appilication.Models
{
    public class Cart
    {
        public Guid UserID { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
        public virtual List<CartDetail> CartDetails { get; set; }
    }
}
