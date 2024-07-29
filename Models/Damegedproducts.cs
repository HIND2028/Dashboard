using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Models
{
    public class Damegedproducts
    {
        [Key]

        public int Id { get; set; }
        //[Required]
        public int? ProductId { get; set; }
        public string Qty { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Product.Damegedproducts))]
        // [InverseProperty(nameof(Product.Models.Product.ProductDetails))]

        public Product? product { get; set; }

    }
}
