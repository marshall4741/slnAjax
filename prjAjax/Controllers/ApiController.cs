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
            //System.Threading.Thread.Sleep(1000);


            return Content($"Hello {user.name} , {user.age}");
            //return Content("<h2>Ajax 你好</h2>","text/html",System.Text.Encoding.UTF8);
        }

        public IActionResult Register(Members member,IFormFile formFile)
        {
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

            //return Content(strPath);



            //=======將資料寫進資料庫中===========================



            member.FileName = formFile.FileName;
            //將上傳的圖轉成二進位
            byte[]? imgByte = null;
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }
            member.FileData = imgByte;

            //將資料寫進資料庫中
            _context.Members.Add(member);
            _context.SaveChanges();

            return Content("新增成功");



            //==================================




            //// 在資料庫中搜尋名稱欄位
            //Members? member0 = _context.Members.FirstOrDefault(p => p.Name == member.name);

            //if (member0 != null)
            //{
            //    return Content($"{member.name} found in the database.");
            //}
            //else
            //{
            //    return Content($"{member.name} not found in the database.");
            //}




            //==================================


            //取得上傳的圖片的資訊
            //string fileInfo = $"{formFile?.FileName} - {formFile?.Length} - {formFile?.ContentType}";
            //return Content(fileInfo);


            //==================================


            //只要ViewModel中的類型名稱跟<input name="email">的"name"一樣，系統就會自動識別
            //return Content($"Hello {member.name} , {member.email} , {member.age}");
        }

        //找到Address中的所有不重複City
        public IActionResult Cities()
        {
            var cities = _context.Address.Select(c => c.City).Distinct();
            return Json(cities);
        }

        //找到Address中的City的所有不重複SiteId
        //如:https://localhost:7051/Api/districts?city=金門縣
        public IActionResult districts(string city)
        {
            var districts = _context.Address.Where(a => a.City == city)
                .Select(a => a.SiteId).Distinct();
            return Json(districts);
        }

        //找到Address中的City的SiteId的所有不重複Road
        //如:https://localhost:7051/Api/road?SiteId=金門縣金城鎮
        public IActionResult roads(string SiteId)
        {
            var road = _context.Address.Where(a => a.SiteId == SiteId)
                .Select(a => a.Road).Distinct();
            return Json(road);
        }


        //讀取資料庫中二進位的圖片
        public IActionResult GetImageByte(int id = 1)//預設抓到會員2號
        {
            Members? member = _context.Members.Find(id);//這裡要抓到會員用ID就夠了，所以用Find()
            byte[]? img = member?.FileData;

            if (img != null)
            {
                return File(img, "image/jpeg");
            }
            return NotFound();
        }
    }
}
