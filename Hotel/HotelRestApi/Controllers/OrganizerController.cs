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
        private readonly IOrganizerLogic _logic;

        public OrganizerController(IOrganizerLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public OrganizerViewModel Login(string login, string password)
        {
            var list = _logic.Read(new OrganizerBindingModel
            {
                Login = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpPost]
        public void Register(OrganizerBindingModel model) => _logic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(OrganizerBindingModel model) => _logic.CreateOrUpdate(model);
    }
}
