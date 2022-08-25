using Microsoft.EntityFrameworkCore;

namespace CV_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<CV> CVs { get; set; }

    }
}
