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
    public class SeminarController : ControllerBase
    {
        private readonly ISeminarLogic _seminarLogic;

        public SeminarController(ISeminarLogic seminarLogic)
        {
            _seminarLogic = seminarLogic;
        }

        [HttpGet]
        public List<SeminarViewModel> GetSeminarList() => _seminarLogic.Read(null)?.ToList();

        [HttpGet]
        public List<SeminarViewModel> GetOrganizerSeminars(int organizerId) => _seminarLogic.Read(new SeminarBindingModel { OrganizerId = organizerId });

        [HttpGet]
        public SeminarViewModel GetSeminar(int seminarId) => _seminarLogic.Read(new SeminarBindingModel { Id = seminarId })?[0];

        [HttpPost]
        public void CreateOrUpdateSeminar(SeminarBindingModel model) => _seminarLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteSeminar(SeminarBindingModel model) => _seminarLogic.Delete(model);

        [HttpPost]
        public void LinkSeminarToConferences(LinkSeminarToConferencesBindingModel model) => _seminarLogic.LinkConferences(model);

    }
}
