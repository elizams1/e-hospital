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
    public class DAAppointment
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DAAppointment(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMTAppointment> data = (
                    from a in db.TAppointments
                    join c in db.MCustomers on a.CustomerId equals c.Id
                    join doo in db.TDoctorOffices on a.DoctorOfficeId equals doo.Id
                    join dos in db.TDoctorOfficeSchedules on a.DoctorOfficeScheduleId equals dos.Id
                    join dot in db.TDoctorOfficeTreatments on a.DoctorOfficeTreatmentId equals dot.Id

                    where a.IsDelete == false
                    select new VMTAppointment
                    {
                        Id = a.Id,
                        CustomerId = c.Id,
                        DoctorOfficeId = doo.Id,
                        DoctorOfficeScheduleId = dos.Id,
                        DoctorOfficeTreatmentId = dot.Id,
                        AppointmentDate = a.AppointmentDate,
                        CreatedBy = a.CreatedBy,
                        CreatedOn = a.CreatedOn,
                        ModifiedBy = a.ModifiedBy,
                        ModifiedOn = a.ModifiedOn,
                        DeletedBy = a.DeletedBy,
                        DeletedOn = a.DeletedOn,
                        IsDelete = a.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Appointment data Successfully fetched!" : "Appointment has no Data!";
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
