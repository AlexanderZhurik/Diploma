using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Diploma.DB
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }
        public Category()
        {
            Items = new List<Item>();
        }
        
    }
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string? ImageRef { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        

        public Item()
        {

        }
        
    }
    public class Context : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public Context()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=itemscategories;Trusted_Connection=True;");
        }
    }
}