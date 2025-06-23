using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalItemCategoryController : Controller
    {
        private DAMedicalItemCategory medicalItemCategory;

        public MedicalItemCategoryController(BATCH336BContext _db)
        {
            medicalItemCategory = new DAMedicalItemCategory(_db);
        }

        [HttpGet]
        public VMResponse GetAll() => medicalItemCategory.GetAll();

        [HttpGet("[action]/{id?}")]
        public VMResponse Get(int id) => medicalItemCategory.GetById(id);

        [HttpGet("[action]/{filter?}")]
        public VMResponse GetByFilter(string filter) => medicalItemCategory.GetByFilter(filter);

        [HttpPost]
        public VMResponse Create(VMMMedicalItemCategory data) => medicalItemCategory.Create(data);

        [HttpPut]
        public VMResponse Update(VMMMedicalItemCategory data) => medicalItemCategory.Update(data);

        [HttpDelete]
        public VMResponse Delete(int id, int userId) => medicalItemCategory.Delete(id, userId);
    }
}
