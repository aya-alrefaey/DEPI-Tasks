using first_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace first_mvc.Repositories
{
    public class ProductsRepository:IProductRepo
    {
        HandoraContext hc;
        public ProductsRepository()
        {
            hc = new HandoraContext();
        }
        public List<Products> get_all()
        {
            return hc.products.ToList();
        }
        public Products? find_id(int id)
        {
            return hc.products.Find(id);
        }
        public Products? find_withcat(int id)
        {
            return hc.products
                      .Include(p => p.category) 
                      .FirstOrDefault(p => p.Id == id);
        }
        public void add(Products pro)
        {
            hc.products.Add(pro);
        }
        public void update(Products pro)
        {
            hc.products.Update(pro);
        }
        public void delete(Products pro)
        {
           
                hc.products.Remove(pro);
           


        }
        public void save()
        {
            hc.SaveChanges();
        }

    }
}
