using System.Collections.Generic;
using Library.UI.Models;

namespace Library.UI.Abstract
{
    public interface IReportServices
    {
        IEnumerable<KeyValuePair<string, int>> GetReport(string ticket);
    }
}
