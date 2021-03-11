using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int Customer_id { get; set; }
        public int Products_id { get; set; }
        public DateTime Reservation { get; set; }
    }
}