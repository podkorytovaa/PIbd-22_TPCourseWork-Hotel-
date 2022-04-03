using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;


namespace HotelBusinessLogic.BusinessLogics
{
    public class LunchLogic : ILunchLogic
    {
        private readonly ILunchStorage _lunchStorage;

        public LunchLogic(ILunchStorage lunchStorage)
        {
            _lunchStorage = lunchStorage;
        }

        public List<LunchViewModel> Read(LunchBindingModel model)
        {
            if (model == null)
            {
                return _lunchStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<LunchViewModel> { _lunchStorage.GetElement(model) };
            }
            return _lunchStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(LunchBindingModel model)
        {
            var element = _lunchStorage.GetElement(new LunchBindingModel
            {
                Name = model.Name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть обед с таким названием");
            }
            if (model.Id.HasValue)
            {
                _lunchStorage.Update(model);
            }
            else
            {
                _lunchStorage.Insert(model);
            }
        }

        public void Delete(LunchBindingModel model)
        {
            var element = _lunchStorage.GetElement(new LunchBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _lunchStorage.Delete(model);
        }
    }
}
