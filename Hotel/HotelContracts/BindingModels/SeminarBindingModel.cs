using System.Collections.Generic;

namespace HotelContracts.BindingModels
{
    public class SeminarBindingModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string SubjectArea { get; set; }

        public int OrganizerId { get; set; }

        public Dictionary<int, string> SeminarConferences { get; set; }
    }
}
