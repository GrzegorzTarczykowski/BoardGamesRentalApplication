namespace BoardGamesRentalApplication.Service
{
    public class EllipsisService : IEllipsisService
    {
        private string ellipsis = "...";

        public string EllipsisOf(string text, int maxCharsToTake)
        {
            if (text.Length <= maxCharsToTake)
                return text;
            return $"{text.Substring(0, maxCharsToTake - ellipsis.Length)}{ellipsis}";
        }
    }
}