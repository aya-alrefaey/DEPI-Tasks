using Microsoft.EntityFrameworkCore;

namespace first_mvc.Models
{
    public class HandoraContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer("server=DESKTOP-VBP7A1N;database=Handora;trusted_connection=true;trustservercertificate=true");
        }
        public DbSet<Products> products { get; set; }
        public DbSet<Customers> customers { get; set; }
        public DbSet<Categories> categories { get; set; }
    }
}
