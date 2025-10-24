using first_mvc.Models;

namespace first_mvc.Repositories
{
    public interface IProductRepo
    {
        public List<Products> get_all();

        public Products? find_id(int id);

        public Products? find_withcat(int id);

        public void add(Products pro);

        public void update(Products pro);

        public void delete(Products pro);

        public void save();
        
    }
}
