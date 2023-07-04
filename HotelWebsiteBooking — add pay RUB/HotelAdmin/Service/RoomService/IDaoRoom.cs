using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.RoomService
{
    public interface IDaoRoom
    {
        Task<List<Room>> IndexAsync(int page);
        Task<bool> AddAsync(Room room, RoomDate date, IFormFile photo);
        Task<Room> GetAsync(int id);
        Task<bool> UpdateAsync(Room room, IFormFile photo);
        Task DeleteConfirmedAsync(int id);
    }
}
