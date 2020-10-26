using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Services.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace URLShortener.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class URLController : Controller
    {
        [HttpGet]
        public ActionResult<List<URL>> Get(string id)
        {
            //Todo - Need to use dependency injection to create the context
            int UserID = Int32.Parse(id);

            //Create the database context
            MyContext context = new MyContext();

            //Now get the user of the context
            //var user = context.Users.FirstOrDefault(row => row.ID == id);

                //var user = context.Users.FirstOrDefault(row => row.UserName == userName && row.PassWord == passWord);
                var urls = context.URL.Where(row => row.User.ID == UserID).ToList();
                //URL boss = new URL();
                //return the user info
                return urls;

        }
        /*
        [HttpGet]
        public ActionResult<URL> Get(string ID, string shortened)
        {
                //Create the database context
                MyContext context = new MyContext();

                //var user = context.Users.FirstOrDefault(row => row.UserName == userName && row.PassWord == passWord);
                var url = context.URL.FirstOrDefault(row => row.User.ID.ToString() == ID && row.ShortenedURL == shortened);
                //URL boss = new URL();
                //return the user info
                return url;
        }
        */
        /*
        [HttpGet]
        public ActionResult<URL> Get(string shortened)
        {
            //Todo - Need to use dependency injection to create the context
            int UserID = Int32.Parse(ID);
            //Create the database context
            MyContext context = new MyContext();

            //Now get the user of the context
            //var user = context.Users.FirstOrDefault(row => row.ID == id);

            //var user = context.Users.FirstOrDefault(row => row.UserName == userName && row.PassWord == passWord);
            var urls = context.URL.Where(row => row.User.ID == UserID).ToList();
            //URL boss = new URL();
            //return the user info
            return urls;
        }
        
        [HttpGet]
        [Route("User/Login")]
        public ActionResult<User> Login(string userName, string passWord)
        {
            //Todo - Need to use dependency injection to create the context

            //Create the database context
            MyContext context = new MyContext();

            //Now get the user of the context
            var user = context.Users.FirstOrDefault(row => row.UserName == userName && row.PassWord == passWord);

            //return the user info
            return user;
        }
        */
    }
}
