using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.StoragesContracts
{
    public interface ILunchStorage
    {
        List<LunchViewModel> GetFullList();
        List<LunchViewModel> GetFilteredList(LunchBindingModel model);
        LunchViewModel GetElement(LunchBindingModel model);
        void Insert(LunchBindingModel model);
        void Update(LunchBindingModel model);
        void Delete(LunchBindingModel model);
    }
}
