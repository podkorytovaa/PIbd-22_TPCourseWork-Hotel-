using System;
using System.Collections.Generic;
using System.Text;

namespace HotelDatebaseImplement.Models
{
    public class RoomLunch
    {
        public int Id { get; set; }


        public int RoomId { get; set; }
        public virtual Room Room { get; set; }


        public int LunchId { get; set; }
        public virtual Lunch Lunch { get; set; }
    }
}
