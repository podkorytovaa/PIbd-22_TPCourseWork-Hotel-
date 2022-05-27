using System;
using System.Collections.Generic;
using System.Linq;
using HotelContracts.BindingModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelContracts.BusinessLogicsContracts;
using HotelBusinessLogic.OfficePackage;
using HotelBusinessLogic.OfficePackage.HelperModels;

namespace HotelBusinessLogic.BusinessLogics
{
    public class OrganizerReportLogic : IOrganizerReportLogic
    {
        private readonly IConferenceStorage _conferenceStorage;
        private readonly ISeminarStorage _seminarStorage;
        private readonly ILunchStorage _lunchStorage;
        private readonly IRoomStorage _roomStorage;
        private readonly OrganizerAbstractSaveToWord _saveToWord;
        private readonly OrganizerAbstractSaveToExcel _saveToExcel;
        private readonly OrganizerAbstractSaveToPdf _saveToPdf;

        public OrganizerReportLogic(IConferenceStorage conferenceStorage, ISeminarStorage seminarStorage, ILunchStorage lunchStorage, IRoomStorage roomStorage,
            OrganizerAbstractSaveToWord saveToWord, OrganizerAbstractSaveToExcel saveToExcel, OrganizerAbstractSaveToPdf saveToPdf)
        {
            _conferenceStorage = conferenceStorage;
            _seminarStorage = seminarStorage;
            _lunchStorage = lunchStorage;
            _roomStorage = roomStorage;
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
        }

        // список обедов по конференциям
        public List<ReportConferenceLunchesViewModel> GetConferenceLunches(List<ConferenceViewModel> conferences)
        {
            var list = new List<ReportConferenceLunchesViewModel>();
            foreach (var conference in conferences)
            {
                var record = new ReportConferenceLunchesViewModel
                {
                    ConferenceName = conference.Name,
                    RoomNumber = string.Empty,
                    Lunches = new List<Tuple<string>>()
                };
                foreach (var room in conference.ConferenceRooms)
                {
                    var model = _roomStorage.GetElement(new RoomBindingModel { Id = room.Key });
                    foreach (var lunch in model.RoomLunches)
                    {
                        var dinner = new Tuple<string>(lunch.Value);
                        if (!record.Lunches.Contains(dinner))
                        {
                            record.Lunches.Add(dinner);
                            record.RoomNumber = model.Number;
                        }
                    }
                }
                list.Add(record);
            }
            return list;
        }

        // отчет по конференциям за выбранный период
        public List<ReportConferencesViewModel> GetConferences(ReportConferenceBindingModel model)
        {
            var list = new List<ReportConferencesViewModel>();
            var conferences = _conferenceStorage.GetFilteredList(new ConferenceBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                OrganizerId = model.OrganizerId
            });

            foreach (var conference in conferences)
            {
                var record = new ReportConferencesViewModel
                {
                    ConferenceName = conference.Name,
                    DateOf = conference.DateOf,
                    SeminarLunches = new List<(SeminarViewModel, List<LunchViewModel>)>()
                };
                var seminars = _seminarStorage.GetFullList().Where(rec => rec.SeminarConferences.Keys.ToList().Contains(conference.Id)).ToList();
                foreach (var seminar in seminars)
                {
                    var lunches = _lunchStorage.GetFullList().Where(rec => rec.LunchSeminars.Keys.ToList().Contains(seminar.Id)).ToList();
                    record.SeminarLunches.Add((seminar, lunches));
                }
                list.Add(record);
            }
            return list;
        }

        public void SaveConferenceLunchesToWord(ReportConferenceBindingModel model)
        {
            _saveToWord.CreateDoc(new OrganizerWordInfo
            {
                FileName = model.FileName,
                Title = "Список обедов по выбранным конференциям",
                ConferenceLunches = GetConferenceLunches(model.Conferences)
            });
        }

        public void SaveConferenceLunchesToExcel(ReportConferenceBindingModel model)
        {
            _saveToExcel.CreateReport(new OrganizerExcelInfo
            {
                FileName = model.FileName,
                Title = "Список обедов по выбранным конференциям",
                ConferenceLunches = GetConferenceLunches(model.Conferences)
            });
        }

        public void SaveConferencesToPdf(ReportConferenceBindingModel model)
        {
            _saveToPdf.CreateDoc(new OrganizerPdfInfo
            {
                FileName = model.FileName,
                Title = "Сведения о семинарах и обедах по конференциям",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Conferences = GetConferences(model)
            });
        }
    }
}
