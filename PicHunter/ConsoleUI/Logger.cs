using PicHunter.ConsoleUI.Enums;

namespace PicHunter.ConsoleUI
{
    internal class Logger
    {
        private const string LOG_FORMAT = "[{0}] ({1}) => {2}";

        private static void Log(string message, LogTypes logType)
        {
            Console.ForegroundColor = (ConsoleColor)logType;
            Console.WriteLine(string.Format(LOG_FORMAT, DateTime.Now, logType.ToString(), message));
            Console.ResetColor();
        }

        public static void i(string message) => Log(message, LogTypes.Info);
        public static void w(string message) => Log(message, LogTypes.Warning);
        public static void e(string message) => Log(message, LogTypes.Error);
        public static void s(string message) => Log(message, LogTypes.Success);
        public static void n() => Console.WriteLine();
    }
}