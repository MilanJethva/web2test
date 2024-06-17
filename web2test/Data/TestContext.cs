        using Microsoft.EntityFrameworkCore;
using web2test.Models.Domain;
namespace web2test.Data
{
    public class TestContext:DbContext
    {
        public TestContext(DbContextOptions<TestContext> dbContextOptions):base(dbContextOptions)
        {
        }
        
        public DbSet<Diffculty> Diffculty { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }



        

    }
}
