using HotelBooking.Models;

namespace HotelBooking.Services
{
    public class BookingService
    {
        private readonly DataService _dataService;

        public BookingService(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<(bool Success, string Message)> BookRoomAsync(
            RoomModel room, BookingModel booking, HotelData hotelData)
        {
            if (!room.IsAvailable)
                return (false, "Номер уже занят.");

            if (string.IsNullOrWhiteSpace(booking.GuestFullName))
                return (false, "Введите ФИО гостя.");

            if (booking.CheckIn >= booking.CheckOut)
                return (false, "Дата выезда должна быть позже даты заезда.");

            if (booking.CheckIn < DateTime.Today)
                return (false, "Дата заезда не может быть в прошлом.");

            // Simulate server confirmation delay
            await Task.Delay(3000);

            booking.RoomNumber = room.RoomNumber;
            booking.TotalPrice = room.PricePerNight * booking.Nights;
            booking.Status = "Активно";
            booking.Id = Guid.NewGuid().ToString();

            room.IsAvailable = false;
            hotelData.Bookings.Add(booking);

            await _dataService.SaveHotelDataAsync(hotelData);
            return (true, $"Номер {room.RoomNumber} успешно забронирован!\nСумма: {booking.TotalPrice:N0} Br");
        }

        public async Task<(bool Success, string Message)> CancelBookingAsync(
            BookingModel booking, HotelData hotelData)
        {
            var room = hotelData.Rooms.FirstOrDefault(r => r.RoomNumber == booking.RoomNumber);
            if (room == null)
                return (false, "Номер не найден.");

            await Task.Delay(1500);

            booking.Status = "Отменено";
            room.IsAvailable = true;

            await _dataService.SaveHotelDataAsync(hotelData);
            return (true, $"Бронирование номера {booking.RoomNumber} отменено.");
        }

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
