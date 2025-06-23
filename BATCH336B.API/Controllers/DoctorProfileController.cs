using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorProfileController : Controller
    {
        public DADoctorProfile doctorProfile;

        public DoctorProfileController(BATCH336BContext _db)
        {
            doctorProfile = new DADoctorProfile(_db);
        }

        [HttpGet("[action]/{id?}")]
        public VMResponse GetDoctorById(long? id) => doctorProfile.GetDoctorById(id);

        [HttpGet("[action]/{id?}")]
        public VMResponse GetCustomerById(long? id) => doctorProfile.GetCustomerById(id);


        [HttpGet("[action]")]
        public VMResponse GetAllAppointment(long? id, string? tgl) => doctorProfile.GetAllAppointment(id, tgl);

        [HttpGet("[action]")]
        public VMResponse GetAllAppointmentBySche(long? id, string? tgl) => doctorProfile.GetAllAppointmentBySche(id, tgl);

        [HttpPost]
        public VMResponse Create(VMTTAppointmnet data) =>doctorProfile.Create(data);
    }
}
