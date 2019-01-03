using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlModel
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int? GenreId { get; set; }
        public int? Year { get; set; }
        public string Lyrics { get; set; }
        public float SizeInMB { get; set; }
        public int DurationInSeconds { get; set; }
        public string Quality { get; set; }
        public byte[] Icon { get; set; }
        public int? AudioFileId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int? UserIdWhoCreated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
