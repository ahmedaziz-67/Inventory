using AutoMapper;
using DAL.Domain;
using DAL.Domain.Models;
using DAL.Helpers.Common;
using DAL.Persistence.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    #region Interface
    public interface IProductsServices
    {
        Task<GetProductsDto> GetAllProducts(PaginationParameters paginationParameters);
        Task<BaseResponse> CreateProduct(CreateProductDto createProductDto);
        Task<BaseResponse> UpdateProduct(CreateProductDto product);
        Task<BaseResponse> DeleteProduct(Guid Id);
    }
    #endregion
    #region Implementation
    public class ProductsServices : IProductsServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly InventoryContext _context;
        public ProductsServices(IMapper mapper, IUnitOfWork unitOfWork, InventoryContext context)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<BaseResponse> CreateProduct(CreateProductDto createProductDto)
        {
            try
            {
                var productEntry = _mapper.Map<CreateProductDto, Products>(createProductDto);
                await _unitOfWork.ProductsRepository.AddAsync(productEntry);
                _unitOfWork.Save();
                return new BaseResponse { StatusCode = "200" };
            }
            catch (Exception ex)
            {
                return new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = ex.ToString() } } };
            }
        }

        public Task<BaseResponse> DeleteProduct(Guid Id)
        {
            var product = _context.Products.Find(Id);
            if (product == null)
                return Task.FromResult(new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = "product not found" } } });
            try
            {
                _context.Products.Remove(product);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = ex.ToString() } } });
            }
            return Task.FromResult(new BaseResponse { StatusCode = "200" });
        }

        public async Task<GetProductsDto> GetAllProducts(PaginationParameters paginationParameters)
        {
            try
            {
                var pagedProducts = await _unitOfWork.ProductsRepository.GetPagedList(paginationParameters);
                var result = _mapper.Map<List<Products>, List<ProductsData>>(pagedProducts.ToList());
                GetProductsDto getProductsDto = new GetProductsDto
                {
                    StatusCode = "200",
                    Data = result
                };
                return getProductsDto;
            }
            catch (Exception ex)
            {
                return new GetProductsDto { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = ex.ToString() } } };
            }
        }

        public async Task<BaseResponse> UpdateProduct(CreateProductDto product)
        {
            try
            {
                var productEntry = _mapper.Map<CreateProductDto, Products>(product);
                await _unitOfWork.ProductsRepository.UpdateAsync(productEntry);
                return new BaseResponse { StatusCode = "200" };
            }
            catch (Exception ex)
            {
                return new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = ex.ToString() } } };
            }
        }
    }
    #endregion
}
