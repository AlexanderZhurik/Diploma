using Microsoft.AspNetCore.Mvc;
using Diploma.DB;
using Microsoft.AspNetCore.Authorization;
namespace Diploma.WebApi.Controllers
{
    [ApiController]
    public class ItemController : ControllerBase
    {
        [HttpGet]
        [Route("Item")]
        public string[] GetAllItems()
        {
            using (var db = new Context())
            {
                var query = db.Items.ToArray();
                var output = new List<string>();
                foreach (var item in query)
                {
                    output.Add(item.Name);
                }
                return output.ToArray();
            }
        }
        [HttpPost]
        [Authorize]
        [Route("AddItem")]
        public void AddAnItem(string name, int price, string description, string category)
        {

            using (var db = new Context())
                {
                var CategoryObj = db.Categories.Where(n => n.Name == category).FirstOrDefault();
                var item = new Item { Name = name, Price = price, Description = description, Category = CategoryObj};
                db.Items.Add(item);
                db.SaveChanges();
                }
            
        }
        [Route("Search")]
        [HttpGet]
        
        public Item GetItemByName(string name)
        {
            using (var db = new Context())
            {
                var query = db.Items.Where(n => n.Name == name).FirstOrDefault();
                return query;
            }
        }
        
        [Route("DeleteItem")]
        [HttpDelete]
        public void DeleteAnItem(string name)
        {
            using var db = new Context();
            if (GetItemByName(name) != null)
            {
                db.Items.Remove(GetItemByName(name));
                db.SaveChanges();

            }
        }

        [Route("Categories")]
        [HttpGet]
        public Category[] GetAllCategories()
        {
            using (var db = new Context())
            {
                return db.Categories.ToArray();

            }
        }

        [Route("CategoryByName")]
        [HttpGet]
        public Category GetCategoryByName(string name)
        {
            using (var db = new Context())
            {
                return db.Categories.Where(c => c.Name == name).FirstOrDefault();
            }
        }
        //[Route("ItemsInCategory")]
        //[HttpGet]
        //public Item[] GetItemsByCategory(Category category)
        //{
        //    return category.Items.ToArray();
        //}
        [Route("ItemsInCategory")]
        [HttpGet]
        public Item[] GetItemsByCategory(string category)
        {
            return GetCategoryByName(category).Items.ToArray();
        }
    }
}