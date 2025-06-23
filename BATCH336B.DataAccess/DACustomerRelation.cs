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
    public class DACustomerRelation
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DACustomerRelation(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMMCustomerRelation> data = (
                    from cr in db.MCustomerRelations
                    where cr.IsDelete == false
                    select new VMMCustomerRelation
                    {
                        Id = cr.Id,
                        Name = cr.Name,
                        CreatedBy = cr.CreatedBy,
                        CreatedOn = cr.CreatedOn,
                        ModifiedBy = cr.ModifiedBy,
                        ModifiedOn = cr.ModifiedOn,
                        DeletedBy = cr.DeletedBy,
                        DeletedOn = cr.DeletedOn,
                        IsDelete = cr.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Customer Relation data Successfully fetched!" : "Customer Relation has no Data!";
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
