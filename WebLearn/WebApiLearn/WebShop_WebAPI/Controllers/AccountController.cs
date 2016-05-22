using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebShopCommon.Business;
using WebShopCommon.Dto;
using WebShopCommon.Enums;
using RebuCommon.Utils;
using System.Net.Http.Headers;
using WebShopCommon.Models;
using Newtonsoft.Json;
using System.IO;
using WebShop.App_Start;
using System.Web;
using System.Web.Http.Cors;
using System.Diagnostics;

namespace WebShop.Controllers
{

    //[AllowCrossSiteJson]
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class AccountController : ApiController
    {
        private AccountBusiness _accountBusiness = new AccountBusiness();


        [HttpGet]
        [Route("Test/Test2")]
        public HttpResponseMessage Test()
        {

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent("{\"Code\":\"0\"}")
            };
			Debug.WriteLine("api/Test/Test");
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }


        [HttpPost]
        [Route("api/Account/Login")]
  
        public HttpResponseMessage Login(AccountDto accountDto)
        {


            if (accountDto == null)
            {
                return new HttpResponseMessage();
            }
            var result = _accountBusiness.CheckAccount(accountDto);

            if (result == AccountValidateStatus.Success)
            {
                StringWriter sw = new StringWriter();
                JsonWriter writer = new JsonTextWriter(sw);

                writer.WriteStartObject();
                writer.WritePropertyName("Code");
                writer.WriteValue(result);
                writer.WritePropertyName("UserId");
                writer.WriteValue(accountDto.UserId);
                writer.WriteEndObject();
                writer.Flush();

                string jsonText = sw.GetStringBuilder().ToString();

                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent(jsonText)
                };

                return resp;

            }
            else
            {

                StringWriter sw = new StringWriter();
                JsonWriter writer = new JsonTextWriter(sw);

                writer.WriteStartObject();
                writer.WritePropertyName("Code");
                writer.WriteValue(result);
                writer.WriteEndObject();
                writer.Flush();

                string jsonText = sw.GetStringBuilder().ToString();

                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent(jsonText)
                };

                return resp;
            }
        }

        [HttpPost]
        [Route("api/Account/Register")]
 
    
        public HttpResponseMessage Register(Account account)
        {
           // HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            if (account == null)
            {
                return new HttpResponseMessage();
            }

            var accout = _accountBusiness.GetAccountByName(account.UserName);

            var result = CommonOperationStatus.AlreadyExist;
            if (accout == null)
            {
                 result = _accountBusiness.CreateAccount(account);
            }

            StringWriter sw = new StringWriter();
            JsonWriter writer = new JsonTextWriter(sw);

            writer.WriteStartObject();
            writer.WritePropertyName("Code");
            writer.WriteValue(result);
            writer.WriteEndObject();
            writer.Flush();

            string jsonText = sw.GetStringBuilder().ToString();

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonText)
            };

            return resp;
        }


		[HttpGet]
		[Route("api/Account/Register")]

		public HttpResponseMessage Register(string userName)
		{
			// HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

		

			var account = _accountBusiness.GetAccountByName(userName);

			var result = CommonOperationStatus.AlreadyExist;
			if (account == null)
			{
				result = _accountBusiness.CreateAccount(account);
			}

			StringWriter sw = new StringWriter();
			JsonWriter writer = new JsonTextWriter(sw);

			writer.WriteStartObject();
			writer.WritePropertyName("Code");
			writer.WriteValue(result);
			writer.WriteEndObject();
			writer.Flush();

			string jsonText = sw.GetStringBuilder().ToString();

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(jsonText)
			};

			return resp;
		}

	}
}
