namespace HotelBooking.Models
{
    public class HotelData
    {
        public List<RoomModel> Rooms { get; set; } = new();
        public List<BookingModel> Bookings { get; set; } = new();
        public List<UserModel> Guests { get; set; } = new();
    }
}
