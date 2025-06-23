using BATCH336B.Models;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BATCH336B.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileModel? profileModel;
        private readonly RegisterModel? registerModel;
        private VMResponse? response = new VMResponse();
        private readonly string imageFolder;

        public ProfileController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            profileModel = new ProfileModel(_config, _webHostEnv);
            registerModel = new RegisterModel(_config);
            imageFolder = _config["ImagesFolder"];
        }

        public IActionResult Index(long id)
        {
            VMMProfile data = profileModel.GetById(id);
            ViewBag.ImgFolder = imageFolder;
            return View(data);
        }

        public IActionResult UpdatePhotoProfile(long id)
        {
            VMMProfile data = profileModel.GetById(id);
            ViewBag.Title = "Edit Photo Profile";
            ViewBag.ImgFolder = imageFolder;
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> UpdatePhotoProfile(VMMProfile formData)
        {
            response = await profileModel.UpdatePhotoProfileAsync(formData);
            return response;
        }

        public IActionResult ProfileMenu(VMMProfile data) {

            return View(data);
        }

        public IActionResult Pembayaran()
        {

            return View();
        }

        public IActionResult Alamat()
        {

            return View();
        }
        public IActionResult Role(int id)
        {
            return View(id);
        }

        public IActionResult Edit(long id)
        {
            VMMProfile? data = profileModel.GetById(id);

            ViewBag.Title = "Edit Data Pribadi";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> Edit(VMMProfile data)
        {

            response = await profileModel.UpdateAsync(data);

            return response;
        }

        public IActionResult UpdateEmail(long id)
        {
            ViewBag.Title = "Ubah Email";
            VMMProfile? data = profileModel.GetById(id);
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> UpdateEmail(VMMProfile data)
        {

            response = await profileModel.UpdateEmailAsync(data);
            return response;
        }

        [HttpPost]
        public VMResponse? CheckEmail(string email)
        {
            response = registerModel.GetByEmail(email);
            return response;
        }

        [HttpPost]
        public VMResponse? SendOtp(int userId,string email)
        {
            var random = new Random();
            int otp = random.Next(100000, 999999);
            string expiredToken = DateTime.Now.AddMinutes(1).ToString("yyyy-MM-dd HH:mm:ss.fff");
            response = profileModel.SendOtp(userId, email, otp, expiredToken);

            HttpContext.Session.SetInt32("otp", otp);
            HttpContext.Session.SetString("firstGenerateOtp", DateTime.Now.ToString("HH:mm:ss"));
            HttpContext.Session.SetString("email", email);
            return response;
        }

        [HttpPost]
        public VMResponse? VerifikasiOtp(int inputOtp)
        {
            int? kodeOtp = HttpContext.Session.GetInt32("otp");
            DateTime? firstGenerateOtp = DateTime.Parse(HttpContext.Session.GetString("firstGenerateOtp"));

            int? expiredOtp = 0;

            var email = HttpContext.Session.GetString("email");
            int id = HttpContext.Session.GetInt32("custId") ?? 0;

            if (DateTime.Now - firstGenerateOtp > TimeSpan.FromMinutes(1))
            {
                expiredOtp = kodeOtp;
                kodeOtp = null;
            }
            if (inputOtp == kodeOtp)
            {
                response.data = profileModel.UpdateToken(email, id).data;
                response!.StatusCode = System.Net.HttpStatusCode.OK;
                response.message = "Successfully";
            }
            else if (inputOtp == expiredOtp)
            {
                response.data = profileModel.UpdateToken(email, id).data;
                response!.StatusCode = System.Net.HttpStatusCode.NoContent;
                response.message = "Your OTP code has expired, please resend the OTP code!";
            }
            else
            {
                response!.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.message = "OTP Code has been Wrong!";
            }

            return response;
        }


        public IActionResult UpdatePassword(long id)
        {
            ViewBag.Title = "Ubah Password";
            VMMProfile? data = profileModel.GetById(id);
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> UpdatePassword(VMMProfile data)
        {

            response = await profileModel.UpdatePasswordAsync(data);
            return response;
        }

        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync("CookieAuth");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
