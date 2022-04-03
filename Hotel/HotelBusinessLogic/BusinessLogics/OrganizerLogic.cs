using System;
using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;

namespace HotelBusinessLogic.BusinessLogics
{
    public class OrganizerLogic : IOrganizerLogic
    {
        private readonly IOrganizerStorage _organizerStorage;

        public OrganizerLogic(IOrganizerStorage organizerStorage)
        {
            _organizerStorage = organizerStorage;
        }

        public List<OrganizerViewModel> Read(OrganizerBindingModel model)
        {
            if (model == null)
            {
                return _organizerStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrganizerViewModel> { _organizerStorage.GetElement(model) };
            }
            return _organizerStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(OrganizerBindingModel model)
        {
            var elementByLogin = _organizerStorage.GetElement(new OrganizerBindingModel { Login = model.Login  });
            if (elementByLogin != null && elementByLogin.Id != model.Id)
            {
                throw new Exception("Уже есть организатор с таким логином");
            }
            if (model.Id.HasValue)
            {
                _organizerStorage.Update(model);
            }
            else
            {
                _organizerStorage.Insert(model);
            }
        }

        public void Delete(OrganizerBindingModel model)
        {
            var element = _organizerStorage.GetElement(new OrganizerBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Организатор не найден");
            }
            _organizerStorage.Delete(model);
        }
    }
}
