using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        public DAMenu menu;

        public MenuController(BATCH336BContext _db)
        {
            menu = new DAMenu(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return menu.GetAll();
        }

        [HttpPost]
        public VMResponse Create(VMMMenu data)
        {
            return menu.Create(data);
        }
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id)
        {
            return menu.GetById(id);
        }
    }
}
