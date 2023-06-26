using HotelWebsiteBooking.Models.Entity;

namespace HotelWebsiteBooking.Service.RoomService
{
    public interface IDaoRoom
    {
        Task<List<Room>> RoomsAsync(int page);
        Task<List<RoomDate>> SearchAsync(DateTime start, DateTime end, int count);
        Task<bool> AddBookingAsync(RoomDate date, Client client, Comment comment, string content, Order order);
    }
}
