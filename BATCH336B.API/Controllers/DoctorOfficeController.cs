using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorOfficeController : Controller
    {
        public DADoctorOffice doctorOffice;
        public DoctorOfficeController(BATCH336BContext _db)
        {
            doctorOffice = new DADoctorOffice(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return doctorOffice.GetAll();
        }
    }
}
