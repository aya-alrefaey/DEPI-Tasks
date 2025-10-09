using first_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace first_mvc.Controllers
{
    public class CustomerController : Controller
    {
        HandoraContext hc = new HandoraContext();
        [Route("/admin/customers")]
        public IActionResult Index()
        {

            var customers = hc.customers.ToList();
            return View(customers);
        }
        [HttpGet]
        [Route("/admin/customers/add")]
        public IActionResult Add_Customer()
        {
         
            return View("addcustomer");
        }
        [HttpPost]
        [Route("/admin/customers/add")]
        public IActionResult Add_Customer(Customers cust)
        {
            cust.Creadted_at = DateTime.Now;
            hc.Add(cust);
            hc.SaveChanges();
            //return View("addcustomer");
            return RedirectToAction("Index", "Customer");
        }
    }
}
