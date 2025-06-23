using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationController : Controller
    {
        public DASpecialization specialization;
        public SpecializationController(BATCH336BContext _db)
        {
            specialization = new DASpecialization(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return specialization.GetAll();
        }
    }
}
