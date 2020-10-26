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
        /*
        [HttpGet]
        public IActionResult Index(Guid test)
        {
            URLViewModel shortenView = new URLViewModel();

            return View(shortenView);
        }*/

        [HttpGet]
        public IActionResult Index(UserViewModel user)
        {
            List<URLViewModel> urls = null;
            int key = user.ID;

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

        [HttpPost]
        public IActionResult Index(string LongURL)
        {

            URLViewModel OriginalUrl = null;
            URLViewModel newURL = new URLViewModel();
            newURL.LongURL = LongURL;

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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001");
                //HTTP Post
                var toSend = newURL;
                //var responseTask = client.PostAsync($"shortener", new { longurl = newURL.LongURL, shorturl = newURL.ShortenedURL });
                 var responseTask = client.PostAsync($"Shortener?longurl={newURL.LongURL}&shorturl={newURL.ShortenedURL}", null);
                //HttpResponseMessage response = client.PostAsync(String.Format("api/OrganizationGroup/UpdateFailureTypeCO?failureTypeId={0}&checkedList={1}", failureTypeId, cs)).Result;
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


        [HttpGet]
        public IActionResult Long(int ID)
        {
            URLViewModel shortenView = new URLViewModel();
            shortenView.ID = ID;

            return View(shortenView);
        }

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

                    //if user is valid, redirect
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
        /*
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            URLViewModel url = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001");
                //HTTP GET
                var responseTask = client.GetAsync($"URL?username={username}");
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
                        return RedirectToAction("Index", "Url");
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
        */
