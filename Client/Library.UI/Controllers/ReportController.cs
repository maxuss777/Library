using Library.UI.Interfaces;

namespace Library.UI.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                List<KeyValuePair<string, int>> report = _reportService.GetReport();

                if (report.Count == 0)
                {
                    TempData["fail_message"] = "Nothing to show, the report is empty!";

                    return RedirectToAction("GetAll", "Books");
                }

                return PartialView("ReportView", report);
            }
            catch
            {
                TempData["fail_message"] = "Sorry, there is some problem with report generating!";

                return RedirectToAction("GetAll", "Books");
            }
        }
    }
}