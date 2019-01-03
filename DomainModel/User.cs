using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role RoleType { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public byte[] Avatar { get; set; }
        public ICollection<DomainModel.Track> FavoriteTracks { get; set; }
        public ICollection<DomainModel.Track> CreatedTracks { get; set; }
        public ICollection<DomainModel.Comment> CreatedComments { get; set; }

        public bool IsValid()
        {
            if(this.NickName == null ||
               this.Email == null ||
               this.Password == null ||
               this.NickName.Length > 30 ||
               this.Email.Length > 200 ||
               this.Password.Length > 30 ||
               this.NickName.Length < 5 ||
               this.Password.Length < 5 ||
               !this.Email.Contains("@"))
            {
                return false;
            }
            return true;
        }
    }
}
