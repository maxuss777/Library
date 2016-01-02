using System;
using System.Configuration;

namespace Library.API.DAL
{
    public class Repository
    {
        protected readonly String ConnectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
    }
}