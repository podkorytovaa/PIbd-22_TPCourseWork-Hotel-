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

        public IActionResult Conferences()
        {
            if (Program.Organizer == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIOrganizer.GetRequest<List<ConferenceViewModel>>($"api/conference/getorganizerconferences?organizerId={Program.Organizer.Id}"));
        }

        [HttpGet]
        public IActionResult ConferenceCreate()
        {
            ViewBag.Rooms = APIOrganizer.GetRequest<List<RoomViewModel>>("api/conference/getroomlist");
            return View();
        }

        [HttpPost]
        public void ConferenceCreate(string conferenceName, DateTime conferenceDateOf, List<int> roomsId)
        {
            List<RoomViewModel> rooms = new List<RoomViewModel>();
            foreach (var roomId in roomsId)
            {
                rooms.Add(APIOrganizer.GetRequest<RoomViewModel>($"api/conference/getroom?roomId={roomId}"));
            }
            if (!string.IsNullOrEmpty(conferenceName) && !string.IsNullOrEmpty(conferenceDateOf.ToString()) && rooms != null)
            {
                APIOrganizer.PostRequest("api/conference/createorupdateconference", new ConferenceBindingModel
                {
                    OrganizerId = (int)Program.Organizer.Id,
                    Name = conferenceName,
                    DateOf = conferenceDateOf,
                    ConferenceRooms = rooms.ToDictionary(room => room.Id, room => room.Number),
                });
                Response.Redirect("Conferences");
                return;
            }
            throw new Exception("Введите название и дату проведения конференции");
        }

        [HttpGet]
        public IActionResult ConferenceUpdate(int conferenceId)
        {
            ViewBag.Conference = APIOrganizer.GetRequest<ConferenceViewModel>($"api/conference/getconference?conferenceId={conferenceId}");
            ViewBag.Rooms = APIOrganizer.GetRequest<List<RoomViewModel>>("api/conference/getroomlist");
            var Seminars = APIOrganizer.GetRequest<List<SeminarViewModel>>("api/seminar/getseminarlist");
            var conferenceSeminars = new List<SeminarViewModel>();
            foreach (var seminar in Seminars)
            {
                if (seminar.SeminarConferences.ContainsKey(conferenceId))
                {
                    conferenceSeminars.Add(seminar);
                }
            }
            ViewBag.ConferenceSeminars = conferenceSeminars; 
            return View();
        }

        [HttpPost]
        public void ConferenceUpdate(int conferenceId, string conferenceName, DateTime conferenceDateOf, List<int> roomsId)
        {
            if (!string.IsNullOrEmpty(conferenceName) && !string.IsNullOrEmpty(conferenceDateOf.ToString()) && roomsId != null)
            {
                var conference = APIOrganizer.GetRequest<ConferenceViewModel>($"api/conference/getconference?conferenceId={conferenceId}");
                if (conference == null)
                {
                    return;
                }
                List<RoomViewModel> rooms = new List<RoomViewModel>();
                foreach (var roomId in roomsId)
                {
                    rooms.Add(APIOrganizer.GetRequest<RoomViewModel>($"api/conference/getroom?roomId={roomId}"));
                }
                APIOrganizer.PostRequest("api/conference/createorupdateconference", new ConferenceBindingModel
                {
                    Id = conference.Id,
                    OrganizerId = (int)Program.Organizer.Id,
                    Name = conferenceName,
                    DateOf = conferenceDateOf,
                    ConferenceRooms = rooms.ToDictionary(room => room.Id, room => room.Number),
                });
                Response.Redirect("Conferences");
                return;
            }
            throw new Exception("Введите название и дату проведения конференции, выберите номер для конференции");
        }

        [HttpGet]
        public void ConferenceDelete(int conferenceId)
        {
            var conference = APIOrganizer.GetRequest<ConferenceViewModel>($"api/conference/getconference?conferenceId={conferenceId}");
            APIOrganizer.PostRequest("api/conference/deleteconference", conference);
            Response.Redirect("Conferences");
        }

        public IActionResult Seminars()
        {
            if (Program.Organizer == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIOrganizer.GetRequest<List<SeminarViewModel>>($"api/seminar/getorganizerseminars?organizerId={Program.Organizer.Id}"));
        }

        [HttpGet]
        public IActionResult SeminarCreate()
        {
            return View();
        }

        [HttpPost]
        public void SeminarCreate(string seminarName, string seminarSubjectArea)
        {
            if (!string.IsNullOrEmpty(seminarName) && !string.IsNullOrEmpty(seminarSubjectArea))
            {
                APIOrganizer.PostRequest("api/seminar/createorupdateseminar", new SeminarBindingModel
                {
                    OrganizerId = (int)Program.Organizer.Id,
                    Name = seminarName,
                    SeminarConferences = new Dictionary<int, string>(),
                    SubjectArea = seminarSubjectArea
                });
                Response.Redirect("Seminars");
                return;
            }
            throw new Exception("Введите название и предметную область семинара");
        }

        [HttpGet]
        public IActionResult SeminarUpdate(int seminarId)
        {
            ViewBag.Seminar = APIOrganizer.GetRequest<SeminarViewModel>($"api/seminar/getseminar?seminarId={seminarId}");
            return View();
        }

        [HttpPost]
        public void SeminarUpdate(int seminarId, string seminarName, string seminarSubjectArea)
        {
            if (!string.IsNullOrEmpty(seminarName) && !string.IsNullOrEmpty(seminarSubjectArea))
            {
                var seminar = APIOrganizer.GetRequest<SeminarViewModel>($"api/seminar/getseminar?seminarId={seminarId}");
                if (seminar == null)
                {
                    return;
                }
                APIOrganizer.PostRequest("api/seminar/createorupdateseminar", new SeminarBindingModel
                {
                    Id = seminar.Id,
                    OrganizerId = (int)Program.Organizer.Id,
                    Name = seminarName,
                    SubjectArea = seminarSubjectArea,
                    SeminarConferences = seminar.SeminarConferences
                });
                Response.Redirect("Seminars");
                return;
            }
            throw new Exception("Введите название и предметную область семинара");
        }

        [HttpGet]
        public void SeminarDelete(int seminarId)
        {
            var seminar = APIOrganizer.GetRequest<SeminarViewModel>($"api/seminar/getseminar?seminarId={seminarId}");
            APIOrganizer.PostRequest("api/seminar/deleteseminar", seminar);
            Response.Redirect("Seminars");
        }

        public IActionResult Participants()
        {
            if (Program.Organizer == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIOrganizer.GetRequest<List<ParticipantViewModel>>($"api/participant/getorganizerparticipants?organizerId={Program.Organizer.Id}"));
        }

        [HttpGet]
        public IActionResult ParticipantCreate()
        {
            ViewBag.Seminars = APIOrganizer.GetRequest<List<SeminarViewModel>>("api/seminar/getseminarlist");
            return View();
        }

        [HttpPost]
        public void ParticipantCreate(string participantFullName, string participantStatus, int seminarId)
        {
            if (!string.IsNullOrEmpty(participantFullName) && !string.IsNullOrEmpty(participantStatus) && seminarId != 0)
            {
                APIOrganizer.PostRequest("api/participant/createorupdateparticipant", new ParticipantBindingModel
                {
                    OrganizerId = (int)Program.Organizer.Id,
                    FullName = participantFullName,
                    Status = participantStatus,
                    SeminarId = seminarId
                });
                Response.Redirect("Participants");
                return;
            }
            throw new Exception("Введите ФИО и статус участника, выберите семинар");
        }

        [HttpGet]
        public IActionResult ParticipantUpdate(int participantId)
        {
            ViewBag.Seminars = APIOrganizer.GetRequest<List<SeminarViewModel>>("api/seminar/getseminarlist");
            ViewBag.Participant = APIOrganizer.GetRequest<ParticipantViewModel>($"api/participant/getparticipant?participantId={participantId}");
            return View();
        }

        [HttpPost]
        public void ParticipantUpdate(int participantId, string participantFullName, string participantStatus, int seminarId)
        {
            if (!string.IsNullOrEmpty(participantFullName) && !string.IsNullOrEmpty(participantStatus) && seminarId != 0)
            {
                var participant = APIOrganizer.GetRequest<ParticipantViewModel>($"api/participant/getparticipant?participantId={participantId}");
                if (participant == null)
                {
                    return;
                }
                APIOrganizer.PostRequest("api/participant/createorupdateparticipant", new ParticipantBindingModel
                {
                    Id = participant.Id,
                    OrganizerId = (int)Program.Organizer.Id,
                    FullName = participantFullName,
                    Status = participantStatus,
                    SeminarId = seminarId
                });
                Response.Redirect("Participants");
                return;
            }
            throw new Exception("Введите ФИО и статус участника, выберите семинар");
        }

        [HttpGet]
        public void ParticipantDelete(int participantId)
        {
            var participant = APIOrganizer.GetRequest<ParticipantViewModel>($"api/participant/getparticipant?participantId={participantId}");
            APIOrganizer.PostRequest("api/participant/deleteparticipant", participant);
            Response.Redirect("Participants");
        }

        [HttpGet]
        public IActionResult LinkSeminarConferences()
        {
            if (Program.Organizer == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Seminars = APIOrganizer.GetRequest<List<SeminarViewModel>>("api/seminar/getseminarlist");
            ViewBag.Conferences = APIOrganizer.GetRequest<List<ConferenceViewModel>>($"api/conference/getorganizerconferences?organizerId={Program.Organizer.Id}");
            return View();
        }

        [HttpPost]
        public void LinkSeminarConferences(int seminarId, List<int> conferencesId)
        {
            if (seminarId != 0 && conferencesId != null)
            {
                APIOrganizer.PostRequest("api/seminar/LinkSeminarToConferences", new LinkSeminarToConferencesBindingModel
                {
                    SeminarId = seminarId,
                    ConferenceId = conferencesId
                });
                Response.Redirect("Seminars");
                return;
            }
            throw new Exception("Выберите семинар и конференции");
        }
    }
}
