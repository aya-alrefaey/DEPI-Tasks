namespace first_mvc.Models
{
    public class ProductCreateViewModel
    {
        public Products product { set; get; }
        public List<Categories> categories { set; get; } = new List<Categories>();
    }
}
