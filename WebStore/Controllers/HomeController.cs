﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View("Sec");

        public IActionResult Second()
        {
            return Content("Sec Controller Aaction");
        }
    }
}
