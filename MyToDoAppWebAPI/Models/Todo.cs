using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyToDoAppWebAPI.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
        public int user { get; set; }
    }
}