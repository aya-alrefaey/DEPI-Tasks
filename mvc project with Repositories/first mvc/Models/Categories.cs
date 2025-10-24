using System.Collections.Generic;

namespace first_mvc.Models
{
    public class Categories
    {
        public int id { set; get; }
        public string Name { get; set; }
        //public string Description { get; set; }

        public ICollection<Products> products { get; } = new List<Products>();
    }
}
