using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioscoopSOA3
{
    public class MovieTicket
    {
        public int rowNr { get; set; }
        public int seatNr { get; set; }
        public Boolean isPremium { get; set; }
        public MovieScreening Screening { get; set; }

        public MovieTicket(int rowNr, int seatNr, Boolean isPremium, MovieScreening screening)
        {
            this.rowNr = rowNr;
            this.seatNr = seatNr;
            this.isPremium = isPremium;
            Screening = screening;
        }

        public Boolean isPremiumTicket()
        {
            return isPremium;
        }

        public double getPrice()
        {
            return Screening.getPricePerSeat();

        }

        public override string ToString()
        {
            return $"Seat {seatNr} in row {rowNr} for {Screening.Movie.Title} at {Screening.DateAndTime}, Premium: {isPremium}, Price: {getPrice()}";
        }




    }
}