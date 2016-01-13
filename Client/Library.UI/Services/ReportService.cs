using Library.UI.Interfaces;

namespace Library.UI.Services
{
    using System;
    using System.Collections.Generic;
    using Library.UI.Helpers;
    using Library.UI.Infrastructure;

    public class ReportService : Service, IReportService
    {
        public List<KeyValuePair<string, int>> GetReport()
        {
            try
            {
                List<KeyValuePair<string, int>> response = GetObjectsAsListKeyValuePairs("GET", UrlResolver.GetApiReportUrl);

                return response.Count == 0 ? null : response;
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return new List<KeyValuePair<string, int>>();
        }
    }
}