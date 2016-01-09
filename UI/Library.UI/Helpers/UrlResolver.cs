using System;
using System.Configuration;

namespace Library.UI.Helpers
{
    public static class UrlResolver
    {
        private static String ApiDomain = ConfigurationManager.AppSettings["APIUrl"];

        public static String Categories_GetAll
        {
            get { return string.Format("{0}Categories", ApiDomain); }
        }

        public static String Books_GetAll
        {
            get { return string.Format("{0}Books", ApiDomain); }
        }

        public static String Books_GetAll_By_Category_Name(string categoryName)
        {
            return string.Format("{0}Books/{1}", ApiDomain, categoryName);
        }
    }
}