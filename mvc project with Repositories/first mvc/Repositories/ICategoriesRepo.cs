using first_mvc.Models;

namespace first_mvc.Repositories
{
    public interface ICategoriesRepo
    {
        public List<Categories> get_all();
      
        public Categories? find_id(int id);
        
        public void add(Categories cat);
        
        public void update(Categories cat);
       
        public void delete(Categories cat);
      
        public void save();
       
    }
}
