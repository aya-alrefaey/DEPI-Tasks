using first_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace first_mvc.Controllers
{
    public class DetailsController1 : Controller
    {
        HandoraContext hc = new HandoraContext();
        [Route("/admin/customer/details/index/{id}")]
        public IActionResult Index(int id)
        {
            var customer = hc.customers.FirstOrDefault(e => e.Id == id);
            return View(customer);
        }
        [Route("/admin/products/details/product_details/{id}")]

        public IActionResult product_details(int id)
        {
            var product = hc.products.Include(e => e.category).FirstOrDefault(e => e.Id == id);
            return View(product);
        }
    }
}
