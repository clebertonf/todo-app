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
        public IActionResult Get() 
            => Ok(AppDbContext.Todos.ToList());

        [HttpGet]
        [Route("/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var todo = AppDbContext.Todos.FirstOrDefault(x => x.Id.Equals(id));
            if (todo is null) return BadRequest();

            return Ok(todo);
        }

        [HttpPost]
        [Route("/create-todo")]
        public IActionResult Post([FromBody] TodoModel todoModel)
        {
            AppDbContext.Todos.Add(todoModel);
            AppDbContext.SaveChanges();

            return Created($"/{todoModel.Id}", todoModel);
        }

        [HttpPut]
        [Route("/update-todo/{id}")]
        public IActionResult Put([FromBody] TodoModel todoModel, [FromRoute] int id)
        {
           var model = AppDbContext.Todos.FirstOrDefault(x => x.Id.Equals(id));
            if (model == null) return NotFound();

            model.Title = todoModel.Title;
            model.Done = todoModel.Done;

            AppDbContext.Todos.Update(model);
            AppDbContext.SaveChanges();

            return Ok(todoModel);
        }

        [HttpDelete]
        [Route("/delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var model = AppDbContext.Todos.FirstOrDefault(x => x.Id.Equals(id));
            if (model == null) return NotFound();

            AppDbContext.Todos.Remove(model);
            AppDbContext.SaveChanges();

            return Ok(model);
        }
    }
}
