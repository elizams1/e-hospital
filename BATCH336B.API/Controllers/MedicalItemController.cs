using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalItemController : Controller
    {
        private DAMedicalItem medicalItem;

        public MedicalItemController(BATCH336BContext _db)
        {
            medicalItem = new DAMedicalItem(_db);
        }

        [HttpGet]
        public VMResponse GetAll() => medicalItem.GetAll();

        [HttpGet("[action]/{id?}")]
        public VMResponse Get(int id) => medicalItem.GetById(id);

        [HttpGet("[action]/{filter?}")]
        public VMResponse GetByFilter(string filter) => medicalItem.GetByFilter(filter);

        //[HttpGet("[action]/{segmen?}/{filter?}/{minPrice?}/{maxPrice?}/{cat?}")]
        [HttpGet("[action]")]
        public VMResponse GetByFilter(string? segmen, string? filter, int? minPrice, int? maxPrice, string? cat)
            => medicalItem.GetByFilter(segmen, filter, minPrice, maxPrice, cat);

        [HttpPost]
        public VMResponse Create(VMMMedicalItem data) => medicalItem.Create(data);

        [HttpPut]
        public VMResponse Update(VMMMedicalItem data) => medicalItem.Update(data);

        [HttpDelete]
        public VMResponse Delete(int id, int userId) => medicalItem.Delete(id, userId);
    }
}
