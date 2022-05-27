using System;
using System.Collections.Generic;
using System.Text;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using System.Text.RegularExpressions;

namespace HotelBusinessLogic.BusinessLogics
{
    public class HeadwaiterLogic : IHeadwaiterLogic
    {
        private readonly IHeadwaiterStorage _headwaiterStorage;
        private readonly int _passwordMaxLength = 50;
        private readonly int _passwordMinLength = 8;

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
            if (!Regex.IsMatch(model.Login, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
            {
                throw new Exception("В качестве логина должна быть указана почта");
            }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length < _passwordMinLength
                || !Regex.IsMatch(model.Password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль длиной от {_passwordMinLength} до { _passwordMaxLength } должен состоять из цифр, букв и небуквенных символов");
            }
            if (!Regex.IsMatch(model.PhoneNumber, @"^((\+7|7|8)+([0-9]){10})$"))
            {
                throw new Exception("Номер телефона введен неверно");
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
