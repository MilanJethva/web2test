using web2test.Models.Domain;

namespace web2test.Models.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
    }
}
