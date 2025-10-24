using first_mvc.Models;

namespace first_mvc.Repositories
{
    public class CustomersRepository:ICustomerRepo
    {
        HandoraContext hc ;
        public CustomersRepository() {
            hc = new HandoraContext();
        }
        public List<Customers> get_all()
        {
            return hc.customers.ToList();
        }
        public Customers? find_id(int id)
        {
            return hc.customers.Find(id);
        }
        public void add(Customers cust) {
        hc.customers.Add(cust);
        }
        public void update(Customers cust) { 
        hc.customers.Update(cust);
        }
        public void delete(Customers cust) {
           
            hc.customers.Remove(cust);
           
        }
        public void save() {
        hc.SaveChanges();
        }

    }
}
