using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336B.DataAccess
{
    public class DACustomer
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DACustomer(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMMCustomer> data = (
                    from c in db.MCustomers
                    join b in db.MBiodata on c.BiodataId equals b.Id
                    where c.IsDelete == false && b.IsDelete == false 
                    select new VMMCustomer
                    {
                        Id = c.Id,
                        BiodataId = b.Id,
                        Dob = c.Dob,
                        Gender = c.Gender,
                        CreateBy = c.CreateBy,
                        CreateOn = (DateTime)c.CreateOn!,
                        ModifiedBy = c.ModifiedBy,
                        ModifiedOn = c.ModifiedOn,
                        DeletedBy = c.DeletedBy,
                        DeletedOn = c.DeletedOn,
                        IsDelete = c.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Customer data Successfully fetched!" : "Customer has no Data!";
                response.StatusCode = (data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }

    }
}
