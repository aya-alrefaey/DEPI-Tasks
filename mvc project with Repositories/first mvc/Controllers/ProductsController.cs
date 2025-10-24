using first_mvc.Models;
using first_mvc.Repositories;
using first_mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace first_mvc.Controllers
{
    public class ProductsController : Controller
    {
        //IProductRepo productrepo;
        //ICategoriesRepo catrepo;
        IProductService proser;
        ICategoryService catser;
        public ProductsController(IProductService pro,ICategoryService cat)
        {
            proser = pro;
            catser= cat;
        }
        [Route("/admin/products")]
        public IActionResult Index()
        {

            var products = proser.get_all();
            return View(products);
        }

        [HttpGet]
        [Route("/admin/products/add")]
        public IActionResult Add_Product()
        {
            var obj = new ProductCreateViewModel
            {
                product = new Products(),
                categories = catser.get_all()
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
                pro.categories = catser.get_all();
                return View(pro);
            }
            if (string.IsNullOrEmpty(pro.product.Price))
            {
                errormsg("Plz Enter Product Price");
                pro.categories = catser.get_all();
                return View(pro);
            }
            proser.add(pro.product);
            //productrepo.save();
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
            var pro = proser.find_id(id);
            if (pro == null)
            {
                return NotFound();
            }
            var viewModel = new ProductCreateViewModel
            {
                product = pro,
                categories = catser.get_all()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("/admin/product/edit/{id}")]
    public IActionResult edit_product(Products pro)
        {
          
            if (pro == null)
            {
                return NotFound();
            }


            proser.update(pro);
           
            return RedirectToAction("index");
        }

        [Route("/admin/products/delete/{id}")]
        public IActionResult DeletProduct(int id)
        {
            //var pro = hc.products.FirstOrDefault(e => e.Id == id);
            var pro = proser.find_id(id);
            if (pro == null)
            {
                return NotFound();
            }
            //hc.Remove(pro);
            //hc.SaveChanges();
            proser.delete(pro);
            //productrepo.save();
            return RedirectToAction("Index", "Products");
        }

        [Route("/admin/products/details/product_details/{id}")]

        public IActionResult product_details(int id)
        {
            //var product = hc.products.Include(e => e.category).FirstOrDefault(e => e.Id == id);
            var product = proser.find_withcat(id);
            return View(product);
        }
    }
}
