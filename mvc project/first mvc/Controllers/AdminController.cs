using first_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace first_mvc.Controllers
{
   
    public class AdminController : Controller
    {
        HandoraContext hc = new HandoraContext();
        public IActionResult Index()
        {
            return View();
        }
        [Route("/admin/customer/delete/{id}")]
        public IActionResult DeletCustomer(int id)
        {
            var cust = hc.customers.FirstOrDefault(e => e.Id == id);
            if (cust == null)
            {
                return NotFound(); 
            }
            hc.Remove(cust);
            hc.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
        [Route("/admin/products/delete/{id}")]
        public IActionResult DeletProduct(int id)
        {
            var pro = hc.products.FirstOrDefault(e => e.Id == id);
            if (pro == null)
            {
                return NotFound();
            }
            hc.Remove(pro);
            hc.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
    }
}
