using HotelWebsiteBooking.Models;
using HotelWebsiteBooking.Models.Entity;
using HotelWebsiteBooking.Service.DateService;
using Microsoft.EntityFrameworkCore;

namespace HotelWebsiteBooking.Service.RoomService
{
    public class DbDaoRoom : IDaoRoom
    {
        private readonly AppDbContext _context;
        private readonly DaoDate _date;       
        
        public DbDaoRoom(AppDbContext context, DaoDate date) 
        {
            _context = context;
            _date = date;
        }
        public async Task<bool> AddBookingAsync(RoomDate date, Client client, Comment comment, string content, Order order)
        {
            client.Start = _date.start;
            client.End = _date.end;
            _context.Add(client);
            await _context.SaveChangesAsync();
            order.ClientId = client.Id;
            order.Date = DateTime.Now;
            _context.Add(order);
            await _context.SaveChangesAsync();
            date.Start = _date.start;
            date.End = _date.end;
            date.RoomId = client.RoomId;
            date.ClientId = client.Id;
            var roomDate = await _context.Dates.FirstOrDefaultAsync(x => x.Start == null && x.RoomId == client.RoomId);
            if (roomDate != null)
            {
                roomDate.Start = _date.start;
                roomDate.End = _date.end;
                roomDate.ClientId = client.Id;
                _context.Dates.Update(roomDate);
            }
            else _context.Dates.Add(date);
            await _context.SaveChangesAsync();

            if (content != null)
            {
                comment.Name = client.Name;
                comment.Email = client.Email;
                comment.Content = content;
                comment.Date = DateTime.Now;
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

            }
            return true;

        }

        public async Task<List<Room>> RoomsAsync(int page)
        {
            var rooms = await _context.Rooms.ToListAsync();           
            List<Room> list = new List<Room>();
            int TotalPages = (int)Math.Ceiling(rooms.Count / 9.0);

            if (page == TotalPages)
            {
                for (var i = (page - 1) * 9; i < rooms.Count; i++)
                {
                    list.Add(rooms[i]);
                }
                return list;
            }
            else
            {
                for (var i = (page - 1) * 9; i < page * 9; i++)
                {
                    list.Add(rooms[i]);
                }
                return list;
            }
        }

        public async Task<List<RoomDate>> SearchAsync(DateTime start, DateTime end, int count)
        {
            _date.SaveDate(start, end);
            await _context.Rooms.LoadAsync();
            var rooms = await _context.Dates.Where(x => x.Room.PersonsCount == count).ToListAsync();
            var roomDates = await _context.Dates.Where(x => x.Start >= start && x.End <= end && x.Room.PersonsCount == count
            || x.Start >= start && x.Start <= end && x.End >= end && x.Room.PersonsCount == count
            || x.Start <= start && x.End <= end && x.End >= start && x.Room.PersonsCount == count
            || x.Start <= start && x.End >= end && x.Room.PersonsCount == count).ToListAsync();
            
            List<RoomDate> list = new List<RoomDate>();

            foreach (var room in rooms)
            {
                foreach (var roomDate in roomDates)
                {
                    if (room.RoomId == roomDate.RoomId)
                    {
                        list.Add(room);
                    }
                }
            }
            foreach (var item in list)
            {
                if (rooms.Contains(item))
                {
                    rooms.Remove(item);
                }

            }
            return rooms.GroupBy(g => g.RoomId)
                        .Select(r => r.First())
                        .ToList();
        }
    }
}
