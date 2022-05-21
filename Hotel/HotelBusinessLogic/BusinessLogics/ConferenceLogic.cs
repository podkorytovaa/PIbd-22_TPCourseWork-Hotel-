using System;
using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;

namespace HotelBusinessLogic.BusinessLogics
{
    public class ConferenceLogic : IConferenceLogic
    {
        private readonly IConferenceStorage _conferenceStorage;

        public ConferenceLogic(IConferenceStorage conferenceStorage)
        {
            _conferenceStorage = conferenceStorage;
        }

        public List<ConferenceViewModel> Read(ConferenceBindingModel model)
        {
            if (model == null)
            {
                return _conferenceStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ConferenceViewModel> { _conferenceStorage.GetElement(model) };
            }
            return _conferenceStorage.GetFilteredList(model);
        }

        /*public void CreateOrUpdate(ConferenceBindingModel model)
        {
            var element = _conferenceStorage.GetElement(new ConferenceBindingModel { Name = model.Name });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть конференция с таким названием");
            }
            if (model.Id.HasValue)
            {
                _conferenceStorage.Update(model);
            }
            else
            {
                _conferenceStorage.Insert(model);
            }
        }*/

        public void CreateConference(CreateConferenceBindingModel model)
        {
            /*var element = _conferenceStorage.GetElement(new ConferenceBindingModel { Name = model.Name });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть конференция с таким названием");
            }
            if (!model.Id.HasValue)
            {*/
            _conferenceStorage.Insert(new ConferenceBindingModel
                {
                    OrganizerId = model.OrganizerId,
                    Name = model.Name,
                    DateOf = model.DateOf
                });
           // }
        }

        public void Update(ConferenceBindingModel model)
        {
            var element = _conferenceStorage.GetElement(new ConferenceBindingModel { Name = model.Name });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть конференция с таким названием");
            }
            if (model.Id.HasValue)
            {
                _conferenceStorage.Update(model);
            }
        }

        public void Delete(ConferenceBindingModel model)
        {
            var element = _conferenceStorage.GetElement(new ConferenceBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Конференция не найдена");
            }
            _conferenceStorage.Delete(model);
        }
    }
}
