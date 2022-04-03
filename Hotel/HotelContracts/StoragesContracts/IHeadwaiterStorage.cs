using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.StoragesContracts
{
    public interface IHeadwaiterStorage
    {
        List<HeadwaiterViewModel> GetFullList();
        List<HeadwaiterViewModel> GetFilteredList(HeadwaiterBindingModel model);
        HeadwaiterViewModel GetElement(HeadwaiterBindingModel model);
        void Insert(HeadwaiterBindingModel model);
        void Update(HeadwaiterBindingModel model);
        void Delete(HeadwaiterBindingModel model);
    }
}
