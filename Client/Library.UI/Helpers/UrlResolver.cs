namespace Library.UI.Helpers
{
    using System;
    using System.Configuration;

    public static class UrlResolver
    {
        private static readonly String ApiDomain = ConfigurationManager.AppSettings["APIUrl"];

        public static String GetApiRegistrationUrl
        {
            get { return string.Format("{0}api/Registration", ApiDomain); }
        }

        public static String GetApiLoginUrl
        {
            get { return string.Format("{0}api/Login", ApiDomain); }
        }

        public static String GetApiReportUrl
        {
            get { return string.Format("{0}/Report", ApiDomain); }
        }

        public static String GetApiBooksUrl
        {
            get { return string.Format("{0}api/Books", ApiDomain); }
        }

        public static String GetApiBooksByCategoryNameUrl(string categoryName)
        {
            return string.Format("{0}api/Books/{1}", ApiDomain, categoryName);
        }

        public static String GetApiBooksByIdUrl(int bookId)
        {
            return string.Format("{0}api/Books/{1}", ApiDomain, bookId);
        }

        public static String GetApiCategoriesUrl
        {
            get { return string.Format("{0}api/Categories", ApiDomain); }
        }

        public static String GetCategoriesByIdUrl(int categoryId)
        {
            return string.Format("{0}api/Categories/{1}", ApiDomain, categoryId);
        }

        public static String GetCategoriesByNameUrl(string categoryName)
        {
            return string.Format("{0}api/Categories/{1}", ApiDomain, categoryName);
        }

        public static String GetCategoriesAddBookUrl(int categoryId, int bookId)
        {
            return string.Format("{0}api/Categories/{1}/addbook/{2}", ApiDomain, categoryId, bookId);
        }

        public static String GetCategoriesRemoveBookUrl(int categoryId, int bookId)
        {
            return string.Format("{0}api/Categories/{1}/removebook/{2}", ApiDomain, categoryId, bookId);
        }

    }
}