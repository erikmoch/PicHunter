using System.Net;
using System.Text;
using System.Text.RegularExpressions;

using PicHunter.ConsoleUI;

namespace PicHunter
{
    internal class Searcher
    {
        private readonly List<string> checkedIds;

        private readonly HttpClient httpClient;
        private readonly Random random;

        private readonly string path;

        private const string LIGHTSHOT_URL = "https://prnt.sc/";
        private const string REGEX_PATTERN = "<img.+?src=[\"'](.+?)[\"'].*?>";

        public Searcher(string userAgent, string pathToSave)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);

            random = new Random();
            checkedIds = new List<string>();

            path = pathToSave;
        }

        public async void Start()
        {
            while (true)
            {
                string id = GetRandomId();
                if (!checkedIds.Contains(id))
                {
                    checkedIds.Add(id);
                    string url = LIGHTSHOT_URL + id;
                    string imageUrl = GetImage(url).Result;

                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        DownloadImage(imageUrl, id);
                        await Task.Delay(1000);
                    }
                }
            }
        }

        private async Task<string> GetImage(string url)
        {
            string body = await httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();

            Match match = Regex.Match(body, REGEX_PATTERN, RegexOptions.IgnoreCase);
            if (match.Success)
                return match.Groups[1].Value;
            else
                return "";
        }

        public void DownloadImage(string imageUrl, string imageId)
        {
            using WebClient webClient = new WebClient();
            try
            {
                Logger.s($"Image found. ID: {imageId}. Downloading...");
                webClient.DownloadFile(new Uri(imageUrl), path + "\\" + imageId + ".png");
            }
            catch { }
        }

        private string GetRandomId(int length = 6)
        {
            const string chars = "1234567890abcdefghijklmnopqrstuvwxyz";
            StringBuilder randomString = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int num = random.Next(chars.Length);
                randomString.Append(chars[num]);
            }
            return randomString.ToString();
        }
    }
}