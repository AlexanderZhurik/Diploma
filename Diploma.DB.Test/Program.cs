using System;
using Diploma.DB;

namespace DbTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new Context();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            var c1 = new Category { Name = "Snakes" };
            var c2 = new Category { Name = "Geckos" };
            var itm1 = new Item { Name = "snake1", Category = c1, Price = 100, Description = "First snake" };
            var itm2 = new Item { Name = "snake2", Category = c1, Price = 200, Description = "Senond snake" };
            var itm3 = new Item { Name = "gecko1", Category = c2, Price = 444, Description = "First gecko" };
            db.Categories.AddRange(new List<Category> { c1, c2 });
            db.Items.AddRange(new List<Item> { itm1, itm2, itm3 });
            db.SaveChanges();

            var query = db.Items.ToArray();
            foreach (var item in query)
            {
                Console.WriteLine($"Name: {item.Name}, Price: {item.Price}, Category: {item.Category.Name}, Descripition: {item.Description}");
            }
            Console.WriteLine("----------------------------");
            var categoryQuery = Console.ReadLine();
            query = db.Items.Where(i => i.Category.Name == categoryQuery).ToArray();
            Console.WriteLine($"{categoryQuery}:");
            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }

        }
    }
}