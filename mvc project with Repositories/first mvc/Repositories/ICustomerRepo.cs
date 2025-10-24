using first_mvc.Models;

namespace first_mvc.Repositories
{
    public interface ICustomerRepo
    {
        public List<Customers> get_all();
        
        public Customers? find_id(int id);
      
        public void add(Customers cust);
       
        public void update(Customers cust);
        public void delete(Customers cust);
       
        public void save();
        
    }
}
