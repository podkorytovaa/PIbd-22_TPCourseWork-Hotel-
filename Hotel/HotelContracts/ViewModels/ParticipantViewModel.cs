using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class ParticipantViewModel
    {
        public int Id { get; set; }

        [DisplayName("Полное имя участника")]
        public string FullName { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        public int SeminarId { get; set; }

        public int OrganizerId { get; set; }
    }
}
