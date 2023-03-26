using System.Globalization;
using System.Resources;

namespace Services.Helper.Extensions
{
    public static class ExceptionResourceManagerExtension
    {
        private static CultureInfo exceptionCulture = new CultureInfo("vi-VN");
        private static string resourceFile = "Services.Helper.Resources.ExceptionMessages";

        public static CultureInfo ExceptionCultureInfo
        {
            get { return exceptionCulture; }
            set { exceptionCulture = value; }
        }

        public static ResourceManager GetResourceManager()
        {
            Thread.CurrentThread.CurrentUICulture = exceptionCulture;
            var resourceManager = new ResourceManager(resourceFile, System.Reflection.Assembly.GetExecutingAssembly());
            return resourceManager;
        }

        public static string GetString(string key, string replace = "")
        {
            var str = GetResourceManager().GetString(key);
            if (string.IsNullOrEmpty(str))
            {
                return replace;
            }
            return str;
        }
    }
}