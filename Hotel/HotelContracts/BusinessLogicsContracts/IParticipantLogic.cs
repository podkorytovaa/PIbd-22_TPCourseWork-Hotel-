using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface IParticipantLogic
    {
        List<ParticipantViewModel> Read(ParticipantBindingModel model);

        void CreateOrUpdate(ParticipantBindingModel model);

        void Delete(ParticipantBindingModel model);
    }
}
