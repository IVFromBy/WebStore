using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index() => View();

        public IActionResult Login() => View("Login");

        public IActionResult ContactUs() => View("ContactUs");

        public IActionResult Cart() => View("Cart");

        public IActionResult Checkout() => View("Checkout");

        public IActionResult Blog() => View("Blog");

        public IActionResult BlogSingle() => View("BlogSingle");

        public IActionResult NotFound() => View("NotFound");

        public IActionResult ProductDetails() => View("ProductDetails");

        public IActionResult Product() => View("Shop");


    }
}
