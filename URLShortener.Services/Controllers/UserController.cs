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
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<User> Get(string userName, string passWord)
        {
            //Todo - Need to use dependency injection to create the context
            
            //Create the database context
            MyContext context = new MyContext();
            
            //retrieving the user from the database whose name matches the given username and password
            var user = context.Users.FirstOrDefault(row => row.UserName == userName && row.PassWord == passWord);

            //return the user 
            return user;
        }
    }
}
