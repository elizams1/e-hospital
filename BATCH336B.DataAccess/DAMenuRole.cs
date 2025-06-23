using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using System.Net;
namespace BATCH336B.DataAccess
{
    public class DAMenuRole
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DAMenuRole(BATCH336BContext _db) { db = _db; }

        public VMResponse GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    VMMMenuRole? data =
                    (
                    from r in db.MMenuRoles
                    where r.IsDelete == false && r.Id == id
                    select new VMMMenuRole
                    {
                        Id = r.Id,
                        MenuId = r.MenuId,
                        RoleId = r.RoleId,
                        CreatedBy = r.CreatedBy,
                        CreatedOn = r.CreatedOn!,
                        ModifiedBy = r.ModifiedBy,
                        ModifiedOn = r.ModifiedOn,
                        DeletedBy = r.DeletedBy,
                        DeletedOn = r.DeletedOn,
                        IsDelete = r.IsDelete
                    }
                    ).FirstOrDefault();

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Menu Role data Successfully fetched!";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"id {id} Menu Role has no Data!";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input Menu Role ID First!";
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

        public VMResponse GetByMenuId(int menuId)
        {
            try
            {
                if (menuId > 0)
                {
                    VMMMenuRole? data =
                    (
                    from r in db.MMenuRoles
                    where r.IsDelete == false && r.MenuId == menuId
                    select new VMMMenuRole
                    {
                        Id = r.Id,
                        MenuId = r.MenuId,
                        RoleId = r.RoleId,
                        CreatedBy = r.CreatedBy,
                        CreatedOn = r.CreatedOn!,
                        ModifiedBy = r.ModifiedBy,
                        ModifiedOn = r.ModifiedOn,
                        DeletedBy = r.DeletedBy,
                        DeletedOn = r.DeletedOn,
                        IsDelete = r.IsDelete
                    }
                    ).FirstOrDefault();

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Menu Role data Successfully fetched!";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"id {menuId} Menu Role has no Data!";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input Menu Role ID First!";
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
                List<VMMMenuRole> data = (
                    from r in db.MMenuRoles
                    where r.IsDelete == false
                    select new VMMMenuRole
                    {
                        Id = r.Id,
                        MenuId = r.MenuId,
                        RoleId = r.RoleId,
                        CreatedBy = r.CreatedBy,
                        CreatedOn = r.CreatedOn!,
                        ModifiedBy = r.ModifiedBy,
                        ModifiedOn = r.ModifiedOn,
                        DeletedBy = r.DeletedBy,
                        DeletedOn = r.DeletedOn,
                        IsDelete = r.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Menu Role data Successfully fetched!" : "Menu Role has no Data!";
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
