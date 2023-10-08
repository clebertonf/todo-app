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
        [Route("/todos")]
        public List<TodoModel> Get()
        {
            return AppDbContext.Todos.ToList();
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
