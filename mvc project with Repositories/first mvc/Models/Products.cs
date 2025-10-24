using System.ComponentModel.DataAnnotations.Schema;

namespace first_mvc.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
 public int categoryId { get; set; }
        public Categories category { get; set; }

    }
}
