using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models.ViewModel.ProductDetails
{
    public class AddProductDetailViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "يرجى ادخال صورة المنتج")]
        public IFormFile Images { get; set; }
        [Required(ErrorMessage = "يرجى ادخال سعر المنتج")]
        public double Price { get; set; }
        [Required(ErrorMessage = "يرجى ادخال كمية المنتج")]
        public int Qty { get; set; }
        [Required(ErrorMessage = "يرجى ادخال لون المنتج")]
        public string Color { get; set; }
    }
}
