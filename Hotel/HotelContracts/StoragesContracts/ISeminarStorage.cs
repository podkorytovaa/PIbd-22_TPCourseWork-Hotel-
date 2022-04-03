using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.StoragesContracts
{
    public interface ISeminarStorage
    {
        List<SeminarViewModel> GetFullList();

        List<SeminarViewModel> GetFilteredList(SeminarBindingModel model);

        SeminarViewModel GetElement(SeminarBindingModel model);

        void Insert(SeminarBindingModel model);

        void Update(SeminarBindingModel model);

        void Delete(SeminarBindingModel model);
    }
}
