using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface IConferenceLogic
    {
        List<ConferenceViewModel> Read(ConferenceBindingModel model);

        //void CreateOrUpdate(CreateConferenceBindingModel model);

        void CreateConference(CreateConferenceBindingModel model);

        void Update(ConferenceBindingModel model);

        void Delete(ConferenceBindingModel model);
    }
}
