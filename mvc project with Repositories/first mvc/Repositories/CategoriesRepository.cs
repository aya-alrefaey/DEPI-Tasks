using first_mvc.Models;

namespace first_mvc.Repositories
{
    public class CategoriesRepository:ICategoriesRepo
    {
        HandoraContext hc;
        public CategoriesRepository()
        {
            hc = new HandoraContext();
        }
        public List<Categories> get_all()
        {
            return hc.categories.ToList();
        }
        public Categories? find_id(int id)
        {
            return hc.categories.Find(id);
        }
        public void add(Categories cat)
        {
            hc.categories.Add(cat);
        }
        public void update(Categories cat)
        {
            hc.categories.Update(cat);
        }
        public void delete(Categories cat)
        {
            
                hc.categories.Remove(cat);
           


        }
        public void save()
        {
            hc.SaveChanges();
        }

    }
}
