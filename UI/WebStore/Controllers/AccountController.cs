using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entites.Identity;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> SignInManager, ILogger<AccountController> logger)
        {
            _UserManager = userManager;
            _SignInManager = SignInManager;
            _logger = logger;
        }

        #region Register
        [AllowAnonymous]
        public async Task<IActionResult> InNameFree(string UserName)
        {
            var user = await _UserManager.FindByNameAsync(UserName);
            return Json(user is null ? "true" : "Пользователь с таким именем уже существует");
        }

        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterViewModel());

        
        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);
            _logger.LogInformation($"Регистрация пользователя {Model.UserName}");

            var user = new User
            {
                UserName = Model.UserName,
            };

            using (_logger.BeginScope("Регистрация пользователя {Model.UserName}"))
            {
                var registration_result = await _UserManager.CreateAsync(user, Model.Password);

                if (registration_result.Succeeded)
                {
                    _logger.LogInformation($"Пользователь {Model.UserName} успешно зарегестрирован");
                    await _UserManager.AddToRoleAsync(user, Role.User);
                    _logger.LogInformation($"Пользователь {Model.UserName} наделён ролью {Role.User}");
                    await _SignInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                _logger.LogWarning("В процессер регистрации пользователя {0} возникли ошибки {1} "
                    , Model.UserName
                    , string.Join(',', registration_result.Errors.Select(e => e.Description)));

                foreach (var error in registration_result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

                return View(Model);
        }
        #endregion

        #region Login
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
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
