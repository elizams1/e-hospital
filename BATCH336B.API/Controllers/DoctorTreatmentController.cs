using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorTreatmentController : Controller
    {
        public DADoctorOffice doctorTreatment;
        public DoctorTreatmentController(BATCH336BContext _db)
        {
            doctorTreatment = new DADoctorOffice(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return doctorTreatment.GetAll();
        }
    }
}
