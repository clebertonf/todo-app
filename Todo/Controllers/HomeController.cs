using Microsoft.AspNetCore.Mvc;
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
    }
}
