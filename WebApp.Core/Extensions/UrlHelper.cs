namespace WebApp.Core.Extensions
{
    public static class UrlHelper
    {
        public static string CreateUrl(string text)
        {
            text = text.Replace("ı", "i");
            text = text.Replace("İ", "i");
            text = text.Replace("ğ", "g");
            text = text.Replace("Ğ", "G");
            text = text.Replace("ç", "c");
            text = text.Replace("Ç", "C");
            text = text.Replace("ş", "s");
            text = text.Replace("Ş", "S");
            text = text.Replace("ö", "o");
            text = text.Replace("Ö", "O");
            text = text.Replace("ü", "u");
            text = text.Replace("Ü", "U");
            text = text.Replace("I", "i");
            text = text.Replace(" ", "-");
            text = text.Replace("[", "-");
            text = text.Replace("]", "-");
            text = text.Replace("(", "-");
            text = text.Replace(")", "-");
            text = text.Replace(":", "-");
            text = text.Replace(";", "-");
            text = text.Replace(".", "-");
            text = text.Replace(",", "-");
            text = text.Replace("~", "-");
            text = text.Replace("!", "");
            text = text.Replace("\\", "");
            text = text.Replace("/", "");
            text = text.Replace("&", "-");
            text = text.Replace("+", "-");
            text = text.Replace("'", "");
            text = text.Replace("--", "-");
            text = text.Trim();
            text = text.ToLower();
            
            return text;
        }
    }
}