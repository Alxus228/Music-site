using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DomainModel.Track> Tracks { get; set; }

        public bool IsValid()
        {
            if (this.Name == null || this.Name.Length > 30)
            {
                return false;
            }
            return true;
        }
    }
}
