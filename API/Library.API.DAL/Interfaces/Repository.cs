namespace Library.API.DataAccess.Interfaces
{
    using System;
    using System.Configuration;

    public class Repository
    {
        protected readonly String ConnectionString = ConfigurationManager.ConnectionStrings["connectionStr"].ConnectionString;
    }
}