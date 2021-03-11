using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models
{
    public class Products
    {
        [Key]
        public int id { get; set; }
        public string product_name { get; set; }
    }
}