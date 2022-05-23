using System;
using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;

namespace HotelBusinessLogic.BusinessLogics
{
    public class SeminarLogic : ISeminarLogic
    {
        private readonly ISeminarStorage _seminarStorage;
        private readonly IConferenceStorage _conferenceStorage;

        public SeminarLogic(ISeminarStorage seminarStorage, IConferenceStorage conferenceStorage)
        {
            _seminarStorage = seminarStorage;
            _conferenceStorage = conferenceStorage;
        }

        public List<SeminarViewModel> Read(SeminarBindingModel model)
        {
            if (model == null)
            {
                return _seminarStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<SeminarViewModel> { _seminarStorage.GetElement(model) };
            }
            return _seminarStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(SeminarBindingModel model)
        {
            var element = _seminarStorage.GetElement(new SeminarBindingModel { Name = model.Name });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть семинар с таким названием");
            }
            if (model.Id.HasValue)
            {
                _seminarStorage.Update(model);
            }
            else
            {
                _seminarStorage.Insert(model);
            }
        }

        public void Delete(SeminarBindingModel model)
        {
            var element = _seminarStorage.GetElement(new SeminarBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Семинар не найден");
            }
            _seminarStorage.Delete(model);
        }

        public void LinkConferences(LinkSeminarToConferencesBindingModel model)
        {
            var seminar = _seminarStorage.GetElement(new SeminarBindingModel { Id = model.SeminarId });
            if (seminar == null)
            {
                throw new Exception("Семинар не найден");
            }
            foreach (var conferenceId in model.ConferenceId)
            {
                var conference = _conferenceStorage.GetElement(new ConferenceBindingModel { Id = conferenceId });
                if (conference == null)
                {
                    throw new Exception("Конференция не найдена");
                }
                if (!seminar.SeminarConferences.ContainsKey(conferenceId))
                {
                    seminar.SeminarConferences.Add(conferenceId, conference.Name);
                }
            }
            _seminarStorage.Update(new SeminarBindingModel
            {
                Id = seminar.Id,
                OrganizerId = seminar.OrganizerId,
                Name = seminar.Name,
                SubjectArea = seminar.SubjectArea,
                SeminarConferences = seminar.SeminarConferences
            });
        }
    }
}
