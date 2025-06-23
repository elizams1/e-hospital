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
    public class DACustomerMember
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DACustomerMember(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMMCustomerMember> data = (
                    from cm in db.MCustomerMembers
                    join pb in db.MBiodata on cm.ParentBiodataId equals pb.Id
                    join c in db.MCustomers on cm.CustomerId equals c.Id
                    join cr in db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                    where cm.IsDelete == false
                    select new VMMCustomerMember
                    {
                        Id = cm.Id,
                        ParentBiodataId = pb.Id,
                        CustomerId = c.Id,
                        CustomerRelationId = cr.Id,
                        CreatedBy = cm.CreatedBy,
                        CreatedOn = cm.CreatedOn,
                        ModifiedBy = cm.ModifiedBy,
                        ModifiedOn = cm.ModifiedOn,
                        DeletedBy = cm.DeletedBy,
                        DeletedOn = cm.DeletedOn,
                        IsDelete = cm.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Customer Member data Successfully fetched!" : "Customer Member has no Data!";
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
