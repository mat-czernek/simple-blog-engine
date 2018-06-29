

using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MyBlog.Infrastructure
{
    /// <summary>
    /// Custom extensions class
    /// </summary>
    public static class CustomExtensions
    {
        /// <summary>
        /// Extend string type by abilty of post slug generation
        /// </summary>
        /// <param name="content">Post content</param>
        /// <returns></returns>
        public static string GenerateSlug(this string content)
        {
            string str = content.RemoveDiacritics().ToLower();
                      
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Replace("-", string.Empty);
            str = string.Join(" ", str.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries));
        
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }

        public static string RemoveDiacritics(this string text)
        {
            var s = new string(text.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());
    
            return s.Normalize(NormalizationForm.FormC);
        }
    }
}