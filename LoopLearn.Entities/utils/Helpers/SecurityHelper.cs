using Ganss.Xss;

namespace LoopLearn.Entities.utils.Helpers
{
    public static class SecurityHelper
    {
        private static readonly HtmlSanitizer _sanitizer;

        static SecurityHelper()
        {
            _sanitizer = new HtmlSanitizer();
            _sanitizer.AllowedTags.Add("details");
            _sanitizer.AllowedTags.Add("summary");
        }

        public static string SanitizeHtml(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;

            return _sanitizer.Sanitize(input);
        }
    }
}
