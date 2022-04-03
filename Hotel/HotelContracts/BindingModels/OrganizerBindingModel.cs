namespace HotelContracts.BindingModels
{
    public class OrganizerBindingModel
    {
        public int? Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
