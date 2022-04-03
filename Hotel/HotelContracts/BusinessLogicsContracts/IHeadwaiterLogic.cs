using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface IHeadwaiterLogic
    {
        List<HeadwaiterViewModel> Read(HeadwaiterBindingModel model);
        void CreateOrUpdate(HeadwaiterBindingModel model);
        void Delete(HeadwaiterBindingModel model);
    }
}
