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
                        record.Lunches.Add(new Tuple<string>(lunch.Value));
                        record.RoomNumber = model.Number;
                    }
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportConferencesViewModel> GetConferences(ReportBindingModel model)
        {
            var list = new List<ReportConferencesViewModel>();
            var conferences = _conferenceStorage.GetFilteredList(new ConferenceBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });

            var lunches = _lunchStorage.GetFullList();

            /*foreach (var conference in conferences)
            {
                foreach (var cs in conference.ConferenceSeminars)
                {
                    foreach (var lunch in lunches)
                    {
                        foreach (var ls in lunch.LunchSeminars)
                        {
                            if (ls.Key == cs.Key)
                            {
                                var seminar = _seminarStorage.GetElement(new SeminarBindingModel
                                {
                                    Id = ls.Key
                                });

                                list.Add(new ReportConferencesViewModel
                                {
                                    DateOf = conference.DateOf,
                                    Name = conference.Name,
                                    Seminar = seminar.Name,
                                    Lunch = lunch.Name
                                });
                            }
                        }
                    }
                }
            }*/
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

        public void SaveConferencesToPdf(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new OrganizerPdfInfo
            {
                FileName = model.FileName,
                Title = "Список семинаров и обедов по выбранным конференциям",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Conferences = GetConferences(model)
            });
        }
    }
}
