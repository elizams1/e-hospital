using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using System.Net;

namespace BATCH336B.DataAccess
{
    public class DADoctor
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DADoctor(BATCH336BContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMMDoctor> data = (
                    from d in db.MDoctors
                    join b in db.MBiodata on d.BiodataId equals b.Id
                    where d.IsDelete == false
                    select new VMMDoctor
                    {
                        Id=d.Id,
                        BiodataId=b.Id,
                        Str = d.Str,
                        CreateBy = d.CreateBy,
                        CreateOn = (DateTime)d.CreateOn!,
                        ModifiedBy = d.ModifiedBy,
                        ModifiedOn = d.ModifiedOn,
                        DeletedBy = d.DeletedBy,
                        DeletedOn = d.DeletedOn,
                        IsDelete = d.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} doctor data Successfully fetched!" : "doctor has no Data!";
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
