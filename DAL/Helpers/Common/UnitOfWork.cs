using DAL.Domain;
using DAL.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers.Common
{
    #region Interface
    public interface IUnitOfWork
    {
        ICategoriesRepository CategoriesRepository { get; }
        IProductsRepository ProductsRepository { get; }
        void Save();
    }
    #endregion
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoriesRepository _categoriesRepository;
        private IProductsRepository _productsRepository;
        private InventoryContext _context;
        public UnitOfWork(InventoryContext context)
        {
            _context = context;
        }
        public ICategoriesRepository CategoriesRepository
        {
            get
            {
                if (_categoriesRepository == null)
                {
                    _categoriesRepository = new CategoriesRepository(_context);
                }

                return _categoriesRepository;
            }
        }

        public IProductsRepository ProductsRepository
        {
            get
            {
                if (_productsRepository == null)
                {
                    _productsRepository = new ProductsRepository(_context);
                }

                return _productsRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
