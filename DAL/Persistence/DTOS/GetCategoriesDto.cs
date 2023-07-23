using DAL.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence.DTOS
{
    public class GetCategoriesDto:BaseResponse
    {
        public GetCategoriesDto()
        {
            Data = new List<CategoriesData>();
        }
        public List<CategoriesData> Data { get; set; }
    }
    public class CategoriesData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeleteDate { get; set; }
    }
}
