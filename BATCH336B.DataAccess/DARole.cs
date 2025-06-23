using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336B.DataAccess
{
    public class DARole
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DARole(BATCH336BContext _db) { db = _db; }

        public VMResponse GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    VMMRole? data =
                    (
                    from r in db.MRoles
                    where r.IsDelete == false && r.Id == id
                    select new VMMRole
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Code = r.Code,
                        CreateBy = r.CreateBy,
                        CreateOn = (DateTime)r.CreateOn!,
                        ModifiedBy = r.ModifiedBy,
                        ModifiedOn = r.ModifiedOn,
                        DeletedBy = r.DeletedBy,
                        DeletedOn = r.DeletedOn,
                        IsDelete = r.IsDelete
                    }
                    ).FirstOrDefault();

                    //response.data = (data != null) ? data : null;
                    //response.message = (data != null) ? $"Category data Successfully fetched!" : $"id {id} Category has no Data!";
                    //response.StatusCode = (data != null) ? HttpStatusCode.OK : HttpStatusCode.NoContent;

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Role data Successfully fetched!";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"id {id} Role has no Data!";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input Role ID First!";
                    response.StatusCode = HttpStatusCode.BadRequest;
                }

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }

        public VMResponse GetAll()
        {
            try
            {
                List<VMMRole> data = (
                    from r in db.MRoles
                    where r.IsDelete == false
                    select new VMMRole
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Code = r.Code,
                        CreateBy = r.CreateBy,
                        CreateOn = (DateTime)r.CreateOn!,
                        ModifiedBy = r.ModifiedBy,
                        ModifiedOn = r.ModifiedOn,
                        DeletedBy = r.DeletedBy,
                        DeletedOn = r.DeletedOn,
                        IsDelete = r.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Role data Successfully fetched!" : "Role has no Data!";
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
