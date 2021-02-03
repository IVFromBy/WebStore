using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entites.Identity;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> SignInManager)
        {
            _UserManager = userManager;
            _SignInManager = SignInManager;
        }

        #region Register
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var user = new User
            {
                UserName = Model.UserName,
            };

            var registration_result = await _UserManager.CreateAsync(user, Model.Password);

            if (registration_result.Succeeded)
            {
                await _SignInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in registration_result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(Model);
        }
        #endregion

        #region Login
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var Login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
#if DEBUG
                false
#else
                true
#endif
                );

            if (Login_result.Succeeded)
            {
                return LocalRedirect(Model.ReturnUrl ?? "/");
                //if (Url.IsLocalUrl(Model.ReturnUrl))
                //    return Redirect(Model.ReturnUrl);
                //return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "неверное имя пользователя или пароль!");

            return View(Model);
        }
        #endregion


        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
    }
}
