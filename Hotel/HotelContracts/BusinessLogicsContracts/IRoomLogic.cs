using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface IRoomLogic
    {
        List<RoomViewModel> Read(RoomBindingModel model);
        void CreateOrUpdate(RoomBindingModel model);
        void Delete(RoomBindingModel model);
    }
}
