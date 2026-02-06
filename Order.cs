using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioscoopSOA3
{
    public class Order
    {
        public int OrderNr { get; set; }
        public Boolean IsStudent { get; set; }
        private const double groupDiscount = 0.9;
        public List<MovieTicket> tickets { get; set; } = new List<MovieTicket>();

        public Order(int orderNr, Boolean isStudent)
        {
            OrderNr = orderNr;
            IsStudent = isStudent;
        }

        public int getOrderNr()
        {
            return OrderNr;
        }

        public void addSeatReservation(MovieTicket ticket)
        {
            tickets.Add(ticket);    
        }

        public double calculatePrice()
        {
            if (tickets.Count == 0)
            {
                return 0.0;
            }
            double totalPrice = 0.0;
            bool secondTicketFree = false;
            bool groupDiscountApplied = false;
            double premiumExtra = 0.0;

            if (IsStudent)
            {
                secondTicketFree = true;
                premiumExtra = 2.0;
            }
            else
            {
                var first= tickets[0];
                var day = first.Screening.DateAndTime.DayOfWeek;        
                bool isWeekday = day != DayOfWeek.Friday && day != DayOfWeek.Saturday && day != DayOfWeek.Sunday;

                if (isWeekday)
                {
                    secondTicketFree = true;
                }
                premiumExtra = 3.0;
                groupDiscountApplied = tickets.Count >= 5;
            }
            for (int i = 0; i < tickets.Count; i++)
            {
                if (secondTicketFree && i % 2 == 1)
                {
                    continue; // Skip the price for the second ticket
                    
                }
                double price = tickets[i].getPrice()
                        + (tickets[i].isPremiumTicket() ? premiumExtra : 0.0);
                totalPrice += price;
            }
            if (groupDiscountApplied)
            {
                totalPrice *= groupDiscount;
            }
            return totalPrice;

        }

        public void export(TicketExportFormat exportFormat)
        {
            switch (exportFormat)
            {
                case TicketExportFormat.PLAINTEXT:
                    foreach (var ticket in tickets)
                    {
                        Console.WriteLine(ticket.ToString());
                    }
                    Console.WriteLine($"Total Price: {calculatePrice()}");
                    break;
                case TicketExportFormat.JSON:
                    var ticketList = new List<Dictionary<string, object>>();
                    foreach (var ticket in tickets)
                    {
                        var ticketDict = new Dictionary<string, object>
                        {
                            { "rowNr", ticket.RowNr },
                            { "seatNr", ticket.SeatNr },
                            { "isPremium", ticket.IsPremium },
                            { "movieTitle", ticket.Screening.Movie.Title },
                            { "dateAndTime", ticket.Screening.DateAndTime },
                            { "price", ticket.getPrice() + (ticket.isPremiumTicket() ? 2.0 : 0.0) },
                        };
                        ticketList.Add(ticketDict);
                    }
                    var orderDict = new Dictionary<string, object>
                    {
                        { "orderNr", OrderNr },
                        { "isStudent", IsStudent },
                        { "tickets", ticketList },
                        { "totalPrice", calculatePrice() },
                    };
                    var json = System.Text.Json.JsonSerializer.Serialize(
                        orderDict,
                        new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
                    );
                    Console.WriteLine(json);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(exportFormat), exportFormat, null);
            }
        }
    }
}
