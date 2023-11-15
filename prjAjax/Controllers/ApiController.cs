using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using prjAjax.Models;
using prjAjax.Models.ViewModel;
using System.IO;

namespace prjAjax.Controllers
{
    public class ApiController : Controller
    {
        //================注入==================
        private readonly IWebHostEnvironment _host;
        private readonly DemoContext _context;
        public ApiController(IWebHostEnvironment host, DemoContext context)
        {
            _host = host;
            _context = context;
        }
        //======================================

        public IActionResult Index(UserViewModel user)
        {
            //延遲5秒
            System.Threading.Thread.Sleep(1000);

            return Content($"Hello {user.name} , {user.age}");
            //return Content("<h2>Ajax 你好</h2>","text/html",System.Text.Encoding.UTF8);
        }

        public IActionResult Register(MemberViewModel member,IFormFile formFile)
        {

            // 在資料庫中搜尋名稱欄位
            Members? member0 = _context.Members.FirstOrDefault(p => p.Name == member.name);

            if (member0 != null)
            {
                return Content($"{member.name} found in the database.");
            }
            else
            {
                return Content($"{member.name} not found in the database.");
            }




            //實際路徑
            //C:\Users\User\Documents\workspace\MSIT153Site\wwwroot\uploads\abc.jpg

            //string strPath = _host.ContentRootPath; //C:\Users\User\Documents\workspace\MSIT153Site
            //string strPath = _host.WebRootPath; //C:\Users\User\Documents\workspace\MSIT153Site\wwwroot


            //Path 可以將物件依你想要路徑，串成實際路徑
            //如：C:\Shared\Ajax\slnAjax\prjAjax\wwwroot\uploads\1.PNG
            string strPath = Path.Combine(_host.WebRootPath, "uploads", formFile.FileName);
            //                           (      根目錄      ,指定的資料夾,     檔案名稱     )


            //將檔案上傳到我指定的路徑
            using (var fileStream = new FileStream(strPath,FileMode.Create))//(路徑,要做什麼)
            {
                formFile.CopyTo(fileStream);
            }

            return Content(strPath);
            


            //==================================


            //取得上傳的圖片的資訊
            //string fileInfo = $"{formFile?.FileName} - {formFile?.Length} - {formFile?.ContentType}";
            //return Content(fileInfo);


            //==================================


            //只要ViewModel中的類型名稱跟<input name="email">的"name"一樣，系統就會自動識別
            //return Content($"Hello {member.name} , {member.email} , {member.age}");
        }
    }
}
