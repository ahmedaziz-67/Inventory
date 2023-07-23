using DAL.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence.DTOS
{
    public class GetProductsDto:BaseResponse
    {
        public List<ProductsData> Data { get; set; }
    }
    public class ProductsData
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeleteDate { get; set; }
    }
}
