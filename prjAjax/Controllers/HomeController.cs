using Microsoft.AspNetCore.Mvc;
using prjAjax.Models;
using System.Diagnostics;

namespace prjAjax.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoContext _context;

		public HomeController(ILogger<HomeController> logger, DemoContext context)
        {
            _logger = logger;
            _context = context;
		}



        public IActionResult Index()
        {
            return View();
        }



        public IActionResult First()
        {
            return View();
        }



        public IActionResult Register()
        {
            return View();
        }


        
        public IActionResult Address()
        {
            //連結資料庫，將城市，鄉鎮，路名做成下拉式選單
            return View();
        }



        public IActionResult Members()
		{
			return View(_context.Members);
		}


        
        public IActionResult Promise()
        {
            //Promise 物件代表一個即將完成、或失敗的非同步操作，以及它所產生的值。
            return View();
        }


        
        public IActionResult Fetch()
        {
            //Fetch API 提供了一個能獲取包含跨網路資源在的資源介面。
            //它有點像我們所熟悉的 XMLHttpRequest ，但這個新的 API 提供了更強更彈性的功能。
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}