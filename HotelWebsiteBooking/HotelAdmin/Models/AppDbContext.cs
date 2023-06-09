﻿using HotelAdmin.Models.Entity;
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
        public DbSet<Category> Categorys { get; set; }
        public DbSet<RoomDate> Dates { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPayable> OrderPayables { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RoomTariff> Tariffs { get; set; }
        public DbSet<RoomTariffAdmin> TariffAdmins { get; set; }
        public DbSet<TariffPlan> TariffPlans { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<RoomTariff>()
                .HasKey(r => r.RoomId);*/

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Email = "admin@mail.ru",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin")

                });

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Photo = "img/room/room-s.jpeg",
                    Name = "Стандарт",
                    Square = "20 кв.м",
                    PersonsCount = 2
                },
                new Category
                {
                    Id = 2,
                    Photo = "img/room/room-sb.jpg",
                    Name = "Стандарт с большой кроватью",
                    Square = "20 кв.м",
                    PersonsCount = 2
                },
                new Category
                {
                    Id = 3,
                    Photo = "img/room/room-si.jpeg",
                    Name = "Стандарт Улучшенный",
                    Square = "25 кв.м",
                    PersonsCount = 2
                },
                new Category
                {
                    Id = 4,
                    Photo = "img/room/room-sbi.jpeg",
                    Name = "Стандарт Улучшенный с большой кроватью",
                    Square = "25 кв.м",
                    PersonsCount = 2
                },
                new Category
                {
                    Id = 5,
                    Photo = "img/room/room-pl.jpg",
                    Name = "Полулюкс",
                    Square = "32 кв.м",
                    PersonsCount = 4
                },
                new Category
                {
                    Id = 6,
                    Photo = "img/room/room-l.jpg",
                    Name = "Люкс",
                    Square = "46 кв.м",
                    PersonsCount = 4
                });

            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    Number = 204,
                    CategoryId = 1
                },
                new Room
                {
                    Id = 2,
                    Number = 307,
                    CategoryId = 2
                },
                new Room
                {
                    Id = 3,
                    Number = 405,
                    CategoryId = 3
                },
                new Room
                {
                    Id = 4,
                    Number = 412,
                    CategoryId = 4
                },
                new Room
                {
                    Id = 5,
                    Number = 514,
                    CategoryId = 5
                },
                new Room
                {
                    Id = 6,
                    Number = 618,
                    CategoryId = 6
                });

            modelBuilder.Entity<RoomDate>().HasData(
                new RoomDate { Id = 1, RoomId = 1 },
                new RoomDate { Id = 2, RoomId = 2 },
                new RoomDate { Id = 3, RoomId = 3 },
                new RoomDate { Id = 4, RoomId = 4 },
                new RoomDate { Id = 5, RoomId = 5 },
                new RoomDate { Id = 6, RoomId = 6 }
                );
            
            modelBuilder.Entity<TariffPlan>().HasData(
                new TariffPlan { Id = 1, Description = "Без питания" },
                new TariffPlan { Id = 2, Description = "Завтрак включён" },
                new TariffPlan { Id = 3, Description = "Полупансион" },
                new TariffPlan { Id = 4, Description = "Включён завтрак, обед и ужин" }
                );

            modelBuilder.Entity<RoomTariffAdmin>().HasData(
                new RoomTariffAdmin
                {
                    Id = 1,
                    CategoryId = 1,
                    TariffPlanId = 1,
                    Price = 3600
                },
                new RoomTariffAdmin
                {
                    Id = 2,
                    CategoryId = 1,
                    TariffPlanId = 2,
                    Price = 4800
                },
                new RoomTariffAdmin
                {
                    Id = 3,
                    CategoryId = 1,
                    TariffPlanId = 3,
                    Price = 6400
                },
                new RoomTariffAdmin
                {
                    Id = 4,
                    CategoryId = 1,
                    TariffPlanId = 4,
                    Price = 7400
                },
                new RoomTariffAdmin
                {
                    Id = 5,
                    CategoryId = 2,
                    TariffPlanId = 1,
                    Price = 3600
                },
                new RoomTariffAdmin
                {
                    Id = 6,
                    CategoryId = 2,
                    TariffPlanId = 2,
                    Price = 4800
                },
                new RoomTariffAdmin
                {
                    Id = 7,
                    CategoryId = 2,
                    TariffPlanId = 3,
                    Price = 6400
                },
                new RoomTariffAdmin
                {
                    Id = 8,
                    CategoryId = 2,
                    TariffPlanId = 4,
                    Price = 7400
                },
                new RoomTariffAdmin
                {
                    Id = 9,
                    CategoryId = 3,
                    TariffPlanId = 1,
                    Price = 4100
                },
                new RoomTariffAdmin
                {
                    Id = 10,
                    CategoryId = 3,
                    TariffPlanId = 2,
                    Price = 5300
                },
                new RoomTariffAdmin
                {
                    Id = 11,
                    CategoryId = 3,
                    TariffPlanId = 3,
                    Price = 6900
                },
                new RoomTariffAdmin
                {
                    Id = 12,
                    CategoryId = 3,
                    TariffPlanId = 4,
                    Price = 7900
                },
                new RoomTariffAdmin
                {
                    Id = 13,
                    CategoryId = 4,
                    TariffPlanId = 1,
                    Price = 4100
                },
                new RoomTariffAdmin
                {
                    Id = 14,
                    CategoryId = 4,
                    TariffPlanId = 2,
                    Price = 5300
                },
                new RoomTariffAdmin
                {
                    Id = 15,
                    CategoryId = 4,
                    TariffPlanId = 3,
                    Price = 6900
                },
                new RoomTariffAdmin
                {
                    Id = 16,
                    CategoryId = 4,
                    TariffPlanId = 4,
                    Price = 7900
                },
                new RoomTariffAdmin
                {
                    Id = 17,
                    CategoryId = 5,
                    TariffPlanId = 1,
                    Price = 10300
                },
                new RoomTariffAdmin
                {
                    Id = 18,
                    CategoryId = 5,
                    TariffPlanId = 2,
                    Price = 11400
                },
                new RoomTariffAdmin
                {
                    Id = 19,
                    CategoryId = 5,
                    TariffPlanId = 3,
                    Price = 12700
                },
                new RoomTariffAdmin
                {
                    Id = 20,
                    CategoryId = 5,
                    TariffPlanId = 4,
                    Price = 13700
                },
                new RoomTariffAdmin
                {
                    Id = 21,
                    CategoryId = 6,
                    TariffPlanId = 1,
                    Price = 12300
                },
                new RoomTariffAdmin
                {
                    Id = 22,
                    CategoryId = 6,
                    TariffPlanId = 2,
                    Price = 13400
                },
                new RoomTariffAdmin
                {
                    Id = 23,
                    CategoryId = 6,
                    TariffPlanId = 3,
                    Price = 14700
                },
                new RoomTariffAdmin
                {
                    Id = 24,
                    CategoryId = 6,
                    TariffPlanId = 4,
                    Price = 15700
                });


            modelBuilder.Entity<RoomTariff>().HasData(
                new RoomTariff
                {
                    Id = 1,
                    RoomId = 1,
                    TariffPlanId = 1,
                    Price = 3600
                },
                new RoomTariff
                {
                    Id = 2,
                    RoomId = 1,
                    TariffPlanId = 2,
                    Price = 4800
                },
                new RoomTariff
                {
                    Id = 3,
                    RoomId = 1,
                    TariffPlanId = 3,
                    Price = 6400
                },
                new RoomTariff
                {
                    Id = 4,
                    RoomId = 1,
                    TariffPlanId = 4,
                    Price = 7400
                },
                new RoomTariff
                {
                    Id = 5,
                    RoomId = 2,
                    TariffPlanId = 1,
                    Price = 3600
                },
                new RoomTariff
                {
                    Id = 6,
                    RoomId = 2,
                    TariffPlanId = 2,
                    Price = 4800
                },
                new RoomTariff
                {
                    Id = 7,
                    RoomId = 2,
                    TariffPlanId = 3,
                    Price = 6400
                },
                new RoomTariff
                {
                    Id = 8,
                    RoomId = 2,
                    TariffPlanId = 4,
                    Price = 7400
                },
                new RoomTariff
                {
                    Id = 9,
                    RoomId = 3,
                    TariffPlanId = 1,
                    Price = 4100
                },
                new RoomTariff
                {
                    Id = 10,
                    RoomId = 3,
                    TariffPlanId = 2,
                    Price = 5300
                },
                new RoomTariff
                {
                    Id = 11,
                    RoomId = 3,
                    TariffPlanId = 3,
                    Price = 6900
                },
                new RoomTariff
                {
                    Id = 12,
                    RoomId = 3,
                    TariffPlanId = 4,
                    Price = 7900
                },
                new RoomTariff
                {
                    Id = 13,
                    RoomId = 4,
                    TariffPlanId = 1,
                    Price = 4100
                },
                new RoomTariff
                {
                    Id = 14,
                    RoomId = 4,
                    TariffPlanId = 2,
                    Price = 5300
                },
                new RoomTariff
                {
                    Id = 15,
                    RoomId = 4,
                    TariffPlanId = 3,
                    Price = 6900
                },
                new RoomTariff
                {
                    Id = 16,
                    RoomId = 4,
                    TariffPlanId = 4,
                    Price = 7900
                },
                new RoomTariff
                {
                    Id = 17,
                    RoomId = 5,
                    TariffPlanId = 1,
                    Price = 10300
                },
                new RoomTariff
                {
                    Id = 18,
                    RoomId = 5,
                    TariffPlanId = 2,
                    Price = 11400
                },
                new RoomTariff
                {
                    Id = 19,
                    RoomId = 5,
                    TariffPlanId = 3,
                    Price = 12700
                },
                new RoomTariff
                {
                    Id = 20,
                    RoomId = 5,
                    TariffPlanId = 4,
                    Price = 13700
                },
                new RoomTariff
                {
                    Id = 21,
                    RoomId = 6,
                    TariffPlanId = 1,
                    Price = 12300
                },
                new RoomTariff
                {
                    Id = 22,
                    RoomId = 6,
                    TariffPlanId = 2,
                    Price = 13400
                },
                new RoomTariff
                {
                    Id = 23,
                    RoomId = 6,
                    TariffPlanId = 3,
                    Price = 14700
                },
                new RoomTariff
                {
                    Id = 24,
                    RoomId = 6,
                    TariffPlanId = 4,
                    Price = 15700
                });
        }
    }
}
