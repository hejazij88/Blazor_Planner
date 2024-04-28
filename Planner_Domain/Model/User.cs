using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planner.Domain.Model;

namespace Planner_Domain.Model
{
    [Table("User")]
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string HashPassword { get; set; }


        #region Relation

        [InverseProperty(nameof(Category.User))] public virtual List<Category> Categories { get; set; } = new List<Category>();
        [InverseProperty(nameof(DatePlan.User))] public virtual List<DatePlan> DatePlans { get; set; } = new List<DatePlan>();

        #endregion
    }
}
