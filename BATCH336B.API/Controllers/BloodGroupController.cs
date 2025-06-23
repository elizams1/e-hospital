using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BloodGroupController : Controller
    {
        public DABloodGroup bloodGroup;

        public BloodGroupController(BATCH336BContext _db)
        {
            bloodGroup = new DABloodGroup(_db);
        }

        [HttpGet]
        public VMResponse Get() => bloodGroup.Get();

        [HttpGet("[action]/{filter?}")]
        public VMResponse Get(string? filter) => bloodGroup.Get(filter);

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(long? id) => bloodGroup.GetById(id);

        [HttpPost]
        public VMResponse Create(VMMBloodGroup data) => bloodGroup.Create(data);


        [HttpPut]
        public VMResponse Update(VMMBloodGroup data) => bloodGroup.Update(data);


        [HttpDelete]
        public VMResponse Delete(int id, int userId) => bloodGroup.Delete(id, userId);

        [HttpGet("[action]/{code?}")]
        public VMResponse GetByCode(string? code) => bloodGroup.GetByCode(code);
    }
}
