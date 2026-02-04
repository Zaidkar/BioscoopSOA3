using System;
using BioscoopSOA3;

var movie = new Movie("Interstellar");
var screeningDate = new DateTime(2026, 2, 3, 19, 30, 0);
var screening = new MovieScreening(movie, screeningDate, pricePerSeat: 5.0);
movie.AddScreening(screening);

var order = new Order(orderNr: 1001, isStudent: true);

order.addSeatReservation(
    new MovieTicket(rowNr: 1, seatNr: 1, isPremium: false, screening: screening)
);
order.addSeatReservation(
    new MovieTicket(rowNr: 1, seatNr: 2, isPremium: true, screening: screening)
);
order.addSeatReservation(
    new MovieTicket(rowNr: 1, seatNr: 3, isPremium: true, screening: screening)
);
order.addSeatReservation(
    new MovieTicket(rowNr: 1, seatNr: 4, isPremium: false, screening: screening)
);

Console.WriteLine($"Order #{order.getOrderNr()} for movie: {movie.Title}");
Console.WriteLine($"Screening: {screening}");
Console.WriteLine();

order.export(TicketExportFormat.PLAINTEXT);

Console.WriteLine();

Console.WriteLine("JSON export:");
order.export(TicketExportFormat.JSON);
