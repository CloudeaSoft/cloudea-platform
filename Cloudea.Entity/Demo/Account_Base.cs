using Cloudea.Infrastructure.Db;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Entity.Demo
{
    public class Account_Base : EntityBase
    {
        [Column(IsNullable = false)]
        public string Username { get; set; }

        [Column(IsNullable = false)]
        public string Password { get; set; }

        [Column(IsNullable =false)]
        [EmailAddress]
        public string Email { get; set; }

        public Account_Base(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }
    }
}
