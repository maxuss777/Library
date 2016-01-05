using System;
using System.Configuration;

namespace Library.API.DAL.Abstract
{
    public class Repository
    {
        protected readonly String ConnectionString = ConfigurationManager.ConnectionStrings["connectionStr"].ConnectionString;
    }
}