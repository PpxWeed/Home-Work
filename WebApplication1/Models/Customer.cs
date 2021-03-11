using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models
{
    public class Customer
    {
        [Key]
        public int id { get; set; }
        
        public int customers_numbers { get; set; }
    }
}