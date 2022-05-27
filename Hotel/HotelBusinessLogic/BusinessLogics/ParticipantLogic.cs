using System;
using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;

namespace HotelBusinessLogic.BusinessLogics
{
    public class ParticipantLogic : IParticipantLogic
    {
        private readonly IParticipantStorage _participantStorage;

        public ParticipantLogic(IParticipantStorage participantStorage)
        {
            _participantStorage = participantStorage;
        }

        public List<ParticipantViewModel> Read(ParticipantBindingModel model)
        {
            if (model == null)
            {
                return _participantStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ParticipantViewModel> { _participantStorage.GetElement(model) };
            }
            return _participantStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ParticipantBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _participantStorage.Update(model);
            }
            else
            {
                _participantStorage.Insert(model);
            }
        }

        public void Delete(ParticipantBindingModel model)
        {
            var element = _participantStorage.GetElement(new ParticipantBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Участник не найден");
            }
            _participantStorage.Delete(model);
        }
    }
}
