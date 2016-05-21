using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShopCommon.Models;

namespace WebShop.Model
{
    public class ShopModel
    {
        public IEnumerable<Shop> Shops
        {
            get;
            set;
        }
    }
}