using HotelAdmin.Helpers;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HotelAdmin.Service.RoomTariffService
{
    public class DbDaoRoomTariff : IDaoRoomTariff
    {
        private readonly AppDbContext _context;

        public DbDaoRoomTariff(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<RoomTariff>> IndexAsync(int page)
        {
            await _context.Rooms.LoadAsync();
            await _context.Categorys.LoadAsync();
            await _context.TariffPlans.LoadAsync();
            var roomTariff = await _context.Tariffs.ToListAsync();
            List<RoomTariff> list = new List<RoomTariff>();
            int TotalPages = (int)Math.Ceiling(roomTariff.Count / 10.0);

            if (!roomTariff.Any())
            {
                return roomTariff;
            }

            if (page == TotalPages)
            {
                for (var i = (page - 1) * 10; i < roomTariff.Count; i++)
                {
                    list.Add(roomTariff[i]);
                }
                return list;
            }
            else
            {
                for (var i = (page - 1) * 10; i < page * 10; i++)
                {
                    list.Add(roomTariff[i]);
                }
                return list;
            }
        }

    }
}
