namespace Shopping_Appilication.Models
{
    public class HoaDonViewModel
    {
        public string HoTen { get; set; }
        public string MaHD { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public int quantityOk { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal TongTien { get; set; }
        public DateTime NgayMua { get; set; }
        public int TrangThaiThanhToan { get; set; }
        public List<CartItem> CartItems { get; set; }
        public HoaDonViewModel()
        {
            CartItems = new List<CartItem>();
        }
    }
}
