using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OnlineShop2.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Type")]
        public string ProductTypeName { get; set; }
    }
}
