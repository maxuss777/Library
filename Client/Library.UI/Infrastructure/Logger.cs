namespace Library.UI.Infrastructure
{
    using System.IO;
    using System.Web;

    public static class Logger
    {
        public static void Write(string content)
        {
            string url = HttpContext.Current.Server.MapPath("~/App_Data/log.txt");

            File.AppendAllText(url, content);
        }
    }
}