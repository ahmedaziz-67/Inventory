using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain.Models
{
    public class Products
    {
        public Products()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Categories Categories { get; set; }
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