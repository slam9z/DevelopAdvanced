using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace WebShopCommon.Models.Mapping
{
    public class ShopMap : EntityTypeConfiguration<Shop>
    {
        public ShopMap()
        {
            HasKey(t=>t.Id);

            ToTable("Shop");

            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ImageId).HasColumnName("ImageId");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Price).HasColumnName("Price");


        }
    }
}
