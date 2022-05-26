using System;
using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using System.Text.RegularExpressions;

namespace HotelBusinessLogic.BusinessLogics
{
    public class OrganizerLogic : IOrganizerLogic
    {
        private readonly IOrganizerStorage _organizerStorage;
        private readonly int _passwordMinLength = 8; 
        private readonly int _passwordMaxLength = 50;

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
            if (!Regex.IsMatch(model.Login, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
            {
                throw new Exception("В качестве логина должна быть указана почта");
            }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length < _passwordMinLength || !Regex.IsMatch(model.Password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль доолжен иметь длину от {_passwordMinLength} до { _passwordMaxLength } и содержать цифры, буквы и небуквенные символы");
            }

            if (!Regex.IsMatch(model.PhoneNumber, @"^((\+7|7|8)+([0-9]){10})$"))
            {
                throw new Exception("Номер телефона введен неверно");
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
