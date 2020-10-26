using System;
namespace URLShort.Models
{
    public class URLViewModel
    {
        public string LongURL { get; set; }
        public string ShortenedURL { get; set; }
        public int AccessCount { get; set; }
        public DateTime LastAccess { get; set; }
        public int ID { get; set; }

    }
}
