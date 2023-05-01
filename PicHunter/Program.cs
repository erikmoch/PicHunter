using PicHunter.ConsoleUI;

namespace PicHunter
{
    public class Program
    {
        private static Searcher? searcher;

        public static void Main()
        {
            Console.Title = "PicHunter - Lightshot Scrapper";

            string[] logo = new string[]
            {
            @"  _____ _      _    _             _            ",
            @" |  __ (_)    | |  | |           | |           ",
            @" | |__) |  ___| |__| |_   _ _ __ | |_ ___ _ __ ",
            @" |  ___/ |/ __|  __  | | | | '_ \| __/ _ \ '__|",
            @" | |   | | (__| |  | | |_| | | | | ||  __/ |   ",
            @" |_|   |_|\___|_|  |_|\__,_|_| |_|\__\___|_|   ",
            @""
            };
            foreach (string line in logo)
                Console.WriteLine(line);

            Logger.i("Welcome to PicHunter!");
            Logger.w("Enter path to save images: ");

            string? path = Console.ReadLine();

            if (Directory.Exists(path))
            {
                Logger.i("Starting searcher.");

                searcher = new Searcher("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36", path);
                searcher.Start();
            }
            else
            {
                Logger.e("Invalid path.");
                Logger.e("Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}