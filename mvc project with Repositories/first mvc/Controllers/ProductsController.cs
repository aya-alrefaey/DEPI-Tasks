using first_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using first_mvc.Repositories;

namespace first_mvc.Controllers
{
    public class ProductsController : Controller
    {
        IProductRepo productrepo;
        ICategoriesRepo catrepo;
        public ProductsController(IProductRepo pro, ICategoriesRepo cat)
        {
            productrepo = pro;
            catrepo = cat;
        }
        [Route("/admin/products")]
        public IActionResult Index()
        {

            var products = productrepo.get_all();
            return View(products);
        }

        [HttpGet]
        [Route("/admin/products/add")]
        public IActionResult Add_Product()
        {
            var obj = new ProductCreateViewModel
            {
                product = new Products(),
                categories = catrepo.get_all()
            };
            return View(obj);
        }
        [HttpPost]
        [Route("/admin/products/add")]
        public IActionResult Add_Product(ProductCreateViewModel pro)
        {
            if (string.IsNullOrEmpty(pro.product.Name))
            {
                errormsg("Plz Enter Product Name");
                pro.categories = catrepo.get_all();
                return View(pro);
            }
            if (string.IsNullOrEmpty(pro.product.Price))
            {
                errormsg("Plz Enter Product Price");
                pro.categories = catrepo.get_all();
                return View(pro);
            }
            productrepo.add(pro.product);
            productrepo.save();
            //hc.Add(pro.product);
            //hc.SaveChanges();

            return RedirectToAction("Index", "Products");
        }
        private void errormsg(string message)
        {
            ViewBag.error = message;
        }

        [HttpGet]
        [Route("/admin/product/edit/{id}")]
        public IActionResult edit_product(int id)
        {
            //var pro = hc.products.FirstOrDefault(e => e.Id == id);
            var pro = productrepo.find_id(id);
            if (pro == null)
            {
                return NotFound();
            }
            var viewModel = new ProductCreateViewModel
            {
                product = pro,
                categories = catrepo.get_all()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("/admin/product/edit/{id}")]
        public IActionResult edit_product(int id, ProductCreateViewModel updated)
        {
            var pro = productrepo.find_id(id);
            //var ppro=hc.products.FirstOrDefault(e => e.Id == id);
            if (pro == null)
            {
                return NotFound();
            }


            pro.Name = updated.product.Name;
            pro.Price = updated.product.Price;
            pro.Description = updated.product.Description;
            pro.Quantity = updated.product.Quantity;
            pro.categoryId = updated.product.categoryId;

            //hc.Update(pro);
            //hc.SaveChanges();
            productrepo.update(pro);
            productrepo.save();

            //ViewBag.msg = "Customer Edited Successfully";
            return RedirectToAction("index");
        }

        [Route("/admin/products/delete/{id}")]
        public IActionResult DeletProduct(int id)
        {
            //var pro = hc.products.FirstOrDefault(e => e.Id == id);
            var pro = productrepo.find_id(id);
            if (pro == null)
            {
                return NotFound();
            }
            //hc.Remove(pro);
            //hc.SaveChanges();
            productrepo.delete(pro);
            productrepo.save();
            return RedirectToAction("Index", "Products");
        }

        [Route("/admin/products/details/product_details/{id}")]

        public IActionResult product_details(int id)
        {
            //var product = hc.products.Include(e => e.category).FirstOrDefault(e => e.Id == id);
            var product = productrepo.find_withcat(id);
            return View(product);
        }
    }
}
