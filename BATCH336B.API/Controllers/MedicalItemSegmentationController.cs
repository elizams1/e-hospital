using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalItemSegmentationController : Controller
    {
        private DAMedicalItemSegmentation medicalItemSegmentation;

        public MedicalItemSegmentationController(BATCH336BContext _db)
        {
            medicalItemSegmentation = new DAMedicalItemSegmentation(_db);
        }

        [HttpGet]
        public VMResponse GetAll() => medicalItemSegmentation.GetAll();

        [HttpGet("[action]/{id?}")]
        public VMResponse Get(int id) => medicalItemSegmentation.GetById(id);

        [HttpGet("[action]/{filter?}")]
        public VMResponse GetByFilter(string filter) => medicalItemSegmentation.GetByFilter(filter);

        [HttpPost]
        public VMResponse Create(VMMMedicalItemSegmentation data) => medicalItemSegmentation.Create(data);

        [HttpPut]
        public VMResponse Update(VMMMedicalItemSegmentation data) => medicalItemSegmentation.Update(data);

        [HttpDelete]
        public VMResponse Delete(int id, int userId) => medicalItemSegmentation.Delete(id, userId);
    }
}
