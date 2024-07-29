using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [InverseProperty(nameof(Dashboard.Models.ProductsDetials.product))]

        public ICollection<ProductsDetials> ProductsDetials { get; set; }
        [InverseProperty(nameof(Dashboard.Models.Damegedproducts.product))]
        public ICollection<Damegedproducts> Damegedproducts { get; set; }

    }
}
