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
    public class DAPrescription
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DAPrescription(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMTPrescription> data = (
                    from p in db.TPrescriptions
                    join a in db.TAppointments on p.AppointmentId equals a.Id
                    join mi in db.MMedicalItems on p.MedicalItemId equals mi.Id

                    where p.IsDelete == false
                    select new VMTPrescription
                    {
                        Id = p.Id,
                        AppointmentId = a.Id,
                        MedicalItemId = mi.Id,
                        Dosage = p.Dosage,
                        Directions = p.Directions,
                        Time = p.Time,
                        Notes = p.Notes,
                        CreatedBy = p.CreatedBy,
                        CreatedOn = p.CreatedOn,
                        ModifiedBy = p.ModifiedBy,
                        ModifiedOn = p.ModifiedOn,
                        DeletedBy = p.DeletedBy,
                        DeletedOn = p.DeletedOn,
                        IsDelete = p.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Prescription data Successfully fetched!" : "Prescription has no Data!";
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
