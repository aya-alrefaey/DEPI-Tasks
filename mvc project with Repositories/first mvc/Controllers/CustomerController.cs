using first_mvc.Models;
using first_mvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace first_mvc.Controllers
{
    public class CustomerController : Controller
    {
        //CustomersRepository custrepo;
        ICustomerRepo custrepo;
        public CustomerController(ICustomerRepo cust)
        {
            custrepo = cust;
        }
        [Route("/admin/customers")]
        public IActionResult Index()
        {

            var customers = custrepo.get_all();
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
            //hc.Add(cust);
            //hc.SaveChanges();
            custrepo.add(cust);
            custrepo.save();
            
            ViewBag.msg = "Customer Added Successfully";
            return View("addcustomer");
            //return RedirectToAction();
        }

        [HttpGet]
        [Route("/admin/customer/edit/{id}")]
        public IActionResult edit_Customer(int id)
        {
            //var cust = hc.customers.FirstOrDefault(e => e.Id == id);
            var cust = custrepo.find_id(id);
            if (cust == null)
            {
                return NotFound();
            }
            return View(cust);
        }

        [HttpPost]
        [Route("/admin/customer/edit/{id}")]
        public IActionResult edit_Customer(int id, Customers updated)
        {
            //var cust = hc.customers.FirstOrDefault(e => e.Id == id);
            var cust = custrepo.find_id(id);
            if (cust == null)
            {
                return NotFound();
            }

         
            cust.Name = updated.Name;
            cust.Email = updated.Email;
            cust.Phone = updated.Phone;
            cust.Gender = updated.Gender;
            if (!string.IsNullOrEmpty(updated.Password))
            {
                cust.Password = updated.Password; 
            }

            //hc.Update(cust);
            //hc.SaveChanges();
            custrepo.update(cust);
            custrepo.save();

            //ViewBag.msg = "Customer Edited Successfully";
            return RedirectToAction("index");
        }
        [Route("/admin/customer/details/index/{id}")]
        public IActionResult cust_details(int id)
        {
            //var customer = hc.customers.FirstOrDefault(e => e.Id == id);
            var customer = custrepo.find_id(id);
            return View(customer);
        }

        [Route("/admin/customer/delete/{id}")]
        public IActionResult DeletCustomer(int id)
        {
            //var cust = hc.customers.FirstOrDefault(e => e.Id == id);
            var cust = custrepo.find_id(id);
            if (cust == null)
            {
                return NotFound(); 
            }
            //hc.Remove(cust);
            //hc.SaveChanges();
            custrepo.delete(cust);
            custrepo.save();
            return RedirectToAction("Index", "Customer");
        }

    }
}
