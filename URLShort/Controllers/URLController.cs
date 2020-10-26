using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using URLShort.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace URLShort.Controllers
{
    public class URLController : Controller
    {
        //method that will be invoked after the user successfully logs in
        //the user's model will be passed as a parameter and a get will be employed to generate the information for a given user
        [HttpGet]
        public IActionResult Index(UserViewModel user)
        {
            //creating a list of urls that will be populated with the urls of a given user
            List<URLViewModel> urls = null;
            int key = user.ID;
            
            //getting the data by invoking the web service
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001");
                //HTTP GET
                var responseTask = client.GetAsync($"URL?id={key}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<URLViewModel>>();
                    readTask.Wait();

                    urls = readTask.Result;

                    //if user is valid, redirect
                    if (user != null)
                    {
                        URLViewModel testModel = new URLViewModel();
                        testModel.ID = user.ID;
                        return View(testModel);
                    }
                }
                else //web api sent error response 
                {
                    //log response status here..

                    URLViewModel userURL = new URLViewModel();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(new URLViewModel());
        }
        
        //post method that takes a long URL, shortens it and then adds it to the database
        [HttpPost]
        public IActionResult Index(string LongURL)
        {

            URLViewModel OriginalUrl = null;
            URLViewModel newURL = new URLViewModel();
            newURL.LongURL = LongURL;
            
            /*for the method of how to truncate the URLS, I thought about just taking off the slash that would
              come after the .com and replacing it with a GUID, but though that would be too long and considered an alternative 
              route of simply generating a random number of at maximum 6 digits to be displayed after
              the .com/net/etc*/
            int cutOff;
            string beforeGuid;
            if (LongURL.IndexOf(".com") != -1)
            {
                cutOff = LongURL.IndexOf(".com");
                beforeGuid = LongURL.Substring(0, cutOff + 4);
            }
            else if (LongURL.IndexOf(".gov") != -1)
            {
                cutOff = LongURL.IndexOf(".gov");
                beforeGuid = LongURL.Substring(0, cutOff + 4);
            }

            else if (LongURL.IndexOf(".net") != -1)
            {
                cutOff = LongURL.IndexOf(".net");
                beforeGuid = LongURL.Substring(0, cutOff + 4);
            }
            else if (LongURL.IndexOf(".co") != -1)
            {
                cutOff = LongURL.IndexOf(".co");
                beforeGuid = LongURL.Substring(0, cutOff + 3);
            }

            else
            {
                cutOff = LongURL.IndexOf(".us");
                beforeGuid = LongURL.Substring(0, cutOff + 3);
            }

            Random random = new Random();
            int randomEnd = random.Next(0, 100000);
            string finalURL = beforeGuid + "/" + randomEnd;
            newURL.ShortenedURL = finalURL;
            
            //after the url had been shortened, then had to use the web service to write the data to the db
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001");
                //HTTP Post
                var toSend = newURL;
                 var responseTask = client.PostAsync($"Shortener?longurl={newURL.LongURL}&shorturl={newURL.ShortenedURL}", null);
                 responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<URLViewModel>();
                    readTask.Wait();

                    OriginalUrl = readTask.Result;

                    //if user is valid, redirect
                    if (OriginalUrl != null)
                    {
                        ViewBag.ShortenedURL = OriginalUrl.ShortenedURL;
                        URLViewModel Temp = new URLViewModel();
                        Temp.ID = 1;

                        return View(Temp);
                    }
                }
                else //web api sent error response 
                {
                    //log response status here..

                    URLViewModel userURL = new URLViewModel();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(new URLViewModel());
        }


        //returns the view for a user on the page for converting the short back to original URL
        [HttpGet]
        public IActionResult Long(int ID)
        {
            URLViewModel shortenView = new URLViewModel();
            shortenView.ID = ID;

            return View(shortenView);
        }

        //Post method that uses the web service to locate the original url based off the shortened url input
        [HttpPost]
        public IActionResult Long(string ShortenedURL)
        {
            
            URLViewModel OriginalUrl = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001");
                //HTTP GET
                var responseTask = client.GetAsync($"Long?shortenedurl={ShortenedURL}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<URLViewModel>();
                    readTask.Wait();

                    OriginalUrl = readTask.Result;

                    //if url is valid
                    if (OriginalUrl != null)
                    {
                        ViewBag.Original = OriginalUrl.LongURL;
                        URLViewModel temp = new URLViewModel();

                        return View(temp);
                    }
                }
                else //web api sent error response 
                {
                    //log response status here..

                    URLViewModel userURL = new URLViewModel();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(new URLViewModel());
        }
    }
}
