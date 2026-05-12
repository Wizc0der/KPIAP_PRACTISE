using HotelBooking.Models;
using HotelBooking.Services;

namespace HotelBooking.Repositories
{
    public class BookingRepository
    {
        private readonly DataService _dataService;
        private HotelData _data = new();

        public BookingRepository(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<List<BookingModel>> GetAllAsync()
        {
            _data = await _dataService.LoadHotelDataAsync();
            return _data.Bookings;
        }

        public async Task AddAsync(BookingModel booking)
        {
            _data.Bookings.Add(booking);
            await _dataService.SaveHotelDataAsync(_data);
        }

        public async Task UpdateAsync(BookingModel booking)
        {
            var existing = _data.Bookings.FirstOrDefault(b => b.Id == booking.Id);
            if (existing != null)
            {
                existing.Status = booking.Status;
                existing.GuestFullName = booking.GuestFullName;
                existing.GuestPhone = booking.GuestPhone;
                existing.GuestEmail = booking.GuestEmail;
                existing.CheckIn = booking.CheckIn;
                existing.CheckOut = booking.CheckOut;
                existing.TotalPrice = booking.TotalPrice;
            }
            await _dataService.SaveHotelDataAsync(_data);
        }

        public async Task DeleteAsync(string bookingId)
        {
            var booking = _data.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking != null) _data.Bookings.Remove(booking);
            await _dataService.SaveHotelDataAsync(_data);
        }
    }
}
