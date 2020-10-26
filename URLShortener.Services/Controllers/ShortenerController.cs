using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Services.Models;


namespace URLShortener.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortenerController : Controller
    {
        //method that is specifically invoked when a user inputs a new URL to be shortened that results in a call to this web service
        //with the shortened and original urls
        [HttpPost]
        public ActionResult<URL> Set(string longurl, string shorturl)
        {
            //Todo - Need to use dependency injection to create the context

            //Create the database context
            MyContext context = new MyContext();
            
            //creating an instance of a URL and then populating it with the longurl and shortened url input
            //then writing it to the db
            URL anotherURL = new URL();
            anotherURL.LongURL = longurl;
            anotherURL.ShortenedURL = shorturl;
            context.URL.Add(anotherURL);

            //saving the changes
            context.SaveChanges();

            return anotherURL;
        }
       
    }
}

