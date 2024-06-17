using Microsoft.EntityFrameworkCore;
using web2test.Data;
using web2test.Models.Domain;

namespace web2test.Models.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private TestContext testcontext;

        public SQLRegionRepository(TestContext testcontext)
        {
            this.testcontext = testcontext;
        }
            public async Task<List<Region>> GetAllAsync()
            {
                return await testcontext.Regions.ToListAsync();
            }
                
       
    }
} 
