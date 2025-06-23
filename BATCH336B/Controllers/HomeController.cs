using BATCH336B.DataAccess;
using BATCH336B.Models;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BATCH336B.Controllers
{
    public class HomeController : Controller
    {
        private readonly MenuModel? menuModel;
        private readonly MenuRoleModel? menuRoleModel;

        private readonly string imageFolder;
        private VMResponse? response;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            _logger = logger;
            menuModel = new MenuModel(_config, _webHostEnv);
            menuRoleModel = new MenuRoleModel(_config);
            imageFolder = _config["ImagesFolder"];
        }

        public IActionResult Index()
        {
            List<VMMMenu> dataIndex = new List<VMMMenu>();
            List<VMMMenu>? data = menuModel?.GetAll();
            List<VMMMenuRole>? MenuRole = menuRoleModel?.GetAll();

            if(HttpContext.Session.GetInt32("roleId") == null)
            {
                HttpContext.Session.SetInt32("roleId", 5);
            }


            foreach (VMMMenuRole dataMenuRole in MenuRole!)
            {
                foreach(VMMMenu dataMenu in data!)
                {
                    if(dataMenuRole.MenuId == dataMenu.Id)
                    {
                        if(dataMenuRole.RoleId == HttpContext.Session.GetInt32("roleId"))
                        {
                            dataIndex.Add(dataMenu);
                        }
                    }
                }
            }

            ViewBag.ImgFolder = imageFolder;

            return View(dataIndex);
        }

        public IActionResult Add()
        {
            ViewBag.ImgFolder = imageFolder;

            return View();
        }

        [HttpPost]
        public VMResponse? Add(VMMMenu formMenu)
        {
            response = menuModel?.Create(formMenu);

            return response;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
