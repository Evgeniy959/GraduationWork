using HotelAdmin.Models.Entity;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;

namespace HotelAdmin.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomDate> Dates { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RoomTariff> Tariffs { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Email = "admin@mail.ru",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin")

                });

            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    Number = "425Л",
                    Photo = "img/room/room-5.jpg",
                    Сategory = "Люкс",
                    PersonsCount = 3
                },
                new Room
                {
                    Id = 2,
                    Number = "305ПЛ",
                    Photo = "img/room/room-4.jpg",
                    Сategory = "Полулюкс",
                    PersonsCount = 3
                },
                new Room
                {
                    Id = 3,
                    Number = "305ПЛ",
                    Photo = "img/room/room-4.jpg",
                    Сategory = "Полулюкс",
                    PersonsCount = 3
                },
                new Room
                {
                    Id = 4,
                    Number = "527С",
                    Photo = "img/room/room-3.jpg",
                    Сategory = "Стандарт",
                    PersonsCount = 3
                });
        }
    }
}
