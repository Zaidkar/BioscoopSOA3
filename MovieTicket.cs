using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioscoopSOA3
{
    public class MovieTicket
    {
        public int RowNr { get; set; }
        public int SeatNr { get; set; }
        public Boolean IsPremium { get; set; }
        public MovieScreening Screening { get; set; }

        public MovieTicket(int rowNr, int seatNr, Boolean isPremium, MovieScreening screening)
        {
            RowNr = rowNr;
            SeatNr = seatNr;
            IsPremium = isPremium;
            Screening = screening;
        }

        public Boolean isPremiumTicket()
        {
            return IsPremium;
        }

        public double getPrice()
        {
            return Screening.getPricePerSeat();
        }

        public override string ToString()
        {
            return $"Seat {SeatNr} in row {RowNr} for {Screening.Movie.Title} at {Screening.DateAndTime}, Premium: {IsPremium}, Price: {getPrice()}";
        }
    }
}
