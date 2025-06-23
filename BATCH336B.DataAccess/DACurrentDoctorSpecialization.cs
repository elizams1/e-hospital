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
    public class DACurrentDoctorSpecialization
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DACurrentDoctorSpecialization(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMTCurrentDoctorSpecialization> data = (
                    from cds in db.TCurrentDoctorSpecializations
                    join d in db.MDoctors on cds.DoctorId equals d.Id
                    join s in db.MSpecializations on cds.SpecializationId equals s.Id
                    where cds.IsDelete == false
                    select new VMTCurrentDoctorSpecialization
                    {
                        Id = cds.Id,
                        DoctorId = d.Id,
                        SpecializationId = s.Id,
                        CreatedBy = cds.CreatedBy,
                        CreatedOn = cds.CreatedOn,
                        ModifiedBy = cds.ModifiedBy,
                        ModifiedOn = cds.ModifiedOn,
                        DeletedBy = cds.DeletedBy,
                        DeletedOn = cds.DeletedOn,
                        IsDelete = cds.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} current doctor specialization data Successfully fetched!" : "current doctor specialization has no Data!";
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
