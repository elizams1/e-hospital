using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalFacilityController : Controller
    {
        public DAMedicalFacility medicalFacility;
        public MedicalFacilityController(BATCH336BContext _db)
        {
            medicalFacility = new DAMedicalFacility(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return medicalFacility.GetAll();
        }
    }
}
