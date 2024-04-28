using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Planner_Domain.Model;
using System.Reflection;
using Planner.Domain.Model;

namespace Planner_Business
{
    public class PlannerContext:DbContext
    {
        private readonly IConfiguration _configuration;

        public PlannerContext()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.Business.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }
        public  PlannerContext (DbContextOptions<PlannerContext> options):base(options){}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("Default");
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User>Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ToDo> ToDoS { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<DatePlan> DatePlans { get; set; }
    }
}
