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
    public class DAAppointmentDone
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DAAppointmentDone(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMTAppointmentDone> data = (
                    from ad in db.TAppointmentDones
                    join a in db.TAppointments on ad.AppointmentId equals a.Id

                    where ad.IsDelete == false
                    select new VMTAppointmentDone
                    {
                        Id = ad.Id,
                        AppointmentId = a.Id,
                        Diagnosis = ad.Diagnosis,
                        CreatedBy = ad.CreatedBy,
                        CreatedOn = ad.CreatedOn,
                        ModifiedBy = ad.ModifiedBy,
                        ModifiedOn = ad.ModifiedOn,
                        DeletedBy = ad.DeletedBy,
                        DeletedOn = ad.DeletedOn,
                        IsDelete = ad.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Appointment done data Successfully fetched!" : "Appointment done has no Data!";
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
