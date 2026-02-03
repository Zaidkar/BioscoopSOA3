using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioscoopSOA3
{
    public class Order
    {
        public int OrderNr { get; set; }
        public Boolean isStudent { get; set; }
        public List<MovieTicket> Tickets { get; set; } = new List<MovieTicket>();
        public Order(int orderNr, Boolean isStudent)
        {
            OrderNr = orderNr;
            this.isStudent = isStudent;
        }

        public int getOrderNr()
        {
            return OrderNr;
        }

        public void addSeatReservation(MovieTicket ticket)
        {
            Tickets.Add(ticket);
        }

        public double calculatePrice()
        {
            return 0.0;
        }

        public void export(TicketExportFormat exportFormat)
        {}
    }
}