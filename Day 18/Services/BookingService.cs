using HotelBooking.Models;

namespace HotelBooking.Services
{
    public class BookingService
    {
        public (bool Valid, string Error) ValidateBooking(BookingModel booking)
        {
            if (string.IsNullOrWhiteSpace(booking.GuestFullName))
                return (false, "Введите ФИО гостя.");
            if (booking.GuestFullName.Trim().Split(' ').Length < 2)
                return (false, "Введите полное ФИО (минимум имя и фамилия).");
            if (string.IsNullOrWhiteSpace(booking.GuestPhone))
                return (false, "Введите номер телефона.");
            if (booking.CheckIn < DateTime.Today)
                return (false, "Дата заезда не может быть в прошлом.");
            if (booking.CheckIn >= booking.CheckOut)
                return (false, "Дата выезда должна быть позже даты заезда.");
            return (true, string.Empty);
        }
    }
}
