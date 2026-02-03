using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioscoopSOA3
{
    public class MovieScreening
    {
        public DateTime DateAndTime { get; set; }
        public double PricePerSeat { get; set; }
        public Movie Movie { get; }

        public MovieScreening(Movie movie, DateTime dateAndTime, double pricePerSeat)
        {
            Movie = movie;
            DateAndTime = dateAndTime;
            PricePerSeat = pricePerSeat;
        }

        public double getPricePerSeat()
        {
            return PricePerSeat;
        }

public override string ToString()
        {
            return $"{Movie.Title} at {DateAndTime}, Price per seat: {PricePerSeat:C}";
        }
    }
}