using HotelBooking.Models;
using HotelBooking.Services;

namespace HotelBooking.Repositories
{
    public class RoomRepository
    {
        private readonly DataService _dataService;
        private HotelData _data = new();

        public RoomRepository(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<List<RoomModel>> GetAllAsync()
        {
            _data = await _dataService.LoadHotelDataAsync();
            return _data.Rooms;
        }

        public async Task AddAsync(RoomModel room)
        {
            _data.Rooms.Add(room);
            await _dataService.SaveHotelDataAsync(_data);
        }

        public async Task UpdateAsync(RoomModel room)
        {
            var existing = _data.Rooms.FirstOrDefault(r => r.RoomNumber == room.RoomNumber);
            if (existing != null)
            {
                existing.Type = room.Type;
                existing.PricePerNight = room.PricePerNight;
                existing.IsAvailable = room.IsAvailable;
                existing.Description = room.Description;
                existing.Capacity = room.Capacity;
            }
            await _dataService.SaveHotelDataAsync(_data);
        }

        public async Task DeleteAsync(int roomNumber)
        {
            var room = _data.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room != null) _data.Rooms.Remove(room);
            await _dataService.SaveHotelDataAsync(_data);
        }
    }
}
