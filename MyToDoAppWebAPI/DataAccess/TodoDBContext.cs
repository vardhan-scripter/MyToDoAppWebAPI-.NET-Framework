using MyToDoAppWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyToDoAppWebAPI.DataAccess
{
    public class TodoDBContext : DbContext
    {
        public TodoDBContext() : base("Todo")
        {

        }
        public DbSet<Todo> TodoList { get; set; }
    }
}