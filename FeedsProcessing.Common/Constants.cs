namespace FeedsProcessing.Common
{

    //TODO : Consider to move the constants to appsettings.config file
    public static class Constants
    {
        public static string BasePath = "D:\\ntf";
        public static string PathFormat = BasePath + "\\{0}\\{1}";
        public static string StateFilePath = BasePath + "\\state.json";
        public static string NotificationFileNameFormat = "{0}.notification.json";
        public static string SummaryFileNameFormat = "{0}.summary.json";
    }
}
