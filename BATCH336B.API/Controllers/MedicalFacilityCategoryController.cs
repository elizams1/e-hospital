using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalFacilityCategoryController : Controller
    {
        public DAMedicalFacilityCategory medicalFacilityCategory;
        public MedicalFacilityCategoryController(BATCH336BContext _db)
        {
            medicalFacilityCategory = new DAMedicalFacilityCategory(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return medicalFacilityCategory.GetAll();
        }
    }
}
