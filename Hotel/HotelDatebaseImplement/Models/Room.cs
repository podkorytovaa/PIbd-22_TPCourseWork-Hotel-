using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDatebaseImplement.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public int Level { get; set; }

        [ForeignKey("RoomId")]
        public virtual List<ConferenceRoom> ConferenceRooms { get; set; }

        [ForeignKey("RoomId")]
        public virtual List<RoomLunch> RoomLunches { get; set; }

        [ForeignKey("RoomId")]
        public virtual List<Roomer> Roomers { get; set; }

        public int HeadwaiterId { get; set; }
        public virtual Headwaiter Headwaiter { get; set; }
    }
}
