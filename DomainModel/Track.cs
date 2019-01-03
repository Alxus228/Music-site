using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int? GenreId { get; set; }
        public DomainModel.Genre Genre { get; set; }
        public int? Year { get; set; }
        public string Lyrics { get; set; }
        public int FavoriteCount { get; set; }
        public float SizeInMB { get; set; }
        public int DurationInSeconds { get; set; }
        public string Quality { get; set; }
        public byte[] Icon { get; set; }
        public int? AudioFileId { get; set; }
        public byte[] AudioFile { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int? UserIdWhoCreated { get; set; }
        public DomainModel.User UserWhoCreated { get; set; }
        public ICollection<DomainModel.Tag> Tags { get; set; }
        public ICollection<DomainModel.Comment> Comments { get; set; }

        public bool IsValid()
        {
            if(this.Quality == null ||
               this.AudioFile == null ||
               this.Name.Length > 100 ||
               this.Author.Length > 100 ||
               this.Lyrics.Length > 100000 ||
               this.Quality.Length > 10 ||
               this.Year < 0 ||
               this.Year > DateTime.Now.Year)
            {
                return false;
            }
            return true;
        }
    }
}
