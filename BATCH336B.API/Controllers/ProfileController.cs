using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        public DAProfile profile;

        public ProfileController(BATCH336BContext _db)
        {
            profile = new DAProfile(_db);
        }

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(long? id) => profile.GetById(id);

        [HttpPost("[action]/{userId?}/{email?}/{otp?}/{expiredToken?}")]
        public VMResponse SendOtp(int userId, string email, int otp, string expiredToken) => profile.SendOtp(userId,email,otp,expiredToken);

        [HttpPost("[action]/{email?}/{userId?}")]
        public VMResponse UpdateToken(string email, int userId) => profile.UpdateToken(email, userId);

        [HttpPut("[action]")]
        public VMResponse UpdateAkun(VMMProfile data) => profile.UpdateAkun(data);

        [HttpPut("[action]")]
        public VMResponse UpdateEmail(VMMProfile data) => profile.UpdateEmail(data);


        [HttpPut("[action]")]
        public VMResponse UpdatePassword(VMMProfile data) => profile.UpdatePassword(data);

        [HttpGet("[action]")]
        public VMResponse GetByEmailOtp(string email) => profile.GetByEmailOtp(email);


        [HttpPut("[action]")]
        public VMResponse UpdatePhotoProfile(VMMProfile data) => profile.UpdatePhotoProfile(data);
    }
}
