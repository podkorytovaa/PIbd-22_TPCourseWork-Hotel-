using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;

namespace HotelBusinessLogic.BusinessLogics
{
    public class RoomerLogic : IRoomerLogic
    {
        private readonly IRoomerStorage _roomerStorage;

        public RoomerLogic(IRoomerStorage roomerStorage)
        {
            _roomerStorage = roomerStorage;
        }

        public List<RoomerViewModel> Read(RoomerBindingModel model)
        {
            if (model == null)
            {
                return _roomerStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<RoomerViewModel> { _roomerStorage.GetElement(model) };
            }
            return _roomerStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(RoomerBindingModel model)
        { 
            var element = _roomerStorage.GetElement(new RoomerBindingModel
            {
                FullName = model.FullName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Постоялец с таким именем уже есть");
            }
            if (model.Id.HasValue)
            {
                _roomerStorage.Update(model);
            }
            else
            {
                _roomerStorage.Insert(model);
            }
        }

        public void Delete(RoomerBindingModel model)
        {
            var element = _roomerStorage.GetElement(new RoomerBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _roomerStorage.Delete(model);
        }
    }
}
