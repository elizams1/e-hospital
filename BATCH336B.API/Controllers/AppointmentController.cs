using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : Controller
    {
        public DAAppointment appointment;
        public AppointmentController(BATCH336BContext _db)
        {
            appointment = new DAAppointment(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return appointment.GetAll();
        }
    }
}
