using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace HotelRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly IOrganizerLogic _organizerLogic;

        public OrganizerController(IOrganizerLogic organizerLogic)
        {
            _organizerLogic = organizerLogic;
        }

        [HttpGet]
        public OrganizerViewModel Login(string login, string password)
        {
            var list = _organizerLogic.Read(new OrganizerBindingModel
            {
                Login = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpPost]
        public void Register(OrganizerBindingModel model) => _organizerLogic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(OrganizerBindingModel model) => _organizerLogic.CreateOrUpdate(model);
    }
}
