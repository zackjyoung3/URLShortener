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
    public class UserController : ControllerBase
    {
        /*
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        */
        [HttpGet]
        public ActionResult<User> Get(string userName, string passWord)
        {
            //Todo - Need to use dependency injection to create the context
            
            //Create the database context
            MyContext context = new MyContext();

            //Now get the user of the context
            //var user = context.Users.FirstOrDefault(row => row.ID == id);

            var user = context.Users.FirstOrDefault(row => row.UserName == userName && row.PassWord == passWord);

            //return the user info
            return user;
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
    }
}
