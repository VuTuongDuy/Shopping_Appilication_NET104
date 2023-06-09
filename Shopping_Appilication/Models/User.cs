﻿namespace Shopping_Appilication.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid? RoleID { get; set; }
        public string? Email { get; set; }
        public string? ResetPasswordToken { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public int Status { get; set; }
        public virtual Role Role { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual List<Bill> Bills { get; set; }
    }
}
