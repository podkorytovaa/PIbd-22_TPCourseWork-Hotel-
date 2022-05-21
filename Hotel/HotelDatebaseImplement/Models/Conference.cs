using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDatebaseImplement.Models
{
    public class Conference
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateOf { get; set; }

        public int OrganizerId { get; set; }

        public virtual Organizer Organizer { get; set; }

        [ForeignKey("ConferenceId")]
        public virtual List<ConferenceSeminar> ConferenceSeminars { get; set; }

        [ForeignKey("ConferenceId")]
        public virtual List<ConferenceRoom> ConferenceRooms { get; set; }
    }
}
