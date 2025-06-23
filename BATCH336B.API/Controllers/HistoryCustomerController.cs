using BATCH336B.DataAccess;
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryCustomerController : Controller
    {
        public DAHistoryCustomer historyCustomer;
        public HistoryCustomerController(BATCH336BContext _db)
        {
            historyCustomer = new DAHistoryCustomer(_db);
        }

        [HttpGet]
        public VMResponse GetAll()
        {
            return historyCustomer.GetAll();
        }

        [HttpGet("[action]/{filter?}")]
        public VMResponse GetByFilter(string filter)
        {
            return historyCustomer.GetByFilter(filter);
        }

        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id)
        {
            return historyCustomer.GetById(id);
        }

        [HttpPut]
        public VMResponse Update(VMTPrescription data)
        {
            return historyCustomer.Update(data);
        }
    }
}
