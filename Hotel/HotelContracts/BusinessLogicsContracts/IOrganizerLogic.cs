using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface IOrganizerLogic
    {
        List<OrganizerViewModel> Read(OrganizerBindingModel model);

        void CreateOrUpdate(OrganizerBindingModel model);

        void Delete(OrganizerBindingModel model);
    }
}
