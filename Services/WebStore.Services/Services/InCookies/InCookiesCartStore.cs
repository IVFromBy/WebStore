using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore.Domain.Entites;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services.InCookies
{
    public class InCookiesCartStore : ICartStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _CartName;

        public InCookiesCartStore(IHttpContextAccessor httpContextAccessor, string cartName)
        {
            _httpContextAccessor = httpContextAccessor;
            var user = httpContextAccessor.HttpContext.User;
            var user_name = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;
            _CartName = $"WebStore.Cart{user_name}";
        }

        public Cart Cart
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                var cookies = context!.Response.Cookies;
                var car_cookies = context.Request.Cookies[_CartName];
                if (car_cookies is null)
                {
                    var cart = new Cart();
                    cookies.Append(_CartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }
                ReplaceCookies(cookies, car_cookies);
                return JsonConvert.DeserializeObject<Cart>(car_cookies);
            }
            set => ReplaceCookies(_httpContextAccessor.HttpContext!.Response.Cookies, JsonConvert.SerializeObject(value));
        }

        private void ReplaceCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_CartName);
            cookies.Append(_CartName, cookie);
        }
    }
}
