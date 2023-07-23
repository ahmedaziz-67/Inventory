using DAL.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Domain
{
    public class InventoryContext : IdentityDbContext<ApplicationUsers>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InventoryContext(DbContextOptions<InventoryContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
