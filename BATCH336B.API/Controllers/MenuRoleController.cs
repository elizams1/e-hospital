using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuRoleController : Controller
    {
        public DAMenuRole menuRole;
        public MenuRoleController(BATCH336BContext _db)
        {
            menuRole = new DAMenuRole(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return menuRole.GetAll();
        }

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id)
        {
            return menuRole.GetById(id);
        }

        [HttpGet("[action]/{menuId?}")]
        public VMResponse GetByMenuId(int menuId)
        {
            return menuRole.GetByMenuId(menuId);
        }

    }
}
