using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using URLShortener.Services.Models;

namespace URLShortener.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<User> Get(int id)
        {
            //Todo - Need to use dependency injection to create the context

            int n = id;

            //Create the database context
            MyContext context = new MyContext();

            //Now get the user of the context
            var user = context.Users.FirstOrDefault(row => row.ID == id);

            //return the user info
            return user;
        }
        /*
        //Todo - Need to add /// comments
        [HttpGet]
        public ActionResult<User> Get(int id)
        {
            //Todo - Need to use dependency injection to create the context

            int n = id;

            //Create the database context
            URLShortenerContext context = new MyContext();

            //Now get the user of the context
            var user = context.Users.FirstOrDefault(row => row.Id == id);

            //return the user info
            return user;
        }
        */
        /*
        [HttpGet]
        public List<StringBuilder> Get()
        {
            // Gets and prints all books in database
            using (var context = new MyContext())
            {
                List<StringBuilder> myUrls = new List<StringBuilder>();

                var urls = context.URL
                  .Include(p => p.User);
                foreach (var url in urls)
                {
                    var data = new StringBuilder();
                    data.AppendLine($"Previous Long URL : {url.LongURL}");
                    data.AppendLine($"Shortened URL: {url.ShortenedURL}");
                    data.AppendLine($"Access Count: {url.AccessCount}");
                    data.AppendLine($"Last Access: {url.LastAccess}");
                    data.AppendLine($"User First Name {url.User.FirstName}");
                    data.AppendLine($"User First Name {url.User.LastName}");
                    myUrls.Add(data);
                }

                return myUrls;
            }
        */
    }
}

