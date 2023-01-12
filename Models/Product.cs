using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop2.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Display(Name ="Product Color")]
        public string ProductColor { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Required]
        [Display(Name = "Product Type")]
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        [ValidateNever]
        public ProductType ProductType { get; set; }

        [Required]
        [Display(Name = "Special Tag")]
        public int SpecialTagId { get; set; }

        [ForeignKey("SpecialTagId")]
        [ValidateNever]
        public SpecialTag SpecialTag { get; set; }

    }
}
