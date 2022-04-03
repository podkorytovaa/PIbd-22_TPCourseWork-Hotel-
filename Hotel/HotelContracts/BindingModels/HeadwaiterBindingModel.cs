using System;
using System.Collections.Generic;
using System.Text;

namespace HotelContracts.BindingModels
{
    public class HeadwaiterBindingModel
    {
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
