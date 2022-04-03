using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;

namespace HotelBusinessLogic.BusinessLogics
{
    public class HeadwaiterLogic : IHeadwaiterLogic
    {
        private readonly IHeadwaiterStorage _headwaiterStorage;

        public HeadwaiterLogic(IHeadwaiterStorage headwaiterStorage)
        {
            _headwaiterStorage = headwaiterStorage;
        }

        public List<HeadwaiterViewModel> Read(HeadwaiterBindingModel model)
        {
            if (model == null)
            {
                return _headwaiterStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<HeadwaiterViewModel> { _headwaiterStorage.GetElement(model) };
            }
            return _headwaiterStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(HeadwaiterBindingModel model)
        {
            var elementByLogin = _headwaiterStorage.GetElement(new HeadwaiterBindingModel
            {
                Login = model.Login
            });
            if (elementByLogin != null && elementByLogin.Id != model.Id)
            {
                throw new Exception("Учетная запись по такому логину уже существует");
            }
            if (model.Id.HasValue)
            {
                _headwaiterStorage.Update(model);
            }
            else
            {
                _headwaiterStorage.Insert(model);
            }
        }

        public void Delete(HeadwaiterBindingModel model)
        {
            var element = _headwaiterStorage.GetElement(new HeadwaiterBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _headwaiterStorage.Delete(model);
        }
    }
}
