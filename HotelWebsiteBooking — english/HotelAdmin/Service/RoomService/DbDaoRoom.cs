using HotelAdmin.Helpers;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HotelAdmin.Service.RoomService
{
    public class DbDaoRoom : IDaoRoom
    {
        private readonly AppDbContext _context;

        public DbDaoRoom(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> AddAsync(Room room, RoomDate date, int[] tariffPlans)
        {
            var roomExsist = await _context.Rooms.FirstOrDefaultAsync(x => x.Number == room.Number);
            if (roomExsist == null) 
            {
                await _context.AddAsync(room);
                await _context.SaveChangesAsync();
                date.RoomId = room.Id;
                await _context.AddAsync(date);
                await _context.Tariffs.AddRangeAsync(tariffPlans.Select(t => new RoomTariff { RoomId = room.Id, TariffPlanId = t, Price = (_context.TariffAdmins.FirstOrDefault(x => x.TariffPlanId == t && x.CategoryId == room.CategoryId)).Price }));
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
            

        }

        public async Task DeleteConfirmedAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Room> GetAsync(int id)
        {
            await _context.Categorys.LoadAsync();
            await _context.Tariffs.LoadAsync();
            var room = await _context.Rooms
                .SingleOrDefaultAsync(m => m.Id == id);

            return room;
        }

        public async Task<List<Room>> IndexAsync(int page)
        {
            await _context.Categorys.LoadAsync();
            //await _context.Tariffs.LoadAsync();
            await _context.TariffAdmins.LoadAsync();
            var rooms = await _context.Rooms.ToListAsync();
            List<Room> list = new List<Room>();
            int TotalPages = (int)Math.Ceiling(rooms.Count / 10.0);

            if (!rooms.Any())
            {
                return rooms;
            }

            if (page == TotalPages)
            {
                for (var i = (page - 1) * 10; i < rooms.Count; i++)
                {
                    list.Add(rooms[i]);
                }
                return list;
            }
            else
            {
                for (var i = (page - 1) * 10; i < page * 10; i++)
                {
                    list.Add(rooms[i]);
                }
                return list;
            }
        }

        public async Task<bool> UpdateAsync(Room room/*, RoomTariff tariff*/, int[] tariffPlans)
        {
            _context.Update(room);
            await _context.SaveChangesAsync();
            var tariffs = await _context.Tariffs.Where(x => x.RoomId == room.Id).ToListAsync();
            var tariffAdmins = await _context.TariffAdmins.Where(x => x.CategoryId == room.CategoryId).ToListAsync();
            if (tariffs.Count < tariffAdmins.Count)
            {
                for (int i = 1; i < tariffAdmins.Count; i++)
                {
                    tariffs[i-1].Price = tariffAdmins[i].Price;
                }
            }
            else if (tariffs.Count == tariffAdmins.Count)
            {
                for (int i = 0; i < tariffAdmins.Count; i++)
                {
                    tariffs[i].Price = tariffAdmins[i].Price;
                }
            }

            _context.Tariffs.UpdateRange(tariffs);
            await _context.SaveChangesAsync();
            return true;

            //var tariff = await _context.Tariffs.FirstOrDefaultAsync(x => x.RoomId == room.Id);
            //if (tariffExsist == null)
            //foreach (var tariff in tariffs)

            /*tariff.Price = (_context.TariffAdmins.FirstOrDefault(x => x.CategoryId == room.CategoryId)).Price;
_context.Tariffs.Update(tariff);*/
            /*foreach (var item in tariffPlans)
            {
                tariff.Price = (_context.TariffAdmins.FirstOrDefault(x => x.TariffPlanId == item && x.CategoryId == room.CategoryId)).Price;
                _context.Tariffs.Update(tariff);
            }*/

            //_context.Tariffs.UpdateRange(tariffPlans.Select(t => new RoomTariff { RoomId = room.Id, TariffPlanId = t, Price = (_context.TariffAdmins.FirstOrDefault(x => x.TariffPlanId == t && x.CategoryId == room.CategoryId)).Price }));
            //tariffPlans.Select(t => new RoomTariff { RoomId = room.Id, TariffPlanId = t, Price = (_context.TariffAdmins.FirstOrDefault(x => x.TariffPlanId == t && x.CategoryId == room.CategoryId)).Price }));

        }
    }
}
