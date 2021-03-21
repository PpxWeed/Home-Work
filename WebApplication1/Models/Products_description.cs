using System.ComponentModel.DataAnnotations;

namespace Webapplication1.Models
{
    public class Products_description
    {
        [Key]
        public int id { get; set; }
        public int products_id { get; set; }
        public string plats { get; set; }
        public string drinks { get; set; }
        public string deserts { get; set; }
    }
}