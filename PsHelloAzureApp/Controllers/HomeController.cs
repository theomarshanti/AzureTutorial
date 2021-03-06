﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PsHelloAzureApp.Models;
using Microsoft.Extensions.Configuration;

namespace PsHelloAzureApp.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            var model = this.configuration["Greeting"];
            return View("Index", model);
        }

        public IActionResult Test()
        {
            throw new InvalidOperationException("This is a failure");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
