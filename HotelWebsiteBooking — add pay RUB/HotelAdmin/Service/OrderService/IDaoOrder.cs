using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.OrderService
{
    public interface IDaoOrder
    {
        Task<List<Order>> IndexAsync(int page);
        Task<Order> GetAsync(int id);
        Task DeleteConfirmedAsync(int id);
    }
}
