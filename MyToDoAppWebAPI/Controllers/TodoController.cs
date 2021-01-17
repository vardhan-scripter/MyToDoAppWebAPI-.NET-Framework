using MyToDoAppWebAPI.DataAccess;
using MyToDoAppWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyToDoAppWebAPI.Controllers
{
    public class TodoController : ApiController
    {
        private TodoDBContext Context = new TodoDBContext();
        private UserDBContext userContext = new UserDBContext();

        // GET api/<controller>
        [HttpGet]
        public Dictionary<string, object> GetTodoDetails()
        {
            var data = Context.TodoList.ToList();
            if (data.Count == 0)
            {
                return new Dictionary<string, object>(){
                    { "success", true} ,
                    { "message", "No todos available" }
                };
            }
            else
            {
                return new Dictionary<string, object>(){
                    { "success", true} ,
                    { "data", data }
                };
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        public Dictionary<string, object> GetTodoDetails(int id)
        {
            var data = Context.TodoList.Where(x => x.Id == id).FirstOrDefault();
            if (data == null)
            {
                return new Dictionary<string, object>(){
                    { "success", true} ,
                    { "message", "No todo available" }
                };
            }
            else
            {
                return new Dictionary<string, object>(){
                    { "success", true} ,
                    { "data", data }
                };
            }
        }

        // POST api/<controller>
        [HttpPost]
        public Dictionary<string, object> AddNewTodo(Todo todo)
        {
            int count = userContext.UserList.Where(x => x.Id == todo.user).Count();
            if (todo.Name == null ||
                todo.Name.Length == 0 || count <= 0)
            {
                return new Dictionary<string, object>(){
                    { "success", false} ,
                    { "message", "Please enter details correctly" }
                };
            }
            bool result = false;
            using (var Context = new TodoDBContext())
            {
                Context.TodoList.Add(todo);
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
                { "message", "Failed to add todo" }
            };
        }

        // PUT api/<controller>/5
        [HttpPut]
        public Dictionary<string, object> UpdateTodo(Todo updatedTodo)
        {
            int UserExists = userContext.UserList.Where(x => x.Id == updatedTodo.user).Count();
            int TodoExists = Context.TodoList.Where(x => x.Id == updatedTodo.Id).Count();

            if (updatedTodo.Name == null ||
                updatedTodo.Name.Length == 0 || UserExists <= 0 || TodoExists <= 0)
            {
                return new Dictionary<string, object>(){
                    { "success", false} ,
                    { "message", "Please enter details correctly" }
                };
            }
            using (var Context = new TodoDBContext())
            {
                if (Context.TodoList.Where(x => x.Id == updatedTodo.Id).Count() == 1)
                {
                    var todo = Context.TodoList.Where(x => x.Id == updatedTodo.Id).FirstOrDefault();
                    todo.Name = updatedTodo.Name;
                    Context.SaveChanges();
                    return new Dictionary<string, object>() {
                        { "success", true } ,
                        { "message", "Todo updated successfully" }
                    };
                }
                else
                {
                    return new Dictionary<string, object>() {
                        { "success", false } ,
                        { "message", "No todo exists with that Id" }
                    };
                }
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public Dictionary<string, object> DeleteToDo(int id)
        {
            using (var Context = new TodoDBContext())
            {
                if(Context.TodoList.Where(x => x.Id == id).Count() == 1)
                {
                    var todo = Context.TodoList.Where(x => x.Id == id).FirstOrDefault();
                    Context.TodoList.Remove(todo);
                    Context.SaveChanges();
                    return new Dictionary<string, object>() {
                        { "success", true } ,
                        { "message", "Todo removed successfully" }
                    };
                }
                else
                {
                    return new Dictionary<string, object>() {
                        { "success", false } ,
                        { "message", "No todo exists with that Id" }
                    };
                }
            }
        }

        // PATCH api/<controller>/5
        [HttpPatch]
        public Dictionary<string, object> CompleteToDo(int id)
        {
            using (var Context = new TodoDBContext())
            {
                if (Context.TodoList.Where(x => x.Id == id).Count() == 1)
                {
                    var todo = Context.TodoList.Where(x => x.Id == id).FirstOrDefault();
                    todo.Done = !todo.Done;
                    Context.SaveChanges();
                    return new Dictionary<string, object>() {
                        { "success", true } ,
                        { "message", "Todo updated successfully" }
                    };
                }
                else
                {
                    return new Dictionary<string, object>() {
                        { "success", false } ,
                        { "message", "No todo exists with that Id" }
                    };
                }
            }
        }
    }
}