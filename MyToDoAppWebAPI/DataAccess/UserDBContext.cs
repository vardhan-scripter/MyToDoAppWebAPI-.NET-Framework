using MyToDoAppWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyToDoAppWebAPI.DataAccess
{
    public class UserDBContext : DbContext
    {
        public UserDBContext() : base("User")
        {

        }
        public DbSet<User> UserList { get; set; }
    }
}