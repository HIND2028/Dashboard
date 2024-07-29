using System.Diagnostics;
using Dashboard.Data;
using Dashboard.Models;
using Dashboard.Models.ViewModel.ProductDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//using ProductDetails = Products.Models.ProductDetails;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateProducts(Product products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                _context.SaveChanges();
                TempData["Add"] = "ÊãÊ ÇáÅÖÇÝÉ ÈäÌÇÍ";
                return RedirectToAction("Addnewitems");

            }
            TempData["Add"] = "áã ÊÊã ÇáÅÖÇÝÉ íÑÌì ÇáÊÃßÏ ãä ÕÍÉ ÇáãÏÎáÇÊ";


            var product = _context.products.ToList();
            return View("Addnewitems", product);

        }
        public IActionResult CreateDetails(AddProductDetailViewModel productDetails)
        {

            if (productDetails.Images == null || productDetails.Images.Length == 0)
            {
                return Content("File Not Selected");
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "img", productDetails.Images.FileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                productDetails.Images.CopyTo(stream);
                stream.Close();
            }

            //productDetails.Images = photo.FileName;

            ProductsDetials newProductDetails = new ProductsDetials()
            {
                Color = productDetails.Color,
                Images = $"/img/{productDetails.Images.FileName}",
                Price = productDetails.Price,
                ProductId = productDetails.ProductId,
                Qty = productDetails.Qty

            };
            _context.productsDetials.Add(newProductDetails);
            _context.SaveChanges();
            return RedirectToAction("ProductsDetails");

        }
        public IActionResult DDelete(int record_no)
        {

            var productddam = _context.damegedproducts.SingleOrDefault(x => x.Id == record_no);//serch on record 

            if (productddam != null)
            {
                _context.damegedproducts.Remove(productddam);
                _context.SaveChanges();
                TempData["ddel"] = true;
            }

            else
            {
                TempData["ddel"] = false;
            }
            //ViewBag.ProductDetails = ProductDetails;
            var damegedproducts = _context.damegedproducts;
            ViewBag.damegedproducts = damegedproducts;
            //return View(productDetails);
            return RedirectToAction("Demag");

        }
        public IActionResult PDelete(int drecord_no)
        {

            var productddel = _context.productsDetials.SingleOrDefault(x => x.Id == drecord_no);//serch on record 

            if (productddel != null)
            {
                _context.productsDetials.Remove(productddel);
                _context.SaveChanges();
                TempData["ddel"] = true;
            }

            else
            {
                TempData["ddel"] = false;
            }
            //ViewBag.ProductDetails = ProductDetails;
            var productDetails = _context.productsDetials;
            ViewBag.ProductDetails = productDetails;
            //return View(productDetails);
            return RedirectToAction("ProductsDetails");

        }
        public IActionResult PEdit(int record_no)
        {

            EditProductDetailViewModel editProductDetailViewModel = _context.productsDetials.Include(pp => pp.product).Select(p => new EditProductDetailViewModel
            {
                Color = p.Color,
                Id = p.Id,
                OldImage = p.Images,
                Price = p.Price,
                Qty = p.Qty,
                ProductId = (int)p.ProductId,
                productName = p.product.Name
            }).SingleOrDefault(x => x.Id == record_no);

            return View(editProductDetailViewModel);

        }
        // new class 

        public IActionResult DEdit(int record_no)
        {

            Damegedproducts damegedproducts = _context.damegedproducts.Include(c => c.product).SingleOrDefault(x => x.Id == record_no);

            if (damegedproducts == null)
            {
                return NotFound();
            }
            else
                return View(damegedproducts);

        }
        public IActionResult DUpdate(Damegedproducts dd)
        {
            var oldDamegedproduct = _context.damegedproducts.FirstOrDefault(d => d.Id == dd.Id);
            if (oldDamegedproduct == null)
            {
                return NotFound();
            }
            else
            {
                oldDamegedproduct.Qty = dd.Qty;
                
                _context.SaveChanges();

                return RedirectToAction("Demag");
            }

        }
        public IActionResult PUpdate(EditProductDetailViewModel productDetails)
        {
            if (ModelState.IsValid)
            {
                Models.ProductsDetials oldProductDetail = _context.productsDetials.Where(p => p.Id == productDetails.Id).FirstOrDefault();
                if (oldProductDetail == null)
                {
                    return NotFound();
                }
                else
                {
                    if (productDetails.Images != null)
                    {
                        // updating old image
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "img", productDetails.Images.FileName);

                        using (FileStream stream = new FileStream(path, FileMode.Create))
                        {
                            productDetails.Images.CopyTo(stream);
                            stream.Close();
                        }

                        oldProductDetail.Images = $"/img/{productDetails.Images.FileName}";
                    }
                    oldProductDetail.Color = productDetails.Color;
                    oldProductDetail.Price = productDetails.Price;
                    oldProductDetail.Qty = productDetails.Qty;

                    _context.Update(oldProductDetail);
                    _context.SaveChanges();
                    return RedirectToAction("ProductsDetails");


                }
            }

            return RedirectToAction("ProductsDetails");
        }
        //[Route("da")]
        public IActionResult AddDemag(Damegedproducts damege)
        {

            _context.Add(damege);
            _context.SaveChanges();

            return RedirectToAction("Demag");
        }
        public IActionResult Demag()
        {

            
            //ViewBag.Username = HttpContext.Session.GetString("userdata");
            var products = _context.products.ToList();

            var Productsdemage = _context.damegedproducts.Join
                (
                     _context.products,

                      demag => demag.ProductId,
                      products => products.Id,


                     (demag, products) => new
                     {
                         demag,
                         products
                     }

                ).Join
                (
                  _context.productsDetials,

                  p => p.demag.ProductId,
                  c => c.ProductId,

                  (p, c) => new
                  {
                      id = p.demag.Id,
                      name = p.products.Name,
                      color = c.Color,
                      qty = p.demag.Qty
                  }
                  ).ToList();

            Console.WriteLine($"{Productsdemage}");


            ViewBag.products = products;
            ViewBag.damage = Productsdemage;


            return View("Damag");
        }


        public IActionResult Addnewitems()
        {
            var products = _context.products.ToList();  //Read
            ViewBag.products = products;
            return View(products);

        }


        public IActionResult Delete(int record_no)
        {
            var productdel = _context.products.SingleOrDefault(p => p.Id == record_no);//serch on record 

            if (productdel != null)
            {
                _context.products.Remove(productdel);
                _context.SaveChanges();
                TempData["del"] = true;
            }

            else
            {
                TempData["del"] = false;
            }


            return RedirectToAction("Addnewitems");

        }

        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.products.Update(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Addnewitems");
        }

        public IActionResult Edit(int record_no)
        {
            var Product = _context.products.SingleOrDefault(x => x.Id == record_no);

            return View(Product);

        }
        public IActionResult Productsdetails()
        {
            var ProductDetails = _context.productsDetials.Join(

                _context.products,

                productsdetails => productsdetails.ProductId,
                product => product.Id,

                (productsdetails, product) => new
                {
                    id = productsdetails.Id,
                    name = product.Name,
                    color = productsdetails.Color,
                    price = productsdetails.Price,
                    qty = productsdetails.Qty,
                    img = productsdetails.Images,
                }



                ).ToList();

            var prodcts = _context.products.ToList();

            ViewBag.products = prodcts;
            ViewBag.ProductDetails = ProductDetails;
            return View();


        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
