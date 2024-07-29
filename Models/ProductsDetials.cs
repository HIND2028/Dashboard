using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Models
{
    public class ProductsDetials
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Product.ProductsDetials))]
       // [InverseProperty(nameof(Product.Models.Product.ProductDetails))]

        public Product?product { get; set; }

        [Required]  
        [StringLength(30)]
        public string?Color { get; set; }
        [Required]      
        [StringLength(30)]
        public int Qty { get; set; }
        [Required]
        [StringLength(50)]
        public string?Images { get; set; }
        [Required]
        public double Price { get; set; }
        public int? ProductId { get; set; }

     
    }
}
