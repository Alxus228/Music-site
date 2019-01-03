using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlModel
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int UserIdWhoCreated { get; set; }
        public int TrackId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
