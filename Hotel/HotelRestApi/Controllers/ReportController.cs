using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HotelRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController
    {
        private readonly IOrganizerReportLogic _reportLogic;
        private readonly IConferenceLogic _conferenceLogic;
        public ReportController(IOrganizerReportLogic reportLogic, IConferenceLogic conferenceLogic)
        {
            _reportLogic = reportLogic;
            _conferenceLogic = conferenceLogic;
        }

        [HttpPost]
        public void CreateReportToWord(ReportConferenceBindingModel model) => _reportLogic.SaveConferenceLunchesToWord(model);

        [HttpPost]
        public void CreateReportToExcel(ReportConferenceBindingModel model) => _reportLogic.SaveConferenceLunchesToExcel(model);

        [HttpPost]
        public void CreateReportToPdf(ReportConferenceBindingModel model) => _reportLogic.SaveConferencesToPdf(model);

        [HttpGet]
        public List<ReportConferencesViewModel> GetConferencesReport(string dateFrom, string dateTo) => _reportLogic.GetConferences(new ReportConferenceBindingModel { DateFrom = Convert.ToDateTime(dateFrom), DateTo = Convert.ToDateTime(dateTo) });
    }
}
