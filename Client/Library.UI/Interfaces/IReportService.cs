using System.Collections.Generic;

namespace Library.UI.Interfaces
{
    public interface IReportService
    {
        List<KeyValuePair<string, int>> GetReport();
    }
}