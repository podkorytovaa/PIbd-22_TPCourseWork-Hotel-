using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDatebaseImplement.Models
{
    public class Lunch
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Dish { get; set; }

        [Required]
        public string Drink { get; set; }


        [ForeignKey("LunchId")]
        public virtual List<LunchSeminar> LunchSeminars { get; set; }

        [ForeignKey("LunchId")]
        public virtual List<RoomLunch> RoomLunches { get; set; }

        public int HeadwaiterId { get; set; }
        public virtual Headwaiter Headwaiter { get; set; }

    }
}
