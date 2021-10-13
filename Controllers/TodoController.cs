using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MeuTodo.Data;
using MeuTodo.Models;
using MeuTodo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuTodo.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("todos/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDataContext context,
            [FromRoute] int id)
        {
            var todo = await context
                .Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return todo == null
                ? NotFound()
                : Ok(todo);
        }
        
        [HttpPost("todos")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDataContext context,
            [FromBody] CreateTodoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = new Todo
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };

            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created($"v1/todos/{todo.Id}", todo);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
        [HttpPut("todos/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDataContext context,
            [FromBody] CreateTodoViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();

            try
            {
                todo.Title = model.Title;
                
                context.Todos.Update(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("todos/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDataContext context,
            [FromRoute] int id)
        {
            var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
    }
}