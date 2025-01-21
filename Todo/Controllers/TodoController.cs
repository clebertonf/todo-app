using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private AppDbContext AppDbContext { get; }
        public TodoController(AppDbContext dbContext)
        {
            AppDbContext = dbContext;
        }

        [HttpGet]
        [Route("/todos")]
        public async Task<IActionResult> Get() 
            => Ok(await AppDbContext.Todos.ToListAsync());

        [HttpGet]
        [Route("/todo/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var todo = await AppDbContext.Todos.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (todo is null) return BadRequest();

            return Ok(todo);
        }

        [HttpPost]
        [Route("/todo")]
        public async Task<IActionResult> Post([FromBody] TodoModel todoModel)
        {
            await AppDbContext.Todos.AddAsync(todoModel);
            await AppDbContext.SaveChangesAsync();

            return Created($"/{todoModel.Id}", todoModel);
        }

        [HttpPut]
        [Route("/todo/{id}")]
        public async Task<IActionResult> Put([FromBody] TodoModel todoModel, [FromRoute] int id)
        {
           var model = await AppDbContext.Todos.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (model is null) return NotFound();

            model.TodoTitle = todoModel.TodoTitle;
            model.IsDone = todoModel.IsDone;

            AppDbContext.Todos.Update(model);
            await AppDbContext.SaveChangesAsync();

            return Ok(todoModel);
        }

        [HttpDelete]
        [Route("/todo/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var model = await AppDbContext.Todos.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (model is null) return NotFound();

            AppDbContext.Todos.Remove(model);
            await AppDbContext.SaveChangesAsync();

            return Ok(model);
        }
    }
}
