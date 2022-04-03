using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface ISeminarLogic
    {
        List<SeminarViewModel> Read(SeminarBindingModel model);

        void CreateOrUpdate(SeminarBindingModel model);

        void Delete(SeminarBindingModel model);
    }
}
