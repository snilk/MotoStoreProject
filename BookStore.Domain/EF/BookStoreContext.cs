using System.Data.Entity;

namespace BookStore.Domain.EF
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext()
            : base("Name=BookStoreDBEntities")
        {
        }

        public virtual DbSet<BookImage> BookImages { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ShopInformation> ShopInformations { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Survey> Surveys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //throw new UnintentionalCodeFirstException();
        }
    }
}