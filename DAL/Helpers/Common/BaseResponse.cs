using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers.Common
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Errors = new List<BaseErrors>();
        }
        public string StatusCode { get; set; }
        public List<BaseErrors> Errors { get; set; }
    }
    public class BaseErrors
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
