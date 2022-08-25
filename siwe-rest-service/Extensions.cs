using System.IO;

using Nethereum.Siwe.Core;

using siwe.Messages;

using siwe_rest_service.Models;

namespace siwe_rest_service
{
    public static class Extensions
    {
        public static DateTime ConvertToDateTime(string providedDateTime)
        {
            DateTime targetDateTime = DateTime.MinValue;

            if (!String.IsNullOrEmpty(providedDateTime))
            {
                if (!DateTime.TryParse(providedDateTime, out targetDateTime))
                    targetDateTime = DateTime.Now.AddMinutes(20);
            }
            else
            {
                targetDateTime = DateTime.Now.AddMinutes(20);
            }

            return targetDateTime;
        }

        public static string GetNotepadText(this MySiweMessage message)
        {
            string siweText = String.Empty;

            DirectoryInfo dbDir = new DirectoryInfo("../db");
            if (dbDir.Exists && !String.IsNullOrEmpty(message?.Address))
            {
                FileInfo textFile = new FileInfo("../db/" + message.Address + ".txt");
                if (textFile.Exists)
                {
                    siweText = File.ReadAllText(textFile.FullName);
                }
            }

            return siweText;
        }

        public static async Task<string> GetNotepadTextAsync(this MySiweMessage message)
        {
            string siweText = String.Empty;

            DirectoryInfo dbDir = new DirectoryInfo("../db");
            if (dbDir.Exists && !String.IsNullOrEmpty(message?.Address))
            {
                FileInfo textFile = new FileInfo("../db/" + message.Address + ".txt");
                if (textFile.Exists)
                {
                    siweText = await File.ReadAllTextAsync(textFile.FullName);
                }
            }

            return siweText;
        }

        public static void SaveNotepadText(this MySiweMessageAndText message)
        {
            DirectoryInfo dbDir = new DirectoryInfo("../db");
            if (!dbDir.Exists)
            {
                dbDir.Create();
            }

            if (!String.IsNullOrEmpty(message?.Address) && !String.IsNullOrEmpty(message?.Text))
            {
                File.WriteAllText("../db/" + message.Address + ".txt", message.Text);
            }
        }

        public static async void SaveNotepadTextAsync(this MySiweMessageAndText message)
        {
            DirectoryInfo dbDir = new DirectoryInfo("../db");
            if (!dbDir.Exists)
            {
                dbDir.Create();
            }

            if (!String.IsNullOrEmpty(message?.Address) && !String.IsNullOrEmpty(message?.Text))
            {
                await File.WriteAllTextAsync("../db/" + message.Address + ".txt", message.Text);
            }
        }
    }
}
