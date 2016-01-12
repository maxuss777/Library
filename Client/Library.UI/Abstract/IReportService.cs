namespace Library.UI.Abstract
{
    using System.Collections.Generic;

    public interface IReportService
    {
        List<KeyValuePair<string, int>> GetReport(string ticket);
    }
}