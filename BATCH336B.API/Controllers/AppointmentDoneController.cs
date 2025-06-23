using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentDoneController : Controller
    {
        public DAAppointmentDone appointmentDone;
        public AppointmentDoneController(BATCH336BContext _db)
        {
            appointmentDone = new DAAppointmentDone(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return appointmentDone.GetAll();
        }
    }
}
