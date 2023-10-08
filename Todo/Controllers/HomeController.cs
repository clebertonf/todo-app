using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private AppDbContext AppDbContext { get; set; }
        public HomeController([FromServices] AppDbContext DbContext)
        {
            AppDbContext = DbContext;
        }

        [HttpGet]
        [Route("/")]
        public List<TodoModel> Get()
        {
            return AppDbContext.Todos.ToList();
        }

        [HttpGet]
        [Route("/{id}")]
        public TodoModel GetById([FromRoute] int id)
        {
            return AppDbContext.Todos.FirstOrDefault(x => x.Id.Equals(id));
        }

        [HttpPost]
        [Route("/create-todo")]
        public TodoModel Post([FromBody] TodoModel todoModel)
        {
            AppDbContext.Todos.Add(todoModel);
            AppDbContext.SaveChanges();

            return todoModel;
        }

        [HttpPut]
        [Route("/update-todo/{id}")]
        public TodoModel Put([FromBody] TodoModel todoModel, [FromRoute] int id)
        {
           var model = AppDbContext.Todos.FirstOrDefault(x => x.Id.Equals(id));
            if (model == null) return todoModel;

            model.Title = todoModel.Title;
            model.Done = todoModel.Done;

            AppDbContext.Todos.Update(model);
            AppDbContext.SaveChanges();

            return todoModel;
        }

        [HttpDelete]
        [Route("/delete/{id}")]
        public TodoModel Delete([FromRoute] int id)
        {
            var model = AppDbContext.Todos.FirstOrDefault(x => x.Id.Equals(id));
            if (model == null) return null;

            AppDbContext.Todos.Remove(model);
            AppDbContext.SaveChanges();

            return model;
        }
    }
}
