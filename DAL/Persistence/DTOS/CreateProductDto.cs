using DAL.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence.DTOS
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public Guid DeleteUserId { get; set; }
        public DateTime DeleteDate { get; set; }
    }
}
