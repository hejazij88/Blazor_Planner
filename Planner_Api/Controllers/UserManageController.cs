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
    public class UserManageController : ControllerBase
    {
        private PlannerContext _context = new PlannerContext();

        [HttpPost]
        public IActionResult Register(RegisterVM vm)
        {
            try
            {
                Planner_Domain.Model.User user = new User
                {
                    UserName = vm.UserName,
                    HashPassword = HashPass.GetSha256(vm.Password)
                };
                _context.Update(user);
                if (_context.Users.Any(user1 => user1.UserName == user.UserName))
                    return BadRequest("Specified Company Name is duplicate !!!");

                _context.SaveChanges();
                return Ok(user);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpPost]
        public IActionResult LogIn(LogInVM vm)
        {
            try
            {
                Planner_Domain.Model.User user = new User
                {
                    UserName = vm.UserName,
                    HashPassword = HashPass.GetSha256(vm.Password)
                };

                var result = _context.Users.FirstOrDefault(user1 =>
                    user1.UserName == user.UserName && user1.HashPassword == user.HashPassword);

                if (result == null)
                    return BadRequest("Does Not Exist User");


                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
