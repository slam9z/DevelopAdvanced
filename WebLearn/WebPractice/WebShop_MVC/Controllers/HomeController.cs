using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Model;
using WebShopCommon.Business;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {

        private ShopBusiness _shopBusiness = new ShopBusiness();
        public ActionResult Index()
        {
            var posts = _shopBusiness.GetShops();
            var model = new ShopModel();
            model.Shops = posts;
            return View(model);
        }

    }
}
