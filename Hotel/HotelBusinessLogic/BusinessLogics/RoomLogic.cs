using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;

namespace HotelBusinessLogic.BusinessLogics
{
    public class RoomLogic : IRoomLogic
    {
        private readonly IRoomStorage _roomStorage;

        public RoomLogic(IRoomStorage roomStorage)
        {
            _roomStorage = roomStorage;
        }

        public List<RoomViewModel> Read(RoomBindingModel model)
        {
            if (model == null)
            {
                return _roomStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<RoomViewModel> { _roomStorage.GetElement(model) };
            }
            return _roomStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(RoomBindingModel model)
        {
            var element = _roomStorage.GetElement(new RoomBindingModel
            {
                Number = model.Number
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Данный номер уже занят");
            }
            if (model.Id.HasValue)
            {
                _roomStorage.Update(model);
            }
            else
            {
                _roomStorage.Insert(model);
            }
        }

        public void Delete(RoomBindingModel model)
        {
            var element = _roomStorage.GetElement(new RoomBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _roomStorage.Delete(model);
        }
    }
}
