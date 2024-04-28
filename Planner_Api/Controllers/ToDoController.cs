using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner_Business;
using Planner_Domain.Model;

namespace Planner.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private PlannerContext _context = new PlannerContext();
        [HttpGet]
        public IActionResult LoadToDo(Guid categoryId)
        {
            try
            {
                var result = _context.ToDoS.Where(todo => todo.CategoryId == categoryId).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet]
        public IActionResult LoadToDoByUserId(Guid userId)
        {
            try
            {
                var result = _context.ToDoS.Include(todo => todo.Category)
                    .Where(todo => todo.Category.UserId == userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateToDo(ToDo toDo)
        {
            try
            {
                _context.ToDoS.Update(toDo);
                if (_context.ToDoS.Any(t => t.Title == toDo.Title && t.CategoryId == toDo.CategoryId && t.Category.UserId==toDo.Category.UserId))
                    return BadRequest("Specified is duplicate !!!");

                _context.SaveChanges();
                return Ok(toDo);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public IActionResult DeleteToDo(ToDo toDo)
        {
            try
            {
                _context.Remove(toDo);
                _context.SaveChanges();
                return Ok(toDo);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
