﻿namespace Shopping_Appilication.Models
{
    public class Size
    {
        public Guid IdSize { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
