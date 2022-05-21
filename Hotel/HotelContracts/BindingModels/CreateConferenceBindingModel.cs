using System;

namespace HotelContracts.BindingModels
{
    // Данные от организатора, для создания конференции
    public class CreateConferenceBindingModel
    {
        public int OrganizerId { get; set; }

        public string Name { get; set; }

        public DateTime DateOf { get; set; }
    }
}
