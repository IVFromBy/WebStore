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

        public IActionResult Error404() => View("NotFound");

        public IActionResult ProductDetails() => View("ProductDetails");

        public IActionResult Product() => View("Shop");

        public IActionResult SecondAction(string id) => Content($"Action with values id:{id}");

        public void Throw() => throw new ApplicationException("Test error!");

        public object ErrorStatus(string code) => code switch
        { "404" => RedirectToAction(nameof(Error404)),
            _ => Content($"Error code: {code}")
        };
    }
}
