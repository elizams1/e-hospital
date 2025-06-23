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
    public class DADoctorOffice
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DADoctorOffice(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMTDoctorOffice> data = (
                    from doo in db.TDoctorOffices
                    join d in db.MDoctors on doo.DoctorId equals d.Id
                    join mf in db.MMedicalFacilities on doo.MedicalFacilityId equals mf.Id

                    where doo.IsDelete == false
                    select new VMTDoctorOffice
                    {
                        Id = doo.Id,
                        DoctorId = d.Id,
                        MedicalFacilityId = mf.Id,
                        Specialization = doo.Specialization,
                        StartDate = doo.StartDate,
                        EndDate = (DateTime)doo.EndDate,
                        CreatedBy = doo.CreatedBy,
                        CreatedOn = doo.CreatedOn,
                        ModifiedBy = doo.ModifiedBy,
                        ModifiedOn = doo.ModifiedOn,
                        DeletedBy = doo.DeletedBy,
                        DeletedOn = doo.DeletedOn,
                        IsDelete = doo.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Doctor office data Successfully fetched!" : "Doctor office has no Data!";
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
