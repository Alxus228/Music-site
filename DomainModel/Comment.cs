using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int UserIdWhoCreated { get; set; }
        public DomainModel.User UserWhoCreated { get; set; }
        public int TrackId { get; set; }
        public DomainModel.Track Track { get; set; }

        public bool IsValid()
        {
            if (this.Content == null || this.Content.Length > 100)
            {
                return false;
            }
            return true;
        }
    }
}
