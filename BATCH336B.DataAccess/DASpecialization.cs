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
    public class DASpecialization
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DASpecialization(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMMSpecialization> data = (
                    from s in db.MSpecializations

                    where s.IsDelete == false
                    select new VMMSpecialization
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CreatedBy = s.CreatedBy,
                        CreatedOn = s.CreatedOn,
                        ModifiedBy = s.ModifiedBy,
                        ModifiedOn = s.ModifiedOn,
                        DeletedBy = s.DeletedBy,
                        DeletedOn = s.DeletedOn,
                        IsDelete = s.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Specialization data Successfully fetched!" : "Specialization has no Data!";
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
