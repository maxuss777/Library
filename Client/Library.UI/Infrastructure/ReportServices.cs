using System.Collections.Generic;
using System.Linq;
using Library.UI.Abstract;
using Library.UI.Helpers;
using Library.UI.Models;
using System.Net;
using System;

namespace Library.UI.Infrastructure
{
    public class ReportServices : Services, IReportServices
    {
        public IEnumerable<KeyValuePair<string,int>> GetReport(string ticket)
        {
            try
            {
                var response = GetObjectsAsDictionary("GET", UrlResolver.Api_Report, ticket: ticket);
                if (!response.Any())
                {
                    return null;
                }
                return response;
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }
            return null;
        }
    }
}