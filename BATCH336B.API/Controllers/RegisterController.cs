using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private DARegister register;
        public VMResponse response = new VMResponse();
        public RegisterController(BATCH336BContext _db)
        {
            register = new DARegister(_db);
        }
        [HttpPost]
        public VMResponse Create(VMMRegister data)
        {
            return register.Create(data);
        }

        [HttpGet("[action]/{email?}")]
        public VMResponse GetByEmail(string email)
        {
            return register.GetByEmail(email);
        }
        [HttpPost("[action]/{target?}/{otp?}")]
        public VMResponse SendEmail(string target, int otp)
        {
            try
            {
                var email = new MimeMessage();

                //Dikirim dari email
                email.From.Add(MailboxAddress.Parse("wendyfirdiansyah86@gmail.com"));
                //Dikirim ke ?
                email.To.Add(MailboxAddress.Parse(target));
                //Isi Email
                email.Subject = "Verifikasi OTP for Register Account In BATCH336B";
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = "" +
                    "<h3>Verifikasi OTP for Register Account In BATCH336B</h3><br>" +
                    "<p>OTP code will be expire in 10 minutes</p><br>" +
                    "<h5>OTP Code : </h5><br>" +
                    $"<div><h3><b>{otp}</b></h3></div>"
                };
                // Simple Mail Transfer Protocol
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("wendyfirdiansyah86@gmail.com", "xrlu jizk vhbh kqpo");
                smtp.Send(email);
                smtp.Disconnect(true);

                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.message = "Successfully for sending email!";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }
    }
}
