namespace Shopping_Appilication.Models
{
    public class Bill
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserID { get; set; }
        public string MaHD { get; set; }
        public decimal ToTalPrice { get; set; }
        public int Status { get; set; }
        public virtual List<BillDetail> BillDetails { get; set; }
        public virtual User User { get; set; }
    }
}
