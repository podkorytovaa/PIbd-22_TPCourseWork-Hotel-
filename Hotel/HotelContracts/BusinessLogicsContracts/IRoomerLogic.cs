using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface IRoomerLogic
    {
        List<RoomerViewModel> Read(RoomerBindingModel model);
        void CreateOrUpdate(RoomerBindingModel model);
        void Delete(RoomerBindingModel model);
    }
}
