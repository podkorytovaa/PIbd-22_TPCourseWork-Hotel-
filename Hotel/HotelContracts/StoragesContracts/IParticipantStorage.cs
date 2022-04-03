using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.StoragesContracts
{
    public interface IParticipantStorage
    {
        List<ParticipantViewModel> GetFullList();

        List<ParticipantViewModel> GetFilteredList(ParticipantBindingModel model);

        ParticipantViewModel GetElement(ParticipantBindingModel model);

        void Insert(ParticipantBindingModel model);

        void Update(ParticipantBindingModel model);

        void Delete(ParticipantBindingModel model);
    }
}
