using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OnlineShop2.Models
{
    public class SpecialTag
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Special Tag")]
        public string Name { get; set; }
    }
}
