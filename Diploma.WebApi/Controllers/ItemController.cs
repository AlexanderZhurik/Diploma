using Microsoft.AspNetCore.Mvc;
using Diploma.DB;
using Microsoft.AspNetCore.Authorization;
namespace Diploma.WebApi.Controllers
{
    [ApiController]
    public class ItemController : ControllerBase
    {
        private Context db = new Context();

        [HttpGet]
        [Route("GetItems")]
        public Item[] GetAllItems()
        {
            return db.Items.ToArray();
        }





        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("AddItem")]
        public void AddAnItem(string name, int price, string description, string category, string image)
        {

           
                var CategoryObj = db.Categories.Where(n => n.Name == category).FirstOrDefault();
                var item = new Item { Name = name, Price = price, Description = description, Category = CategoryObj, ImageRef = image};
                db.Items.Add(item);
                db.SaveChanges();
                
            
        }





        [Route("Search")]
        [HttpGet]
        public Item[] GetItemsByNamePart(string name)
        {
            
                var query = db.Items.Where(n => n.Name.Contains(name)).ToArray();
                return query;
            
        }




        [HttpGet]
        [Route("ItemByName")]
        public Item GetItemByName(string name)
        {
            
                var query = db.Items.Where(n => n.Name == name).FirstOrDefault();
                return query;
            
        }




        [Route("DeleteItem")]
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public void DeleteAnItem(string name)
        {
            
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
                return db.Categories.ToArray();
        }





        [Route("CategoryByName")]
        [HttpGet]
        public Category GetCategoryByName(string name)
        {  
                return db.Categories.Where(c => c.Name == name).FirstOrDefault();
        }
        


        [Route("ItemsInCategory")]
        [HttpGet]
        public Item[] GetItemsByCategory(string category)
        {
            return db.Items.Where(c => c.Category.Name == category).ToArray();
        }





        [Authorize(Roles = "Administrator")]
        [Route("AddCategory")]
        [HttpPost]
        public void AddACategory(string name)
        {
            db.Categories.Add(new Category { Name = name }); 
            db.SaveChanges();
        }




        [Authorize(Roles = "Administrator")]
        [Route("DeleteCategory")]
        [HttpDelete]
        public void DeleteACategory(string name)
        {
            if (GetCategoryByName(name) != null)
            {
                var itemsInCategory = db.Items.Where(c => c.Category == GetCategoryByName(name)).ToArray();
                db.Items.RemoveRange(itemsInCategory);
                db.Categories.Remove(GetCategoryByName(name));
                db.SaveChanges();
            }
        }
    }
}