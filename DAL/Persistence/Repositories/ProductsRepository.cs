using DAL.Domain;
using DAL.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence.Repositories
{
    #region Interface
    public interface IProductsRepository : IBaseRepository<Products>
    {
    }
    #endregion
    public class ProductsRepository : BaseRepository<Products>, IProductsRepository
    {
        public ProductsRepository(InventoryContext context) : base(context)
        {

        }
    }
}
