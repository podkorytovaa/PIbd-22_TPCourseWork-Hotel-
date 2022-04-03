namespace HotelContracts.BindingModels
{
    public class ParticipantBindingModel
    {
        public int? Id { get; set; }

        public string FullName { get; set; }

        public string Status { get; set; }

        public int SeminarId { get; set; }

        public int OrganizerId { get; set; }
    }
}
