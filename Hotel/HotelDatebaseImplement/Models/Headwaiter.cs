using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDatebaseImplement.Models
{
    public class Headwaiter
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        //эл.почта
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [ForeignKey("HeadwaiterId")]
        public virtual List<Lunch> Lunches { get; set; }

        [ForeignKey("HeadwaiterId")]
        public virtual List<Room> Rooms { get; set; }

        [ForeignKey("HeadwaiterId")]
        public virtual List<Roomer> Roomers { get; set; }
    }
}
