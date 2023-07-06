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
            modelBuilder.Entity<RoomTariff>()
                .HasKey(r => r.RoomId);

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
                    Number = "204",
                    Photo = "img/room/room-s.jpeg",
                    Сategory = "Стандарт",
                    Square = "20кв.м",
                    PersonsCount = 2
                },
                new Room
                {
                    Id = 2,
                    Number = "307СБ",
                    Photo = "img/room/room-sb.jpg",
                    Сategory = "Стандарт с большой кроватью",
                    Square = "20кв.м",
                    PersonsCount = 2
                },
                new Room
                {
                    Id = 3,
                    Number = "405СУ",
                    Photo = "img/room/room-si.jpeg",
                    Сategory = "Стандарт Улучшенный",
                    Square = "25кв.м",
                    PersonsCount = 2
                },
                new Room
                {
                    Id = 4,
                    Number = "405СУ",
                    Photo = "img/room/room-sbi.jpeg",
                    Сategory = "Стандарт Улучшенный с большой кроватью",
                    Square = "25кв.м",
                    PersonsCount = 2
                },
                new Room
                {
                    Id = 5,
                    Number = "514ПЛ",
                    Photo = "img/room/room-pl.jpg",
                    Сategory = "Полулюкс",
                    Square = "32кв.м",
                    PersonsCount = 4
                },
                new Room
                {
                    Id = 6,
                    Number = "618Л",
                    Photo = "img/room/room-l.jpg",
                    Сategory = "Люкс",
                    Square = "46кв.м",
                    PersonsCount = 4
                });
                /*new Room
                {
                    Id = 7,
                    Number = "725ДЛ",
                    Photo = "img/room/room-dl.jpeg",
                    Сategory = "Делюкс",
                    Square = "58 кв.м",
                    PersonsCount = 4
                });*/

            modelBuilder.Entity<RoomDate>().HasData(
                new RoomDate { Id = 1, RoomId = 1 },
                new RoomDate { Id = 2, RoomId = 2 },
                new RoomDate { Id = 3, RoomId = 3 },
                new RoomDate { Id = 4, RoomId = 4 },
                new RoomDate { Id = 5, RoomId = 5 },
                new RoomDate { Id = 6, RoomId = 6 }
                );

            /*modelBuilder.Entity<RoomTariff>().HasData(
                new RoomTariff
                {
                    RoomId = 1,
                    Description = "Без питания",
                    Price = 3600
                },
                new RoomTariff
                {
                    RoomId = 1,
                    Description = "Завтрак включён",
                    Price = 4800
                },
                new RoomTariff
                {
                    RoomId = 1,
                    Description = "Полупансион",
                    Price = 6400
                },
                new RoomTariff
                {
                    RoomId = 1,
                    Description = "Включён завтрак, обед и ужин",
                    Price = 7400
                },
                new RoomTariff
                {
                    RoomId = 2,
                    Description = "Без питания",
                    Price = 3600
                },
                new RoomTariff
                {
                    RoomId = 2,
                    Description = "Завтрак включён",
                    Price = 4800
                },
                new RoomTariff
                {
                    RoomId = 2,
                    Description = "Полупансион",
                    Price = 6400
                },
                new RoomTariff
                {
                    RoomId = 2,
                    Description = "Включён завтрак, обед и ужин",
                    Price = 7400
                },
                new RoomTariff
                {
                    RoomId = 3,
                    Description = "Без питания",
                    Price = 4100
                },
                new RoomTariff
                {
                    RoomId = 3,
                    Description = "Завтрак включён",
                    Price = 5300
                },
                new RoomTariff
                {
                    RoomId = 3,
                    Description = "Полупансион",
                    Price = 6900
                },
                new RoomTariff
                {
                    RoomId = 3,
                    Description = "Включён завтрак, обед и ужин",
                    Price = 7900
                },
                new RoomTariff
                {
                    RoomId = 4,
                    Description = "Без питания",
                    Price = 4100
                },
                new RoomTariff
                {
                    RoomId = 4,
                    Description = "Завтрак включён",
                    Price = 5300
                },
                new RoomTariff
                {
                    RoomId = 4,
                    Description = "Полупансион",
                    Price = 6900
                },
                new RoomTariff
                {
                    RoomId = 4,
                    Description = "Включён завтрак, обед и ужин",
                    Price = 7900
                },
                new RoomTariff
                {
                    RoomId = 5,
                    Description = "Без питания",
                    Price = 10300
                },
                new RoomTariff
                {
                    RoomId = 5,
                    Description = "Завтрак включён",
                    Price = 11400
                },
                new RoomTariff
                {
                    RoomId = 5,
                    Description = "Полупансион",
                    Price = 12700
                },
                new RoomTariff
                {
                    RoomId = 5,
                    Description = "Включён завтрак, обед и ужин",
                    Price = 13700
                },
                new RoomTariff
                {
                    RoomId = 6,
                    Description = "Без питания",
                    Price = 12300
                },
                new RoomTariff
                {
                    RoomId = 6,
                    Description = "Завтрак включён",
                    Price = 13400
                },
                new RoomTariff
                {
                    RoomId = 6,
                    Description = "Полупансион",
                    Price = 14700
                },
                new RoomTariff
                {
                    RoomId = 6,
                    Description = "Включён завтрак, обед и ужин",
                    Price = 15700
                });*/
        }
    }
}
