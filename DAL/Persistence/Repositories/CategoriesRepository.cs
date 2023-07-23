using DAL.Domain;
using DAL.Domain.Models;

namespace DAL.Persistence.Repositories
{
    #region Interface
    public interface ICategoriesRepository : IBaseRepository<Categories>
    {
    }
    #endregion
    #region Implementation
    public class CategoriesRepository  : BaseRepository<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(InventoryContext context) : base(context)
        {
            
        }
    }
    #endregion
}
