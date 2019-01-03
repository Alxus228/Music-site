using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlModel
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleType { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public byte[] Avatar { get; set; }
        public bool IsDeleted { get; set; }
    }
}
