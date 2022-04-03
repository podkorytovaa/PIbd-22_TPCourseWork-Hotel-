using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.StoragesContracts
{
    public interface IRoomStorage
    {
        List<RoomViewModel> GetFullList();
        List<RoomViewModel> GetFilteredList(RoomBindingModel model);
        RoomViewModel GetElement(RoomBindingModel model);
        void Insert(RoomBindingModel model);
        void Update(RoomBindingModel model);
        void Delete(RoomBindingModel model);
    }
}
