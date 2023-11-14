using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using prjAjax.Models.ViewModel;

namespace prjAjax.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index(UserViewModel user)
        {
            return Content($"Hello {user.name} , {user.age}");
            //return Content("<h2>Ajax 你好</h2>","text/html",System.Text.Encoding.UTF8);
        }
    }
}
