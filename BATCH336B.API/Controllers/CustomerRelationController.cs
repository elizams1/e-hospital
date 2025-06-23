using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerRelation : Controller
    {
        public DACustomerRelation customerRelation;
        public CustomerRelation(BATCH336BContext _db)
        {
            customerRelation = new DACustomerRelation(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return customerRelation.GetAll();
        }
    }
}
