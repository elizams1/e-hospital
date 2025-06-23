
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;

namespace BATCH336B.DataAccess
{
    public class DAMenu
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DAMenu(BATCH336BContext _db) { db = _db; }

        
        public VMResponse GetAll()
        {
            try
            {
                List<VMMMenu> data = (
                    from c in db.MMenus
                    where c.IsDelete == false
                    select new VMMMenu
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Url = c.Url,
                        ParentId = c.ParentId,
                        BigIcon = c.BigIcon,
                        SmallIcon = c.SmallIcon,
                        CreateBy = c.CreateBy,
                        CreateOn = (DateTime)c.CreateOn!,
                        ModifiedBy = c.ModifiedBy,
                        ModifiedOn = c.ModifiedOn,
                        DeletedBy = c.DeletedBy,
                        DeletedOn = c.DeletedOn,
                        IsDelete = c.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0 ) ? $"{data.Count} Menu data Successfully fetched!" : "Menu has no Data!";
                response.StatusCode = (data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMResponse GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    VMMMenu? data =
                    (
                    from c in db.MMenus
                    where c.IsDelete == false && c.Id == id
                    select new VMMMenu
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Url = c.Url,
                        ParentId = c.ParentId,
                        BigIcon = c.BigIcon,
                        SmallIcon = c.SmallIcon,
                        CreateBy = c.CreateBy,
                        CreateOn = (DateTime)c.CreateOn!,
                        ModifiedBy = c.ModifiedBy,
                        ModifiedOn = c.ModifiedOn,
                        DeletedBy = c.DeletedBy,
                        DeletedOn = c.DeletedOn,
                        IsDelete = c.IsDelete
                    }
                    ).FirstOrDefault();

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Menu Id data Successfully fetched!";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"id {id} Menu Id has no Data!";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input Menu ID First!";
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
        public VMResponse Create(VMMMenu data)
        {
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    MMenu menu = new MMenu();

                    menu.Name = data.Name;
                    menu.Url = data.Url;
                    menu.ParentId = menu.Id;
                    menu.BigIcon = data.BigIcon;
                    menu.SmallIcon = data.SmallIcon;

                    menu.CreateBy = data.CreateBy;
                    menu.CreateOn = DateTime.Now;

                    menu.IsDelete = false;

                    db.Add(menu);
                    db.SaveChanges();

                    dbTrans.Commit();

                    response.data = menu;
                    response.StatusCode=HttpStatusCode.Created;
                    response.message = "New Menu has been Successfully Created!";

                }
                catch (Exception e)
                {
                    //Undo changes from Transaction process
                    dbTrans.Rollback();

                    response.data = data;
                    response.message = "New Menu has been Failed Created! : " + e.Message;
                }
            }
                return response;
        }
    }
}
