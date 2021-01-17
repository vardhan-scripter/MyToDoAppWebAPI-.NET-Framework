using System;
using System.Collections.Generic;
using System.Web.Http;
using MyToDoAppWebAPI.DataAccess;
using MyToDoAppWebAPI.Models;
using System.Linq;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private UserDBContext Context = new UserDBContext();

        [HttpPost]
        public Dictionary<string, object> UserRegistration(User user)
        {
            if (user.Name == null ||
                user.Name.Length == 0 ||
                user.Email == null ||
                user.Email.Length == 0 ||
                user.Password == null ||
                user.Password.Length == 0)
            {
                return new Dictionary<string, object>(){
                    { "success", false} ,
                    { "message", "Please enter details correctly" }
                };
            }
            bool result = false;
            using (var Context = new UserDBContext())
            {
                Context.UserList.Add(user);
                Context.SaveChanges();
                result = true;
            }
            if (result)
            {
                return new Dictionary<string, object>() {
                    { "success", true }
                };
            }
            return new Dictionary<string, object>() {
                { "success", false } ,
                { "message", "Failed to add user" }
            };
        }

        public Dictionary<string, object> GetUserDetails(int id)
        {
            var data = Context.UserList.Where(x => x.Id == id).FirstOrDefault();
            if (data == null)
            {
                return new Dictionary<string, object>() {
                    { "success", false } ,
                    { "message", "No user present with that id" }
                };
            }
            else
            {
                return new Dictionary<string, object>() {
                    { "success", true },
                    { "data", data }
                };
            }
        }
    }
}