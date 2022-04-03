using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.StoragesContracts
{
    public interface IRoomerStorage
    {
        List<RoomerViewModel> GetFullList();
        List<RoomerViewModel> GetFilteredList(RoomerBindingModel model);
        RoomerViewModel GetElement(RoomerBindingModel model);
        void Insert(RoomerBindingModel model);
        void Update(RoomerBindingModel model);
        void Delete(RoomerBindingModel model);
    }
}
