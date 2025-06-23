using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        public DACustomer customer;
        public CustomerController(BATCH336BContext _db)
        {
            customer = new DACustomer(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return customer.GetAll();
        }
    }
}
