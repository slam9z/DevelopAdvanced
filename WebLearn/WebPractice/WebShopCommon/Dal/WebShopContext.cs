using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebShopCommon.Models;
using WebShopCommon.Models.Mapping;

namespace WebShopCommon.Dal
{
    public partial class WebShopContext : DbContext
    {
        static WebShopContext()
        {
            //阻止自动创建表
           // Database.SetInitializer<WebShopContext>(null);
        }

        public WebShopContext(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public DbSet<Account> Accounts { get; set; }
       
        public DbSet<Shop> Shops { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new ShopMap());

        }

    }
}
