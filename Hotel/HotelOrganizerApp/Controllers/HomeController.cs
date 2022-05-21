using HotelContracts.BindingModels;
using HotelContracts.ViewModels;
using HotelOrganizerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HotelOrganizerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (Program.Organizer == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.Organizer == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.Organizer);
        }

        [HttpPost]
        public void Privacy(string login, string password, string fio, string phone)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password)  && !string.IsNullOrEmpty(fio) && !string.IsNullOrEmpty(phone))
            {
                APIOrganizer.PostRequest("api/organizer/updatedata", new
                OrganizerBindingModel
                {
                    Id = Program.Organizer.Id,
                    FullName = fio,
                    Login = login,
                    Password = password,
                    PhoneNumber = phone
                });
                Program.Organizer.FullName = fio;
                Program.Organizer.Login = login;
                Program.Organizer.Password = password;
                Program.Organizer.PhoneNumber = phone;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль, ФИО и номер телефона");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string login, string password)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                Program.Organizer = APIOrganizer.GetRequest<OrganizerViewModel>($"api/organizer/login?login={login}&password={password}");
                if (Program.Organizer == null)
                {
                    throw new Exception("Неверный логин/пароль");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин и пароль");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void Register(string login, string password, string fio, string phone)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(fio) && !string.IsNullOrEmpty(phone))
            {
                APIOrganizer.PostRequest("api/organizer/register", new OrganizerBindingModel
                {
                    FullName = fio,
                    Login = login,
                    Password = password,
                    PhoneNumber = phone
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите логин, пароль, ФИО и номер телефона");
        }

        [HttpGet]
        public IActionResult Conferences()
        {
            if (Program.Organizer == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIOrganizer.GetRequest<List<ConferenceViewModel>>($"api/main/getconferences?organizerId={Program.Organizer.Id}"));
        }

        [HttpGet]
        public IActionResult Conference()
        {
            return View();
        }

        [HttpPost]
        public void Conference(string name, DateTime date)
        {
            APIOrganizer.PostRequest("api/main/createconference", new CreateConferenceBindingModel
            {
                OrganizerId = (int)Program.Organizer.Id,
                Name = name,
                DateOf = date
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Seminars()
        {
            if (Program.Organizer == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIOrganizer.GetRequest<List<SeminarViewModel>>($"api/main/getseminars?organizerId={Program.Organizer.Id}"));
        }

        [HttpGet]
        public IActionResult Seminar()
        {
            return View();
        }

        [HttpPost]
        public void Seminar(string name, string area)
        {
            APIOrganizer.PostRequest("api/main/createseminar", new SeminarBindingModel
            {
                OrganizerId = (int)Program.Organizer.Id,
                Name = name,
                SubjectArea = area
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Participants()
        {
            if (Program.Organizer == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIOrganizer.GetRequest<List<ParticipantViewModel>>($"api/main/getparticipants?organizerId={Program.Organizer.Id}"));
        }

        [HttpGet]
        public IActionResult Participant()
        {
            ViewBag.Seminars = APIOrganizer.GetRequest<List<SeminarViewModel>>("api/main/getseminarlist");
            return View();
        }


        [HttpPost]
        public void Participant(string fio, string status, int seminar)
        {
            APIOrganizer.PostRequest("api/main/createparticipant", new ParticipantBindingModel
            {
                OrganizerId = (int)Program.Organizer.Id,
                FullName = fio,
                Status = status,
                SeminarId = seminar
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult LinkSeminarConference()
        {
            ViewBag.Seminars = APIOrganizer.GetRequest<List<SeminarViewModel>>("api/main/getseminarlist");
            ViewBag.Conferences = APIOrganizer.GetRequest<List<ConferenceViewModel>>("api/main/getconferencelist");
            return View();
        }

        [HttpPost]
        public void LinkSeminarConference(int seminar)
        {
            Response.Redirect("Index");
        }
    }
}
