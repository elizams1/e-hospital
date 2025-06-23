using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrentDoctorSpecializationController : Controller
    {
        public DACurrentDoctorSpecialization currentDoctorSpecialization;
        public CurrentDoctorSpecializationController(BATCH336BContext _db)
        {
            currentDoctorSpecialization = new DACurrentDoctorSpecialization(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return currentDoctorSpecialization.GetAll();
        }
    }
}
