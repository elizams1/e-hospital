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
    public class DAMedicalFacilityCategory
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DAMedicalFacilityCategory(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMMMedicalFacilityCategory> data = (
                    from mfc in db.MMedicalFacilityCategories

                    where mfc.IsDelete == false
                    select new VMMMedicalFacilityCategory
                    {
                        Id = mfc.Id,
                        Name = mfc.Name,
                        CreatedBy = mfc.CreatedBy,
                        CreatedOn = mfc.CreatedOn,
                        ModifiedBy = mfc.ModifiedBy,
                        ModifiedOn = mfc.ModifiedOn,
                        DeletedBy = mfc.DeletedBy,
                        DeletedOn = mfc.DeletedOn,
                        IsDelete = mfc.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Medical facility category data Successfully fetched!" : "Medical facility category has no Data!";
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
