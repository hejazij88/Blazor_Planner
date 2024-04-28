using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Domain.Model;
using Planner.Domain.ViewModel;
using Planner_Business;
using Planner_Domain.Model;
using System.Linq;

namespace Planner.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private PlannerContext _context = new PlannerContext();
        [HttpGet]
        public IActionResult LoadActivityByDateId(Guid DatePlanId)
        {
            try
            {

                var result = _context.Activities.
                    Include(activity => activity.ToDo)
                    .Where(activity => activity.DateId == DatePlanId).ToList();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateActivity(List<Activity> activities)
        {
            try
            {
                _context.UpdateRange(activities);

                _context.SaveChanges();
                return Ok(activities);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpGet]
        public IActionResult DayActivity(Guid userId)
        {
            try
            {
                var activityList = _context.Activities.Include(activity => activity.ToDo)
                     .Where(activity =>
                         activity.DatePlan.UserId == userId && activity.DatePlan.DateTime.Value == DateTime.Today).ToList();

                return Ok(activityList);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpGet]
        public IActionResult MountActivity(Guid userId)
        {
            try
            {
                var today = DateTime.Now;
                var lastMonth = today.AddMonths(-1);
                var chartData = _context.Activities
                    .Where(activity => activity.IsDo==true && activity.DatePlan.DateTime>=lastMonth)
                    .GroupBy(activity => activity.ToDoId)
                    .Select(group => new ChartVM
                    {
                        ToDoTitle = group.First().ToDo.Title,
                        CountDo = group.Count()
                    })
                    .ToList();

                return Ok(chartData);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public IActionResult MountNotActivity(Guid userId)
        {
            try
            {
                var today = DateTime.Now;
                var lastMonth = today.AddMonths(-1);
                var chartData = _context.Activities
                    .Where(activity => activity.IsDo == false && activity.DatePlan.DateTime >= lastMonth)
                    .GroupBy(activity => activity.ToDoId)
                    .Select(group => new ChartVM
                    {
                        ToDoTitle = group.First().ToDo.Title,
                        CountDo = group.Count()
                    })
                    .ToList();

                return Ok(chartData);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }



        [HttpGet]
        public IActionResult AllTimeActivity(Guid userId)
        {
            try
            {
                var today = DateTime.Now;
                var chartData = _context.Activities
                    .Where(activity => activity.IsDo == true)
                    .GroupBy(activity => activity.ToDoId)
                    .Select(group => new ChartVM
                    {
                        ToDoTitle = group.First().ToDo.Title,
                        CountDo = group.Count()
                    })
                    .ToList();

                return Ok(chartData);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
