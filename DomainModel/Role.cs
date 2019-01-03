using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public enum Role : int
    {
        Admin = 1,
        Moderator = 2,
        Regular = 3
    }
    
    public static class RoleExtension
    {
        public static string ToString(this Role role)
        {
            switch(role)
            {
                case Role.Admin:
                    return "Admin";
                case Role.Moderator:
                    return "Moderator";
                case Role.Regular:
                    return "Regular";
                default:
                    throw new Exception("Something went wrong.");
            }
        }
    }
}
