using System;
using System.Collections.Generic;

namespace WebShopCommon.Models
{
    public  class Account
    {
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string RepeatPassword { get; set; }
        
        public System.DateTime CreatedTime { get; set; }
        public System.DateTime UpdatedTime { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
    }
}
