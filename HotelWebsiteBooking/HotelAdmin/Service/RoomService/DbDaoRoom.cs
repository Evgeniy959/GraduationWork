﻿using HotelAdmin.Helpers;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
                /*room.Number = extendedRoom.Number;
                room.CategoryId = extendedRoom.CategoryId;*/
                await _context.AddAsync(room);
                await _context.SaveChangesAsync();
                date.RoomId = room.Id;
                /*var roomTariff = new RoomTariff
                {
                    Price = extendedRoom.Price,
                    TariffPlanId = extendedRoom.TariffPlanId,
                    RoomId = room.Id,
                };*/
                await _context.AddAsync(date);
                //await _context.AddAsync(roomTariff);
                //int price = (await _context.Tariffs.FirstOrDefaultAsync(x => x.TariffPlanId == extendedRoom.TariffPlanId && x.Room.CategoryId == extendedRoom.CategoryId)).Price;
                //await _context.Tariffs.AddRangeAsync(tariffPlans.Select(t => new RoomTariff { RoomId = room.Id, TariffPlanId = t, Price = (_context.TariffAdmins.FirstOrDefaultAsync(x => x.TariffPlanId == t && x.CategoryId == room.CategoryId)).Result.Price }));
                await _context.Tariffs.AddRangeAsync(tariffPlans.Select(t => new RoomTariff { RoomId = room.Id, TariffPlanId = t, Price = (_context.TariffAdmins.FirstOrDefault(x => x.TariffPlanId == t && x.CategoryId == room.CategoryId)).Price }));
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
            

        }
        /*public async Task<bool> AddAsync(Room room, RoomDate date, IFormFile photo, Category category)
        {
            try
            {
                category.Photo = await FileUploadHelper.Upload(photo);
            }
            catch (Exception) { }
            var roomExsist = await _context.Rooms.FirstOrDefaultAsync(x => x.Number == room.Number);
            if (roomExsist == null)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                date.RoomId = room.Id;
                _context.Add(date);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;


        }*/

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
            var room = await _context.Rooms
                .SingleOrDefaultAsync(m => m.Id == id);

            return room;
        }

        public async Task<List<Room>> IndexAsync(int page)
        {
            await _context.Categorys.LoadAsync();
            await _context.Tariffs.LoadAsync();
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

        public async Task<bool> UpdateAsync(Room room, IFormFile photo)
        {
            /*try
            {
                room.Photo = await FileUploadHelper.Upload(photo);
            }
            catch (Exception) { }*/

            _context.Update(room);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
