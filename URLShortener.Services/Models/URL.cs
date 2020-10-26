using System;
namespace URLShortener.Services.Models
{
    public class URL
    {
        public int ID { get; set; }
        public string LongURL { get; set; }
        public string ShortenedURL { get; set; }
        public int AccessCount { get; set; }
        public DateTime LastAccess { get; set; }
        public User User { get; set; }
    }
}
