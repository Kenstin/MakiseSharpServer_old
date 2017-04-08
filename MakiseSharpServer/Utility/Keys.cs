using System.Text.RegularExpressions;

namespace MakiseSharpServer.Utility
{
    public class Keys
    {
        public static string Dearmor(string key)
        {
            const string pattern = @"-----BEGIN [^-]+-----([A-Za-z0-9+\/=\s]+)-----END [^-]+-----";
            var match = Regex.Match(key, pattern);

            return match.Groups[1].ToString().Trim();
        }
    }
}
