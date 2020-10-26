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
    public class URLController : Controller
    {
        //Get that will retrieve a list of URLs for a given user
        [HttpGet]
        public ActionResult<List<URL>> Get(string id)
        {
            //passed the user id in as a string so must parse to into before called to db for data retrieval
            int UserID = Int32.Parse(id);

            //Create the database context
            MyContext context = new MyContext();
            
            //creating the list of urls for a given user
            var urls = context.URL.Where(row => row.User.ID == UserID).ToList();
            
            //return the user info
            return urls;
    }
}
