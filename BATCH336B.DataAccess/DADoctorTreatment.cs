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
    public class DADoctorTreatment
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DADoctorTreatment(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMTDoctorTreatment> data = (
                    from dt in db.TDoctorTreatments
                    join d in db.MDoctors on dt.DoctorId equals d.Id

                    where dt.IsDelete == false
                    select new VMTDoctorTreatment
                    {
                        Id = dt.Id,
                        DoctorId = d.Id,
                        Name = dt.Name,
                        CreatedBy = dt.CreatedBy,
                        CreatedOn = dt.CreatedOn,
                        ModifiedBy = dt.ModifiedBy,
                        ModifiedOn = dt.ModifiedOn,
                        DeletedBy = dt.DeletedBy,
                        DeletedOn = dt.DeletedOn,
                        IsDelete = dt.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Doctor Treatment data Successfully fetched!" : "Doctor Treatment has no Data!";
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
