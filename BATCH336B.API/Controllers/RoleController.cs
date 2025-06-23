using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        public DARole role;
        public RoleController(BATCH336BContext _db)
        {
            role = new DARole(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return role.GetAll();
        }

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id)
        {
            return role.GetById(id);
        }

    }
}
