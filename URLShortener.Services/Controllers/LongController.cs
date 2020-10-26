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
    public class LongController : Controller
    {
        //method and controller that are specifically invoked for the retrieval of an original, long url from a shortened input
        [HttpGet]
        public ActionResult<URL> Get(string shortenedurl)
        {
            //Todo - Need to use dependency injection to create the context

            //Create the database context
            MyContext context = new MyContext();
            
            //returning the url that matches the shortenedURL
            var url = context.URL.FirstOrDefault(row => row.ShortenedURL == shortenedurl);
            
            return url;

        }
        
    }
}
