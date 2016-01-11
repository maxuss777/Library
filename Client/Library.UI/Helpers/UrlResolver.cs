using System;
using System.Configuration;

namespace Library.UI.Helpers
{
    public static class UrlResolver
    {
        public static String ApiDomain = ConfigurationManager.AppSettings["APIUrl"];

        public static String Api_Registration
        {
            get { return string.Format("{0}Registration", ApiDomain); }
        }
        public static String Api_Login
        {
            get { return string.Format("{0}Login", ApiDomain); }
        }

        #region Book
        public static String Books_Url
        {
            get { return string.Format("{0}Books", ApiDomain); }
        }
        public static String Books_By_Category_Name_Url(string categoryName)
        {
            return string.Format("{0}Books/{1}", ApiDomain, categoryName);
        }
        public static String Books_Id_Url(int bookId)
        {
            return string.Format("{0}Books/{1}", ApiDomain, bookId);
        }
        #endregion

        #region Category
        public static String Categories_GetAll
        {
            get { return string.Format("{0}Categories", ApiDomain); }
        }
        public static String Categories_Url
        {
            get { return string.Format("{0}Categories", ApiDomain); }
        }
        public static String Categories_Id_Url(int categoryId)
        {
            return string.Format("{0}Categories/{1}", ApiDomain, categoryId);
        }
        public static String Categories_Name_Url(string categoryName)
        {
            return string.Format("{0}Categories/{1}", ApiDomain, categoryName);
        }
        public static String Categories_AddBook(int categoryId, int bookId)
        {
            return string.Format("{0}Categories/{1}/addbook/{2}", ApiDomain, categoryId, bookId);
        }
        public static String Categories_RemoveBook(int categoryId, int bookId)
        {
            return string.Format("{0}Categories/{1}/removebook/{2}", ApiDomain, categoryId, bookId);
        }
        #endregion
        
    }
}