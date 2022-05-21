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
    public class MainController : ControllerBase
    {
        private readonly IConferenceLogic _conference;
        private readonly ISeminarLogic _seminar;
        private readonly IParticipantLogic _participant;

        public MainController(IConferenceLogic conference, ISeminarLogic seminar, IParticipantLogic participant)
        {
            _conference = conference;
            _seminar = seminar;
            _participant = participant;
        }

        [HttpGet]
        public List<ParticipantViewModel> GetParticipants(int organizerId) => _participant.Read(new ParticipantBindingModel { OrganizerId = organizerId });

        [HttpGet]
        public ParticipantViewModel GetParticipant(int participantId) => _participant.Read(new ParticipantBindingModel { Id = participantId })?[0];

        [HttpPost]
        public void CreateParticipant(ParticipantBindingModel model) => _participant.CreateOrUpdate(model);

        [HttpGet]
        public List<SeminarViewModel> GetSeminarList() => _seminar.Read(null)?.ToList();

        [HttpGet]
        public List<SeminarViewModel> GetSeminars(int organizerId) => _seminar.Read(new SeminarBindingModel { OrganizerId = organizerId });

        [HttpGet]
        public SeminarViewModel GetSeminar(int seminarId) => _seminar.Read(new SeminarBindingModel { Id = seminarId })?[0];

        [HttpPost]
        public void CreateSeminar(SeminarBindingModel model) => _seminar.CreateOrUpdate(model);

        [HttpGet]
        public List<ConferenceViewModel> GetConferenceList() => _conference.Read(null)?.ToList();

        [HttpGet]
        public List<ConferenceViewModel> GetConferences(int organizerId) => _conference.Read(new ConferenceBindingModel { OrganizerId = organizerId });

        [HttpGet]
        public ConferenceViewModel GetConference(int conferenceId) => _conference.Read(new ConferenceBindingModel { Id = conferenceId })?[0];

        [HttpPost]
        public void CreateConference(CreateConferenceBindingModel model) => _conference.CreateConference(model); // ???
    }
}
