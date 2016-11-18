using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCommon.Models;

namespace WebShopCommon.Business
{
    public class ShopBusiness : BusinessBase
    {
        public IEnumerable<Shop> GetShops()
        {
            var shops = new List<Shop>();
            shops.Add(new Shop {
                Id = new Guid("3bce4931-6c75-41ab-afe0-2ec108a30860"),
                Name = "梦见 Drifting Dream",
                ImageId = new Guid("3bce4931-6c75-41ab-afe0-2ec108a30862"),
                Price= 70
            });

            shops.Add(new Shop
            {
                Id = new Guid("3bce4931-6c75-41ab-afe0-2ec108a30860"),
                Name = "梦见 Drifting Dream",
                ImageId = new Guid("3bce4931-6c75-41ab-afe0-2ec108a30862"),
                Price = 70
            });
            shops.Add(new Shop
            {
                Id = new Guid("3bce4931-6c75-41ab-afe0-2ec108a30860"),
                Name = "梦见 Drifting Dream",
                ImageId = new Guid("3bce4931-6c75-41ab-afe0-2ec108a30862"),
                Price = 70
            });
            shops.Add(new Shop
            {
                Id = new Guid("3bce4931-6c75-41ab-afe0-2ec108a30860"),
                Name = "梦见 Drifting Dream",
                ImageId = new Guid("3bce4931-6c75-41ab-afe0-2ec108a30862"),
                Price = 70
            });
            shops.Add(new Shop
            {
                Id = new Guid("3bce4931-6c75-41ab-afe0-2ec108a30860"),
                Name = "梦见 Drifting Dream",
                ImageId = new Guid("3bce4931-6c75-41ab-afe0-2ec108a30862"),
                Price = 70

            });

            return shops;
        }
    }
}
