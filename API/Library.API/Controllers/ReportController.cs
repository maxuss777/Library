namespace Library.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Library.API.Business.Interfaces;

    [Authorize]
    public class ReportController : ApiController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [Route("report")]
        public HttpResponseMessage Get()
        {
            try
            {
                List<KeyValuePair<string, int>> report = _reportService.GetReport();

                return report.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, report)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Sorry, the report hasn't been created.");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }
    }
}