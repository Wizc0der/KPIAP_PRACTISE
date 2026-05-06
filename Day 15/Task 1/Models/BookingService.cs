using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Task_1.Models;

namespace Task_1.Services
{
    public class BookingService
    {
        private ObservableCollection<Room> rooms;
        private ObservableCollection<Booking> bookings;

        public BookingService()
        {
            rooms = new ObservableCollection<Room>();
            bookings = new ObservableCollection<Booking>();
            InitializeRooms();
        }

        private void InitializeRooms()
        {
            rooms.Add(new Room { Number = 101, Type = "Стандарт", Price = 3000, Status = "Свободен" });
            rooms.Add(new Room { Number = 102, Type = "Стандарт", Price = 3000, Status = "Свободен" });
            rooms.Add(new Room { Number = 103, Type = "Стандарт", Price = 3000, Status = "Свободен" });
            rooms.Add(new Room { Number = 104, Type = "Стандарт", Price = 3000, Status = "Забронирован" });
            rooms.Add(new Room { Number = 201, Type = "Люкс", Price = 5000, Status = "Свободен" });
            rooms.Add(new Room { Number = 202, Type = "Люкс", Price = 5000, Status = "Забронирован" });
            rooms.Add(new Room { Number = 203, Type = "Люкс", Price = 5000, Status = "Свободен" });
            rooms.Add(new Room { Number = 204, Type = "Люкс", Price = 5000, Status = "Свободен" });
            rooms.Add(new Room { Number = 301, Type = "Сьют", Price = 8000, Status = "Свободен" });
            rooms.Add(new Room { Number = 302, Type = "Сьют", Price = 8000, Status = "Свободен" });
            rooms.Add(new Room { Number = 303, Type = "Сьют", Price = 8000, Status = "Забронирован" });
            rooms.Add(new Room { Number = 401, Type = "Президентский", Price = 15000, Status = "Свободен" });
        }

        public ObservableCollection<Room> GetRooms() => rooms;
        public ObservableCollection<Booking> GetBookings() => bookings;

        public async Task<BookingResult> BookRoomAsync(Room room, Booking booking)
        {
            await Task.Delay(3000);

            if (room.Status == "Забронирован")
                return new BookingResult { Success = false, Message = "Номер уже забронирован" };

            if (string.IsNullOrWhiteSpace(booking.GuestName))
                return new BookingResult { Success = false, Message = "Введите ФИО гостя" };

            if (booking.CheckIn >= booking.CheckOut)
                return new BookingResult { Success = false, Message = "Дата выезда должна быть позже заезда" };

            room.Status = "Забронирован";
            booking.RoomNumber = room.Number;
            booking.TotalPrice = (booking.CheckOut.Date - booking.CheckIn.Date).Days * room.Price;
            booking.Status = "Активна";
            bookings.Add(booking);

            return new BookingResult
            {
                Success = true,
                Message = $"Бронь №{booking.Id} создана!\nСумма: {booking.TotalPrice:0} ₽"
            };
        }

        public async Task<BookingResult> CancelBookingAsync(Room room)
        {
            await Task.Delay(1000);

            if (room.Status == "Свободен")
                return new BookingResult { Success = false, Message = "Номер не забронирован" };

            room.Status = "Свободен";
            return new BookingResult { Success = true, Message = "Бронь отменена" };
        }
    }

    public class BookingResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}