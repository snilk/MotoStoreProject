using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MotoStore.Domain.EF
{
    public class MotoStoreContext : DbContext
    {
        public MotoStoreContext()
            : base("Name=MotoStoreDBEntities")
        {
        }

        public virtual DbSet<MotoImage> MotoImages { get; set; }
        public virtual DbSet<Motorcycle> Motorcycles { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ShopInformation> ShopInformations { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //throw new UnintentionalCodeFirstException();
        }
    }
}