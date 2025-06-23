using BATCH336B.DataModel;
using BATCH336B.Models;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;

namespace BATCH336B.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserModel? userModel;
        private readonly BiodataModel biodataModel;
        private readonly RoleModel roleModel;
        private readonly RegisterModel registerModel;
        private readonly TokenModel? tokenModel;

        private VMResponse? response = new VMResponse();
        public AuthController(IConfiguration _config)
        {
            userModel = new UserModel(_config);
            biodataModel = new BiodataModel(_config);
            roleModel = new RoleModel(_config);
            registerModel = new RegisterModel(_config);
            tokenModel = new TokenModel(_config);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public VMResponse Login(VMMUser data)
        {

            VMResponse? response = userModel!.GetByEmail(data.Email!);

            if (response != null)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    VMMUser? user = JsonConvert.DeserializeObject<VMMUser>(JsonConvert.SerializeObject(response.data));
                    VMMBiodatum? biodata = biodataModel.GetById((int)user!.BiodataId!);
                    VMMRole? role = new VMMRole();

                    if (user.RoleId != null)
                    {
                        role = roleModel.GetById((int)user.RoleId);
                    }
                    else
                    {
                        role.Name = "";
                    }

                    if (user.IsLocked == null || user.IsLocked == false)
                    {
                        if (data.Password == user!.Password)
                        {
                            HttpContext.Session.SetInt32("custId", (int)user.Id);
                            HttpContext.Session.SetInt32("roleId", (int)role.Id);
                            HttpContext.Session.SetString("custEmail", user.Email!);
                            HttpContext.Session.SetString("custFullname", biodata.Fullname!);
                            HttpContext.Session.SetInt32("custBiodataId", (int)user.BiodataId);
                            HttpContext.Session.SetString("custRoleName", role.Name!);

                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Role, user.RoleId.ToString()!),
                            };
                            var identity = new ClaimsIdentity(claims, "CookieAuth");
                            var principal = new ClaimsPrincipal(identity);
                            HttpContext.SignInAsync("CookieAuth", principal);
                            HttpContext.Session.SetString("successMsg", $"~Hallo, {HttpContext.Session.GetString("custFullname")}");
                            user.LoginAttempt = 0;
                            user.LastLogin = DateTime.Now;
                            userModel.Update(user);

                        }
                        else
                        {
                            response.StatusCode = System.Net.HttpStatusCode.NoContent;

                            if (user.LoginAttempt == null)
                            {
                                user.LoginAttempt = 1;
                                userModel.Update(user);
                            }
                            else
                            {
                                user.LoginAttempt++;
                                userModel.Update(user);
                            }

                            if (user.LoginAttempt >= 5)
                            {
                                user.IsLocked = true;
                                userModel.Update(user);
                            }
                            //HttpContext.Session.SetString("errorMsg", "Wrong Password...");
                        }
                    }
                    else
                    {
                        response.StatusCode = System.Net.HttpStatusCode.Forbidden;
                    }
                }
                else
                {
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    //HttpContext.Session.SetString("errorMsg", "Wrong Email...");
                }
            }

            return response;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Add()
        {
            ViewBag.Title = "Register";
            return View();
        }

        [HttpPost]
        public VMResponse? Add(VMMRegister data)
        {
            if(data.Biodatum.MobilePhone == "0")
            {
                data.Biodatum.MobilePhone = null;
            }
            else
            {
                data.Biodatum.MobilePhone = data.Biodatum.MobilePhone!.Replace(" ","");
                data.Biodatum.MobilePhone = data.Biodatum.MobilePhone!.Replace("-","");
            }


            response = registerModel.Create(data);
            if(response != null)
            {
                HttpContext.Session.SetString("successMsg", $"Account Successfully created, Please Login!");

            }
            else
            {
                HttpContext.Session.SetString("dangerMsg", $"Account Failed created!");
            }

            return response;
        }

        [HttpPost]
        public VMResponse? CheckEmail(string data)
        {
            response = registerModel.GetByEmail(data);
            return response;
        }
        //[HttpPost]
        //public VMResponse? SendEmail(string email)
        //{
        //    var random = new Random();
        //    int otp = random.Next(1000, 9999);
        //    response = registerModel.SendEmail(email, otp);

        //    HttpContext.Session.SetInt32("otp", otp);
        //    HttpContext.Session.SetString("firstGenerateOtp", DateTime.Now.ToString("HH:mm:ss"));

        //    return response;
        //}

        [HttpPost]
        public VMResponse? SendEmail(string email)
        {
            List<VMTToken>? cekToken = new List<VMTToken>();

            response = tokenModel!.GetByEmail(email!);

            cekToken = JsonConvert.DeserializeObject<List<VMTToken>>(JsonConvert.SerializeObject(response.data));

            if (cekToken != null)
            {
                foreach(VMTToken dataToken in cekToken)
                {
                    dataToken.IsExpired = true;
                    response = tokenModel!.Update(dataToken);
                }
            }
            VMTToken? token = new VMTToken();


            var random = new Random();

            var otp = random.Next(1000, 9999);
            response = registerModel.SendEmail(email, otp);

            token.Token = otp.ToString();
            token.Email = email;
            token.UserId = null;
            token.IsExpired = false;
            token.UsedFor = "Register Account";
            token.CreatedBy = -1;
            token.CreatedOn = DateTime.Now;
            token.ExpiredOn = token.CreatedOn + TimeSpan.FromMinutes(10);
            token.IsDelete = false;

            response = tokenModel!.Create(token);

            return response;
        }

        [HttpPost]
        public VMResponse? VerifikasiOtp(string inputOtp, string email)
        {
            List<VMTToken>? cekToken = new List<VMTToken>();
            List<VMTToken>? lastExpiredToken = new List<VMTToken>();

            response = tokenModel!.GetByEmail(email!);

            cekToken = JsonConvert.DeserializeObject<List<VMTToken>>(JsonConvert.SerializeObject(response.data));
            VMTToken? lastToken = cekToken!.LastOrDefault();
            string? expiredOtp = "";

            if (cekToken.Count > 1)
            {
                lastExpiredToken = cekToken!.OrderByDescending(x => x.CreatedOn).Skip(1).ToList();
                
                foreach(VMTToken cek in lastExpiredToken)
                {
                    if(inputOtp == cek.Token)
                    {
                        expiredOtp = cek.Token;
                    }
                }
            }

            string? kodeOtp = lastToken!.Token;

            if (lastToken.ExpiredOn < DateTime.Now)
            {
                expiredOtp = kodeOtp;
                lastToken.IsExpired = true;
                response = tokenModel!.Update(lastToken);
                kodeOtp = null;
            }
            if (inputOtp == kodeOtp)
            {
                response!.StatusCode = System.Net.HttpStatusCode.OK;
                response.message = "Successfully";
            }
            else if (inputOtp == expiredOtp)
            {
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

        //[HttpPost]
        //public VMResponse? VerifikasiOtp(int inputOtp)
        //{
        //    int? kodeOtp = HttpContext.Session.GetInt32("otp");
        //    DateTime? firstGenerateOtp = DateTime.Parse(HttpContext.Session.GetString("firstGenerateOtp"));

        //    int? expiredOtp = 0;

        //    if(DateTime.Now - firstGenerateOtp > TimeSpan.FromMinutes(10))
        //    {
        //        expiredOtp = kodeOtp;
        //        kodeOtp = null;
        //    }
        //    if(inputOtp == kodeOtp)
        //    {
        //        response!.StatusCode = System.Net.HttpStatusCode.OK;
        //        response.message = "Successfully";
        //    }
        //    else if(inputOtp == expiredOtp)
        //    {
        //        response!.StatusCode = System.Net.HttpStatusCode.NoContent;
        //        response.message = "Your OTP code has expired, please resend the OTP code!";
        //    }
        //    else
        //    {
        //        response!.StatusCode = System.Net.HttpStatusCode.NotFound;
        //        response.message = "OTP Code has been Wrong!";
        //    }

        //    return response;
        //}
    }
}