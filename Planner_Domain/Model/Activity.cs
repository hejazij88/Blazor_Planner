using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Planner.Domain.Model;

namespace Planner_Domain.Model
{
    [Table("Activity")]

    public class Activity
    {

        public Guid Id { get; set; }
        public Guid ToDoId { get; set; }
        public Guid? DateId { get; set; }
        public bool IsDo { get; set; }
        [ForeignKey(nameof(ToDoId))]public virtual ToDo? ToDo { get; set; }
        [ForeignKey(nameof(DateId))] public virtual DatePlan? DatePlan { get; set; }

    }
}
