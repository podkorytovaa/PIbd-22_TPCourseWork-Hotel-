namespace HotelDatebaseImplement.Models
{
    public class ConferenceRoom
    {
        public int Id { get; set; }

        public int ConferenceId { get; set; }

        public int RoomId { get; set; }

        public virtual Conference Conference { get; set; }

        public virtual Room Room { get; set; }
    }
}
