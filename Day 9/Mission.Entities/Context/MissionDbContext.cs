﻿using Mission.Entities;
using Microsoft.EntityFrameworkCore;
using Mission.Entities.Entities;

namespace Mission.Entities.Context
{
    public class MissionDbContext(DbContextOptions<MissionDbContext> options) : DbContext(options)
    {
        public DbSet<User> User { get; set; }

        public DbSet<MissionTheme> MissionThemes { get; set; }

        public DbSet<MissionSkill> MissionSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            builder.Entity<User>().HasData(new User()
            {
                Id = 1,
                FirstName = "Hitarth",
                LastName = "Soni",
                EmailAddress = "hitarthsoni947@gmail.com",
                UserType = "admin",
                Password = "root",
                PhoneNumber = "9016531077",
                CreatedDate = new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc)   ,
                IsDeleted = false
            });

            base.OnModelCreating(builder);
        }
    }
}
