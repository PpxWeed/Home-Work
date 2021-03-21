using System.ComponentModel.DataAnnotations;

namespace Webapplication1.DTO
{
    public class AddProducts
    {
        [Required]
        public string Product_name { get; set; }
        [Required]
        public string Plats { get; set; }
        [Required]
        public string Drinks { get; set; }
        [Required]
        public string Deserts { get; set; }
 
    }
}