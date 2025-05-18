using System.Text.RegularExpressions;

namespace PkmWebServer.Utils;
public static class NameCleaner
{
    public static string CleanNameKey(string pNameKey)
    {
        string name = pNameKey.ToUpper();
        name = Regex.Replace(name, @"\W", "");
        return name;
    }
}
