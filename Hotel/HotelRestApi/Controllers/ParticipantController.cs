using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HotelRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantLogic _participantLogic;

        public ParticipantController(IParticipantLogic participantLogic)
        {
            _participantLogic = participantLogic;
        }

        [HttpGet]
        public List<ParticipantViewModel> GetParticipantList() => _participantLogic.Read(null)?.ToList();

        [HttpGet]
        public List<ParticipantViewModel> GetOrganizerParticipants(int organizerId) => _participantLogic.Read(new ParticipantBindingModel { OrganizerId = organizerId });

        [HttpGet]
        public ParticipantViewModel GetParticipant(int participantId) => _participantLogic.Read(new ParticipantBindingModel { Id = participantId })?[0];

        [HttpPost]
        public void CreateOrUpdateParticipant(ParticipantBindingModel model) => _participantLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteParticipant(ParticipantBindingModel model) => _participantLogic.Delete(model);
    }
}
