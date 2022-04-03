using HotelDatebaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelDatebaseImplement
{
    public class HotelDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=HotelDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Organizer> Organizers { set; get; }
        public virtual DbSet<Conference> Conferences { set; get; }
        public virtual DbSet<Seminar> Seminars { set; get; }
        public virtual DbSet<Participant> Participants { set; get; }
        public virtual DbSet<Headwaiter> Headwaiters { set; get; }
        public virtual DbSet<Lunch> Lunches { set; get; }
        public virtual DbSet<Room> Rooms { set; get; }
        public virtual DbSet<Roomer> Roomers { set; get; }
        public virtual DbSet<ConferenceSeminar> ConferenceSeminars { set; get; }
        public virtual DbSet<ConferenceRoom> ConferenceRooms { set; get; }
        public virtual DbSet<LunchSeminar> LunchSeminars { set; get; }
        public virtual DbSet<RoomLunch> RoomLunches { set; get; }
    }
}
