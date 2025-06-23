using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public DAUser user;
        public UserController(BATCH336BContext _db)
        {
            user = new DAUser(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return user.GetAll();
        }

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id)
        {
            return user.GetById(id);
        }

        [HttpGet("[action]/{email?}")]
        public VMResponse GetByEmail(string email)
        {
            return user.GetByEmail(email);
        }

        [HttpPut]
        public VMResponse Update(VMMUser data)
        {
            return user.Update(data);
        }
    }
}
