using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.DbContexts
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
            
        }

    }
}
