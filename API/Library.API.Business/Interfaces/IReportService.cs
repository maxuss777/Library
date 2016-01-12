namespace Library.API.Business.Interfaces
{
    using System.Collections.Generic;

    public interface IReportService
    {
        List<KeyValuePair<string, int>> GetReport();
    }
}