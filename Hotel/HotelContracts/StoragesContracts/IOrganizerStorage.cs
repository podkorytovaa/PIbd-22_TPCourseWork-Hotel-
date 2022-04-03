using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.StoragesContracts
{
    public interface IOrganizerStorage
    {
        List<OrganizerViewModel> GetFullList();

        List<OrganizerViewModel> GetFilteredList(OrganizerBindingModel model);

        OrganizerViewModel GetElement(OrganizerBindingModel model);

        void Insert(OrganizerBindingModel model);

        void Update(OrganizerBindingModel model);

        void Delete(OrganizerBindingModel model);
    }
}
