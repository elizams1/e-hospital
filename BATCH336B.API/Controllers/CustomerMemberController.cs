using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerMemberController : Controller
    {
        public DACustomerMember customerMember;
        public CustomerMemberController(BATCH336BContext _db)
        {
            customerMember = new DACustomerMember(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return customerMember.GetAll();
        }
    }
}
