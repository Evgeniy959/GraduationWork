using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.RoomTariffService
{
    public interface IDaoRoomTariff
    {
        Task<List<RoomTariff>> IndexAsync(int page);
        
    }
}
