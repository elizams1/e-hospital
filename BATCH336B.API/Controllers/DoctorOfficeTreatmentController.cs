using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorOfficeTreatmentController : Controller
    {
        public DADoctorOfficeTreatment doctorOfficeTreatment;
        public DoctorOfficeTreatmentController(BATCH336BContext _db)
        {
            doctorOfficeTreatment = new DADoctorOfficeTreatment(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return doctorOfficeTreatment.GetAll();
        }
    }
}
