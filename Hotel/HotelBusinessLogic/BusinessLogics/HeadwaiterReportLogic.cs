using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelContracts.BusinessLogicsContracts;
using HotelBusinessLogic.OfficePackage;
using HotelBusinessLogic.OfficePackage.HelperModels;

namespace HotelBusinessLogic.BusinessLogics
{
    public class HeadwaiterReportLogic : IHeadwaiterReportLogic
    {
        private readonly IConferenceStorage _conferenceStorage;
        private readonly ISeminarStorage _seminarStorage;
        private readonly ILunchStorage _lunchStorage;
        private readonly IRoomStorage _roomStorage;

        private readonly HeadwaiterAbstractSaveToWord _saveToWord;
        private readonly HeadwaiterAbstractSaveToExcel _saveToExcel;
        private readonly HeadwaiterAbstractSaveToPdf _saveToPdf;

        public HeadwaiterReportLogic(IConferenceStorage conferenceStorage, ISeminarStorage seminarStorage, ILunchStorage lunchStorage, IRoomStorage roomStorage,
            HeadwaiterAbstractSaveToWord saveToWord, HeadwaiterAbstractSaveToExcel saveToExcel, HeadwaiterAbstractSaveToPdf saveToPdf)
        {
            _conferenceStorage = conferenceStorage;
            _seminarStorage = seminarStorage;
            _lunchStorage = lunchStorage;
            _roomStorage = roomStorage;
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
        }

        // Получение списка семинаров по номерам
        public List<ReportRoomSeminarsViewModel> GetRoomSeminars(List<RoomViewModel> rooms)//
        {
            var list = new List<ReportRoomSeminarsViewModel>();

            foreach (var room in rooms)
            {
                var record = new ReportRoomSeminarsViewModel
                {
                    RoomNumber = room.Number,
                    LunchName = "",
                    Seminars = new List<Tuple<string>>()
                };
                foreach (var rl in room.RoomLunches)
                {
                    var model = _lunchStorage.GetElement(new LunchBindingModel { Id = rl.Key });
                    foreach (var seminar in model.LunchSeminars)
                    {
                        var sem = new Tuple<string>(seminar.Value);
                        if (!record.Seminars.Contains(sem))
                        {
                            record.Seminars.Add(sem);
                            record.LunchName = model.Name;
                        }
                    }
                }
                list.Add(record);
            }
            return list;
        }

        // Получение списка обедов с указанием семинара и номера за определенный период
        public List<ReportLunchesViewModel> GetLunches(ReportBindingModel model, int headwaiterId)//
        {
            var headwaitersRooms = _roomStorage.GetFilteredList(new RoomBindingModel
            {
                HeadwaiterId = headwaiterId
            });
            var list = new List<ReportLunchesViewModel>();
            var conferences = _conferenceStorage.GetFilteredList(new ConferenceBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            foreach (var conference in conferences)
            {
                foreach (var cr in conference.ConferenceRooms)
                {
                    var room = _roomStorage.GetElement(new RoomBindingModel
                    {
                        Id = cr.Key
                    });
                    var isH = false;
                    foreach (var hr in headwaitersRooms)
                    {
                        if (hr.Id == room.Id) isH = true;
                    }
                    if (!isH) { continue; }
                    foreach (var rl in room.RoomLunches)
                    {
                        var lunch = _lunchStorage.GetElement(new LunchBindingModel
                        {
                            Id = rl.Key
                        });
                        foreach (var ls in lunch.LunchSeminars)
                        {
                            var seminar = _seminarStorage.GetElement(new SeminarBindingModel
                            {
                                Id = ls.Key
                            });
                            var record = new ReportLunchesViewModel
                            {
                                Date = conference.DataOf,
                                Name = lunch.Name,
                                Dish = lunch.Dish,
                                Drink = lunch.Drink,
                                Seminar = seminar.Name,
                                Room = room.Number
                            };
                            list.Add(record);
                        }
                    }
                }
            }
            return list;
        }

        // Сохранение семинаров по номерам в файл-Word
        public void SaveRoomSeminarsToWord(ReportRoomBindingModel model)//
        {
            _saveToWord.CreateDoc(new HeadwaiterWordInfo
            {
                FileName = model.FileName,
                Title = "Список семинаров по выбранным номерам",
                RoomSeminars = GetRoomSeminars(model.Rooms)
            });
        }

        // Сохранение семинаров по номерам в файл-Excel
        public void SaveRoomSeminarsToExcel(ReportRoomBindingModel model)//
        {
            _saveToExcel.CreateReport(new HeadwaiterExcelInfo
            {
                FileName = model.FileName,
                Title = "Список семинаров по выбранным номерам",
                RoomSeminars = GetRoomSeminars(model.Rooms)
            });
        }

        // Сохранение  обедов с указанием семинара и номера заказов в файл-Pdf
        public void SaveLunchesToPdf(ReportBindingModel model, int headwaiterId)//
        {
            _saveToPdf.CreateDoc(new HeadwaiterPdfInfo
            {
                FileName = model.FileName,
                Title = "Список обедов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Lunches = GetLunches(model, headwaiterId)
            });
        }
    }
}
