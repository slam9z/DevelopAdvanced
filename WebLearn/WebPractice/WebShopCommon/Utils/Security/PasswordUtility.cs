using System;
using System.Security.Cryptography;
using System.Text;


namespace RebuCommon.Utils
{
    public static class PasswordUtility
    {
        public static string EncryptPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);

            byte[] result = Encoding.Default.GetBytes(password);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);

            var base64 = Convert.ToBase64String(output);

            return base64;
        }
    }
}
