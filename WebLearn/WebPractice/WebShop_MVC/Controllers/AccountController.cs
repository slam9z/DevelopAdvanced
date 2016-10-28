using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebShopCommon.Dto;
using WebShopCommon.Business;
using WebShopCommon.Enums;
using WebShopCommon.Models;


namespace WebApplication2015.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {

        private AccountBusiness _accountBusiness = new AccountBusiness();

        public AccountController()
        {
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountDto accountDto)
        {


            var result = _accountBusiness.CheckAccount(accountDto);

            switch (result)
            {
                case AccountValidateStatus.Success:
                    return RedirectToLocal(null);

            }

            return RedirectToLocal(null);
        }

        [HttpPost]
        public ActionResult Register(Account account)
        {
            // HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");


              var accout = _accountBusiness.GetAccountByName(account.UserName);

            var result = CommonOperationStatus.AlreadyExist;
            if (accout == null)
            {
                result = _accountBusiness.CreateAccount(account);
            }
            switch (result)
            {
                case CommonOperationStatus.Success:
                    return RedirectToLocal(null);

            }

            return RedirectToLocal(null);

        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


    }
}