using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.StoragesContracts
{
    public interface IConferenceStorage
    {
        List<ConferenceViewModel> GetFullList();

        List<ConferenceViewModel> GetFilteredList(ConferenceBindingModel model);

        ConferenceViewModel GetElement(ConferenceBindingModel model);

        void Insert(ConferenceBindingModel model);

        void Update(ConferenceBindingModel model);

        void Delete(ConferenceBindingModel model);
    }
}
