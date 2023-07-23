using AutoMapper;
using DAL.Domain;
using DAL.Domain.Models;
using DAL.Helpers.Common;
using DAL.Persistence.DTOS;

namespace Business_Layer.Services
{
    #region Interface
    public interface ICategoriesService
    {
        Task<GetCategoriesDto> GetAllCategories(PaginationParameters paginationParameters);
        Task<BaseResponse> CreateCategory(CreateCategoryDto createCategoryDto);
        Task<BaseResponse> UpdateCategory(CreateCategoryDto category);
        Task<BaseResponse> DeleteCategory(Guid Id);
    }
    #endregion
    #region Implementation
    public class CategoriesService : ICategoriesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly InventoryContext _context;
        public CategoriesService(IMapper mapper, IUnitOfWork unitOfWork, InventoryContext context)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<GetCategoriesDto> GetAllCategories(PaginationParameters paginationParameters)
        {
            try
            {
                var pagedCategories = await _unitOfWork.CategoriesRepository.GetPagedList(paginationParameters);
                var result = _mapper.Map<List<Categories>, List<CategoriesData>>(pagedCategories.ToList());
                GetCategoriesDto getCategoriesDto = new GetCategoriesDto
                {
                     StatusCode = "200",
                     Data = result
                };
                return getCategoriesDto;
            }
            catch (Exception ex)
            {
                return new GetCategoriesDto { StatusCode="400",Errors=new List<BaseErrors> { new BaseErrors {Code="702",Message=ex.ToString() } } };
            }
        }
        
        async Task<BaseResponse> ICategoriesService.CreateCategory(CreateCategoryDto createCategoryDto)
        {
            try
            {
                var categoryEntry = _mapper.Map<CreateCategoryDto, Categories>(createCategoryDto);
                await _unitOfWork.CategoriesRepository.AddAsync(categoryEntry);
                _unitOfWork.Save();
                return new BaseResponse { StatusCode = "200" };
            }
            catch (Exception ex)
            {
                return new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = ex.ToString() } } };
            }
        }

        Task<BaseResponse> ICategoriesService.DeleteCategory(Guid Id)
        {
            var category = _context.Categories.Find(Id);
            if (category == null)
                return Task.FromResult(new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = "Category not found" } } });
            try
            {
                _context.Categories.Remove(category);
            }
            catch (Exception ex)
            {
                return Task.FromResult( new BaseResponse { StatusCode = "400", Errors = new List<BaseErrors> { new BaseErrors { Code = "702", Message = ex.ToString() } } });
            }
            return Task.FromResult( new BaseResponse { StatusCode = "200" });
        }

        async Task<BaseResponse> ICategoriesService.UpdateCategory(CreateCategoryDto category)
        {
            try
            {
                var categoryEntry = _mapper.Map<CreateCategoryDto, Categories>(category);
                await _unitOfWork.CategoriesRepository.UpdateAsync(categoryEntry);
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