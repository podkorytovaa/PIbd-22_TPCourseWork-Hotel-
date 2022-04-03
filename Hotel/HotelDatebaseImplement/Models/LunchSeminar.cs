namespace HotelDatebaseImplement.Models
{
    public class LunchSeminar
    {
        public int Id { get; set; }


        public int LunchId { get; set; }
        public virtual Lunch Lunch { get; set; }


        public int SeminarId { get; set; }
        public virtual Seminar Seminar { get; set; }
    }
}
