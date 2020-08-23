using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIproject.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;

namespace APIproject.Controllers
{
    public class HomeController : Controller
    {
        private const string V = "next_page_token";
        HttpClient httpClient;
        static string key = "AIzaSyAdjZUL9htcWqhQRaTazHBRHV11CYBokr4"; // Add API key here

        public ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
             return View();
          
        }

        /*   validation second page variables  */
        [HttpPost]
        public ActionResult GetAPIData(
            String location2, 
            String nature, 
            String city, 
            String outdoor)
        {

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", key);

            httpClient.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string URL = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=%27"+ location2 + "%27+point+of+interest&language=en&key="+ key;

            string Google_Path = URL;
            string parksdata = "";
            string nextpagetoken = "";
            Root parks = null; // Creating object Parks (From Model).  
                               //Dictionary<string, object> response_parse;
                               //Dictionary<string, object> parksdata;



            httpClient.BaseAddress = new Uri(Google_Path);
            List<Root> FinalResult = new List<Root>();
            // Root[] FinalResult = new Root[5];  // Try linked List

            try
            {

                Planvalidation Plan = new Planvalidation();
                Plan.location2 = location2;
                Plan.nature = nature;
                Plan.city = city;
                Plan.outdoor = outdoor;

                /*go to DB to write variables if those are correct ----must go to trending page*/

                if (!String.IsNullOrEmpty(Plan.location2))
                {

                    HttpResponseMessage response = httpClient.GetAsync(Google_Path).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        // Asynchronous for wait till get result, common for remote operation                    
                        parksdata = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    }

                    if (!parksdata.Equals(""))
                    {
                        // Deserialize parksdata string into the Parks Object. 
                        //parks = JsonConvert.DeserializeObject<Root>(parksdata);
                        //response_parse = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(parksdata));

                        while (true)
                        {
                            parksdata = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            parks = JsonConvert.DeserializeObject<Root>(parksdata);
                            FinalResult.Add(parks);

 
                            object has_next_page = parks.GetType().GetProperty("next_page_token");
                            if (!has_next_page.Equals("") || has_next_page is null) /// Check how to handle Null, should we use === while comparing string null (actual NULL)?? . 
                            {
                                Console.WriteLine("the value is:" + has_next_page);
                                break;
                            }
                            else
                            {
                                nextpagetoken = Convert.ToString(parks.next_page_token);
                                string URL_Copy = URL;
                                string URL_Final = URL_Copy + "&pagetoken=" + nextpagetoken;
                                response = httpClient.GetAsync(URL_Final).GetAwaiter().GetResult();

                            }
                        }

                    }
                    ViewBag.TopAttraction = "Top Attraction in " + Plan.location2;
                    return View(FinalResult);
                }
                else
                {
                    return View("Plan");
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;


        }



        private readonly ILogger<HomeController> _logger;
                public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Homepage()
        {
            return View();
        }

        public IActionResult Plan()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }

        // capture of signon variables: user & password 
        [HttpPost]
        public ActionResult LoginResults(String user, String password)
        {
            Userlogin user1 = new Userlogin();
            user1.user = user;
            user1.password = password;

            /*go to plan if user & pass are correct */


            if (!String.IsNullOrEmpty(user1.user) && !String.IsNullOrEmpty(user1.password))
            {
                return View("Plan");
            }
            else
            {
                return View("Index");
            }

        }
    }


}





