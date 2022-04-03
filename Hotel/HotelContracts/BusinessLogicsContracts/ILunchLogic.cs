using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface ILunchLogic
    {
        List<LunchViewModel> Read(LunchBindingModel model);
        void CreateOrUpdate(LunchBindingModel model);
        void Delete(LunchBindingModel model);
    }
}
