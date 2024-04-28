using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planner.Domain.ViewModel;
using Planner_Business;
using Planner_Domain.Model;
using ProjectName;

namespace Planner.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private PlannerContext _context=new PlannerContext();
        [HttpGet]
        public IActionResult LoadCategory(Guid userId)
        {
            try
            {
                var result = _context.Categories.Where(category => category.UserId == userId).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            try
            {
                _context.Update(category);
                if (_context.Categories.Any(c =>  c.Title == category.Title  && c.UserId==category.UserId))
                    return BadRequest("Specified Company Name is duplicate !!!");

                _context.SaveChanges();
                return Ok(category);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public IActionResult DeleteCategory(Category category)
        {
            try
            {
                _context.Remove(category);
                _context.SaveChanges();
                return Ok(category);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
