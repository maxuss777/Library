using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library.API.Business.Abstract;

namespace Library.API.Controllers
{
    public class ReportController : ApiController
    {
        private IReportServices _reportServices;

        public ReportController(IReportServices reportSer)
        {
            _reportServices = reportSer;
        }

        [HttpGet]
        [Route("report")]
        public HttpResponseMessage Get()
        {
            try
            {
                var report =_reportServices.GetReport();
                return report != null || !report.Equals(default(Dictionary<string,int>))
                    ? Request.CreateResponse(HttpStatusCode.OK, report)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Sorry, the report hasn't been created.");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }
    }
}