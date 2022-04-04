using System.Collections.Generic;
using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class SeminarViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название семинара")]
        public string Name { get; set; }

        [DisplayName("Предметная область")]
        public string SubjectArea { get; set; }

        public int OrganizerId { get; set; }
    }
}
