﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Net.Http;
using URLShort.Models;
using System.Net.Http.Formatting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace URLShort.Controllers
{
    public class UserController : Controller
    {
        
        [HttpGet]
        public IActionResult Index(UserViewModel user)
        {
            UserViewModel loginView = new UserViewModel();

            return View(loginView);
        }

        //Post method that uses the web service to retrieve a given user based off of their username and password
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            UserViewModel user = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001");
                //HTTP GET
                var responseTask = client.GetAsync($"User?username={username}&password={password}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UserViewModel>();
                    readTask.Wait();

                    user = readTask.Result;

                    //if user is valid, redirect
                    if (user != null)
                    {
                        return RedirectToAction("Index", "Url", user);
                    }
                }
                else //web api sent error response 
                {
                    //log response status here..

                    user = new UserViewModel();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(new UserViewModel());
        }
    }
}
