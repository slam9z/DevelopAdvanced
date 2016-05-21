using System;

namespace WebShopCommon.Dto
{
    public class AccountDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }

        public Guid UserId { get; set; }
    }
}