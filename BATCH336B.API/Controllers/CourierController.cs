using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourierController : Controller
    {
        private DACourier courier;
        public CourierController(BATCH336BContext _db)
        {
            courier = new DACourier(_db);

        }

        [HttpGet]

        public VMResponse GetAll() => courier.GetByFilter();

        [HttpGet("[action]/{id?}")]

        public VMResponse Get(int id) => courier.GetById(id);

        [HttpGet("[action]/{name?}")]
        public VMResponse GetBy(string name) => courier.GetByFilter(name);


        [HttpPost]
        public VMResponse Create(VMMCourier data) => courier.Create(data);


        [HttpPut]
        public VMResponse Update(VMMCourier data) => courier.Update(data);


        [HttpDelete]
        public VMResponse Delete(int id, int userId) => courier.Delete(id, userId);
    }
}
