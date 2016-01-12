using System.Linq;
using System.Web.Mvc;
using Library.UI.Abstract;

namespace Library.UI.Controllers
{
    public class ReportController : Controller
    {
        private IReportServices _reportServices;

        public ReportController(IReportServices repoServ)
        {
            _reportServices = repoServ;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var report = _reportServices.GetReport(Request.Cookies["_auth"].Value);
                if (report == null || !report.Any())
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