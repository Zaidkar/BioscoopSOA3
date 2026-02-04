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
            var totalPrice = 0.0;
            var ticketsToPay = new List<MovieTicket>();

            if (tickets.Count == 0)
            {
                return 0.0;
            }

            if (IsStudent)
            {
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        ticketsToPay.Add(tickets[i]);
                    }
                }

                foreach (var ticket in ticketsToPay)
                {
                    double price = ticket.getPrice();
                    if (ticket.isPremiumTicket())
                    {
                        price += 2.0;
                    }
                    totalPrice += price;
                }
            }
            else
            {
                var first = tickets.FirstOrDefault();
                var day = first != null ? first.Screening.DateAndTime.DayOfWeek : DayOfWeek.Monday;
                bool isWeekday =
                    day != DayOfWeek.Friday && day != DayOfWeek.Saturday && day != DayOfWeek.Sunday;

                if (isWeekday)
                {
                    for (int i = 0; i < tickets.Count; i++)
                    {
                        if (i % 2 == 0)
                        {
                            ticketsToPay.Add(tickets[i]);
                        }
                    }
                }
                else
                {
                    ticketsToPay.AddRange(tickets);
                }

                foreach (var ticket in ticketsToPay)
                {
                    double price = ticket.getPrice();
                    if (ticket.isPremiumTicket())
                    {
                        price += 3.0;
                    }
                    totalPrice += price;
                }

                if (tickets.Count >= 5)
                {
                    totalPrice = totalPrice * groupDiscount;
                }
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
