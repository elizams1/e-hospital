using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : Controller
    {
        public DADoctor doctor;
        public DoctorController(BATCH336BContext _db)
        {
            doctor = new DADoctor(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return doctor.GetAll();
        }
    }
}
