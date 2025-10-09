using first_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace first_mvc.Controllers
{
    public class ProductsController : Controller
    {
        HandoraContext hc = new HandoraContext();
        [Route("/admin/products")]
        public IActionResult Index()
        {
            
            var products = hc.products.ToList();
            return View(products);
        }

        [HttpGet]
        [Route("/admin/products/add")]
        public IActionResult Add_Product()
        {
            var cat = hc.categories.ToList();
            return View(cat);
        }
        [HttpPost]
        [Route("/admin/products/add")]
        public IActionResult Add_Product(Products pro)
        {
            
            hc.Add(pro);
            hc.SaveChanges();
          
            return RedirectToAction("Index", "Products");
        }
    }
}
