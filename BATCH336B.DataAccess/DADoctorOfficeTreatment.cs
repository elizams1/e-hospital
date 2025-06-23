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
    public class DADoctorOfficeTreatment
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DADoctorOfficeTreatment(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMTDoctorOfficeTreatment> data = (
                    from dot in db.TDoctorOfficeTreatments
                    join dt in db.TDoctorTreatments on dot.DoctorTreatmentId equals dt.Id
                    join doo in db.TDoctorOffices on dot.DoctorOfficeId equals doo.Id

                    where dot.IsDelete == false
                    select new VMTDoctorOfficeTreatment
                    {
                        Id = dot.Id,
                        DoctorTreatmentId = dt.Id,
                        DoctorOfficeId = doo.Id,
                        CreatedBy = dot.CreatedBy,
                        CreatedOn = dot.CreatedOn,
                        ModifiedBy = dot.ModifiedBy,
                        ModifiedOn = dot.ModifiedOn,
                        DeletedBy = dot.DeletedBy,
                        DeletedOn = dot.DeletedOn,
                        IsDelete = dot.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Doctor Office Treatment data Successfully fetched!" : "Doctor Office Treatment has no Data!";
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
