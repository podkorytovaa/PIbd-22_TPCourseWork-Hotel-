using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class ParticipantViewModel
    {
        public int Id { get; set; }

        [DisplayName("ФИО участника")]
        public string FullName { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        public int SeminarId { get; set; }
       
        [DisplayName("Название семинара")]
        public string SeminarName { get; set; }

        public int OrganizerId { get; set; }
    }
}
