namespace HotelDatebaseImplement.Models
{
    public class ConferenceSeminar
    {
        public int Id { get; set; }

        public int ConferenceId { get; set; }

        public int SeminarId { get; set; }

        public virtual Conference Conference { get; set; }

        public virtual Seminar Seminar { get; set; }
    }
}
