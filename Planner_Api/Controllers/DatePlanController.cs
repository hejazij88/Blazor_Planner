using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Domain.Model;
using Planner_Business;
using Planner_Domain.Model;

namespace Planner.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DatePlanController : ControllerBase
    {
        private PlannerContext _context = new PlannerContext();
        [HttpGet]
        public IActionResult LoadDatePlan(Guid userId)
        {
            try
            {
                var result = _context.DatePlans.Include(plan=>plan.Activities)
                    .ThenInclude(activity =>activity.ToDo)
                    .Where(plan => plan.UserId == userId).ToList();
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateDatePlan(DatePlan datePlan)
        {
            try
            {
                _context.Update(datePlan);
                _context.SaveChanges();
                return Ok(datePlan);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public IActionResult DeleteDatePlan(DatePlan datePlan)
        {
            try
            {
                _context.Remove(datePlan);
                _context.SaveChanges();
                return Ok(datePlan);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


    }
}
