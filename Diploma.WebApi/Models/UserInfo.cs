namespace Diploma.WebApi.Models
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        
    }
    public class StaticUsers
    {
        public UserInfo Admin = new UserInfo() { UserName = "Admin", UserEmail = "example@gmail.com", Password = "12345678" };
           
       
    }
}
