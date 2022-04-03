using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDatebaseImplement.Models
{
    public class Seminar
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SubjectArea { get; set; }

        public int OrganizerId { get; set; }

        public virtual Organizer Organizer { get; set; }

        [ForeignKey("SeminarId")]
        public virtual List<Participant> Participants { get; set; }

        [ForeignKey("SeminarId")]
        public virtual List<ConferenceSeminar> ConferenceSeminars { get; set; }

        [ForeignKey("SeminarId")]
        public virtual List<LunchSeminar> LunchSeminars { get; set; }
    }
}
