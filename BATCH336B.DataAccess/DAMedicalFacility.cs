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
    public class DAMedicalFacility
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DAMedicalFacility(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMMMedicalFacility> data = (
                    from mf in db.MMedicalFacilities
                    join mfc in db.MMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                    join l in db.MLocations on mf.LocationId equals l.Id

                    where mf.IsDelete == false
                    select new VMMMedicalFacility
                    {
                        Id = mf.Id,
                        Name = mf.Name,
                        MedicalFacilityCategoryId = mfc.Id,
                        LocationId = l.Id,
                        FullAddress = mf.FullAddress,
                        Email = mf.Email,
                        PhoneCode = mf.PhoneCode,
                        Phone = mf.Phone,
                        Fax = mf.Fax,
                        CreatedBy = mf.CreatedBy,
                        CreatedOn = mf.CreatedOn,
                        ModifiedBy = mf.ModifiedBy,
                        ModifiedOn = mf.ModifiedOn,
                        DeletedBy = mf.DeletedBy,
                        DeletedOn = mf.DeletedOn,
                        IsDelete = mf.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Medical facility data Successfully fetched!" : "Medical facility has no Data!";
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
