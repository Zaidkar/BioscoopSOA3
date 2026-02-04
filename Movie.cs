using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioscoopSOA3
{
    public class Movie
    {
        public string Title { get; set; }

        public List<MovieScreening> Screenings { get; set; } = new List<MovieScreening>();

        public Movie(string title)
        {
            Title = title;
        }

        public void AddScreening(MovieScreening screening)
        {
            Screenings.Add(screening);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
