using first_mvc.Models;
using first_mvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace first_mvc.Controllers
{
    public class DetailsController1 : Controller
    {
        ProductsRepository productrepo;
        CustomersRepository custrepo;
        public DetailsController1()
        {
            productrepo = new ProductsRepository();
            custrepo = new CustomersRepository();
        }
        //[Route("/admin/customer/details/index/{id}")]
        //public IActionResult Index(int id)
        //{
        //    //var customer = hc.customers.FirstOrDefault(e => e.Id == id);
        //    var customer = custrepo.find_id(id);
        //    return View(customer);
        //}
        //[Route("/admin/products/details/product_details/{id}")]

        //public IActionResult product_details(int id)
        //{
        //    //var product = hc.products.Include(e => e.category).FirstOrDefault(e => e.Id == id);
        //    var product =productrepo.find_id(id);
        //    return View(product);
        //}
    }
}
