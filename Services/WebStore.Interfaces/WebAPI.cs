using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Interfaces
{
    public static class WebAPI
    {
        public const string TestWebAPI = "api/values";
        public const string Emploees = "api/employees";
        public const string Products = "api/products";
        public const string Orders = "api/orders";

        public static class Identity
        {
            public const string User = "api/users";
            public const string Role = "api/roles";
        }
    }
}
