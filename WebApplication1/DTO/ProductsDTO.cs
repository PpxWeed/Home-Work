using System;


namespace Webapplication1.DTO
{
    public class ProductsDTO
    {
        internal int id {get; set;}
        public int Products_id { get; set; }
        public string Product_name { get; set; }
        public string plats { get; set; }
        public string drinks { get; set; }
        public string deserts { get; set; }
        public DateTime Reservation { get; set; }
    }
}