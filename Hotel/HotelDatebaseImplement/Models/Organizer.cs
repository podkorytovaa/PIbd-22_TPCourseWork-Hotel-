using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDatebaseImplement.Models
{
    public class Organizer
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [ForeignKey("OrganizerId")]
        public virtual List<Conference> Conferences { get; set; }

        [ForeignKey("OrganizerId")]
        public virtual List<Seminar> Seminars { get; set; }

        [ForeignKey("OrganizerId")]
        public virtual List<Participant> Participants { get; set; }
    }
}
