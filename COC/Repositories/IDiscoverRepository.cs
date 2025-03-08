
using COC.ModelDB.QUDB;
using COC.Models;
namespace COC.Repositories
{
    public interface IDiscoverRepository
    {
        Task<IEnumerable<Discover>> GetAll();
        Task<Discover> GetById(int id);
        Task Add (Discover discover);
        Task Update(Discover discover);
        Task Delete(int id);
    }
}
