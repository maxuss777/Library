using System.Collections.Generic;

namespace Library.API.Business.Abstract
{
    public interface IReportServices
    {
        Dictionary<string,int> GetReport();
    }
}
