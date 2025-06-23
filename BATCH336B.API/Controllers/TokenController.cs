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
    public class TokenController : Controller
    {
        private DAToken token;
        public VMResponse response = new VMResponse();
        public TokenController(BATCH336BContext _db)
        {
            token = new DAToken(_db);
        }
        [HttpPost]
        public VMResponse Create(VMTToken data)
        {
            return token.Create(data);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return token.GetAll();
        }

        [HttpGet("[action]/{userid?}")]
        public VMResponse GetByUserId(int userid)
        {
            return token.GetByUserId(userid);
        }

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id)
        {
            return token.GetById(id);
        }

        [HttpGet("[action]/{email?}")]
        public VMResponse GetByEmail(string email)
        {
            return token.GetByEmail(email);
        }

        [HttpPut]
        public VMResponse Update(VMTToken data)
        {
            return token.Update(data);
        }
    }
}
