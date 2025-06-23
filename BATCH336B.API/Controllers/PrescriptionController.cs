using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : Controller
    {
        public DAPrescription prescription;
        public PrescriptionController(BATCH336BContext _db)
        {
            prescription = new DAPrescription(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return prescription.GetAll();
        }
    }
}
