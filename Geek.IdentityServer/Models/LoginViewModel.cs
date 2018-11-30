using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Geek.IdentityServer.Models
{
    public class LoginViewModel
    {
        [Display(Name = "登录名称")]
        public string UserName { get; set; }

        [Display(Name = "登录密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
