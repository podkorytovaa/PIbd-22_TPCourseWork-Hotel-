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
    public class ConferenceController : ControllerBase
    {
        private readonly IConferenceLogic _conferenceLogic;
        private readonly IRoomLogic _roomLogic;

        public ConferenceController(IConferenceLogic conferenceLogic, IRoomLogic roomLogic)
        {
            _conferenceLogic = conferenceLogic;
            _roomLogic = roomLogic;
        }

        [HttpGet]
        public List<ConferenceViewModel> GetConferenceList() => _conferenceLogic.Read(null)?.ToList();

        [HttpGet]
        public List<ConferenceViewModel> GetOrganizerConferences(int organizerId) => _conferenceLogic.Read(new ConferenceBindingModel { OrganizerId = organizerId });

        [HttpGet]
        public ConferenceViewModel GetConference(int conferenceId) => _conferenceLogic.Read(new ConferenceBindingModel { Id = conferenceId })?[0];

        [HttpPost]
        public void CreateOrUpdateConference(ConferenceBindingModel model) => _conferenceLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteConference(ConferenceBindingModel model) => _conferenceLogic.Delete(model);

        [HttpGet]
        public List<RoomViewModel> GetRoomList() => _roomLogic.Read(null)?.ToList();

        [HttpGet]
        public RoomViewModel GetRoom(int roomId) => _roomLogic.Read(new RoomBindingModel { Id = roomId })?[0];
    }
}
