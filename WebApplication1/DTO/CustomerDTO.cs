using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Webapplication1.DTO
{
    public class CustomerDTO
    {
        [Key]
        public int customers_numbers { get; set; }
        public int customers_id_Id {get; set;}
        public int customers_id { get; set; }
        public string customers_name { get; set; }
        public string customers_occasion { get; set; }

        public List<ProductsDTO> Production { get; set; }
    }
}