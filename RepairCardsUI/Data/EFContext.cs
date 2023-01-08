using RepairCardsUI.Models;
using System.Data.Entity;
using System.Linq;

namespace RepairCardsUI.Data
{
    public class EFContext : DbContext
    {
        public EFContext() : base("Data source = productionsql; Initial catalog = production; Integrated security = true;")
        {
            Database.SetInitializer<EFContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<PermissionCardOperation>().Property(x => x.Labor).HasPrecision(18, 4);
        }

        public DbSet<PermissionCard> PermissionCards { get; set; }
        public DbSet<PermissionCardProduct> PermissionCardProducts { get; set; }
        public DbSet<PermissionCardPurchasedProduct> PermissionCardPurchasedProducts { get; set; }
        public DbSet<PermissionCardMaterial> PermissionCardMaterials { get; set; }
        public DbSet<PermissionCardOperation> PermissionCardOperations { get; set; }

        public string GetNewNumber(int department) => Database.SqlQuery<string>(
            $"select cast(isnull(max(convert(int, left(Number, charindex('/', Number) - 1))), 0) + 1 as varchar(max)) + '/{department}.'  + right(year(getdate()), 2) from CRPermissionCards where Number like '%/{department}.' + right(year(getdate()), 2)").First();
    }
}
