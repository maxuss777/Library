using System;
using System.Configuration;

namespace Library.UI.Helpers
{
    public static class UrlResolver
    {
        public static String ApiDomain = ConfigurationManager.AppSettings["APIUrl"];

        public static String Categories_GetAll
        {
            get { return string.Format("{0}Categories", ApiDomain); }
        }

        public static String Books_Url
        {
            get { return string.Format("{0}Books", ApiDomain); }
        }
        public static String Categories_Url
        {
            get { return string.Format("{0}Categories", ApiDomain); }
        }

        public static String Books_By_Category_Name_Url(string categoryName)
        {
            return string.Format("{0}Books/{1}", ApiDomain, categoryName);
        }
        public static String Books_Id_Url(int bookId)
        {
            return string.Format("{0}Books/{1}", ApiDomain, bookId);
        }
    }
}