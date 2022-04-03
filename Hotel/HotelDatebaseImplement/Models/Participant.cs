using System.ComponentModel.DataAnnotations;

namespace HotelDatebaseImplement.Models
{
    public class Participant
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Status { get; set; }

        public int SeminarId { get; set; }

        public int OrganizerId { get; set; }

        public virtual Seminar Seminar { get; set; }

        public virtual Organizer Organizer { get; set; }
    }
}
