using System;
using System.ComponentModel.DataAnnotations;

namespace HotelDatebaseImplement.Models
{
    public class Roomer
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateBooking { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public int HeadwaiterId { get; set; }
        public virtual Headwaiter Headwaiter { get; set; }       
    }
}
