using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BiodataController : Controller
    {
        public DABiodata biodata;
        public BiodataController(BATCH336BContext _db)
        {
            biodata = new DABiodata(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return biodata.GetAll();
        }

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id)
        {
            return biodata.GetById(id);
        }

        [HttpPut]
        public VMResponse Update(VMMBiodatum data)
        {
            return biodata.Update(data);
        }

    }
}
