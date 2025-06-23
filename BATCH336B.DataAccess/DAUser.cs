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
    public class DAUser
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DAUser(BATCH336BContext _db) { db = _db; }

        public VMResponse GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    VMMUser? data =
                    (
                    from u in db.MUsers
                    where u.IsDelete == false && u.Id == id
                    select new VMMUser
                    {
                        Id = u.Id,
                        BiodataId = u.BiodataId,
                        RoleId = u.RoleId,
                        Email = u.Email,
                        Password = u.Password,
                        LoginAttempt = u.LoginAttempt,
                        IsLocked = u.IsLocked,
                        LastLogin = u.LastLogin,
                        CreateBy = u.CreateBy,
                        CreateOn = (DateTime)u.CreateOn!,
                        ModifiedBy = u.ModifiedBy,
                        ModifiedOn = u.ModifiedOn,
                        DeletedBy = u.DeletedBy,
                        DeletedOn = u.DeletedOn,
                        IsDelete = u.IsDelete
                    }
                    ).FirstOrDefault();

                    //response.data = (data != null) ? data : null;
                    //response.message = (data != null) ? $"Category data Successfully fetched!" : $"id {id} Category has no Data!";
                    //response.StatusCode = (data != null) ? HttpStatusCode.OK : HttpStatusCode.NoContent;

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"User data Successfully fetched!";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"id {id} User has no Data!";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input User ID First!";
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
                List<VMMUser> data = (
                    from u in db.MUsers
                    where u.IsDelete == false
                    select new VMMUser
                    {
                        Id = u.Id,
                        BiodataId = u.BiodataId,
                        RoleId = u.RoleId,
                        Email = u.Email,
                        Password = u.Password,
                        LoginAttempt = u.LoginAttempt,
                        IsLocked = u.IsLocked,
                        LastLogin = u.LastLogin,
                        CreateBy = u.CreateBy,
                        CreateOn = (DateTime)u.CreateOn!,
                        ModifiedBy = u.ModifiedBy,
                        ModifiedOn = u.ModifiedOn,
                        DeletedBy = u.DeletedBy,
                        DeletedOn = u.DeletedOn,
                        IsDelete = u.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} User data Successfully fetched!" : "User has no Data!";
                response.StatusCode = (data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMResponse GetByEmail(string email)
        {
            try
            {
                if (email != null)
                {
                    VMMUser? data =
                    (
                    from u in db.MUsers
                    where u.IsDelete == false && u.Email == email
                    select new VMMUser
                    {
                        Id = u.Id,
                        BiodataId = u.BiodataId,
                        RoleId = u.RoleId,
                        Email = u.Email,
                        Password = u.Password,
                        LoginAttempt = u.LoginAttempt,
                        IsLocked = u.IsLocked,
                        LastLogin = u.LastLogin,
                        CreateBy = u.CreateBy,
                        CreateOn = (DateTime)u.CreateOn!,
                        ModifiedBy = u.ModifiedBy,
                        ModifiedOn = u.ModifiedOn,
                        DeletedBy = u.DeletedBy,
                        DeletedOn = u.DeletedOn,
                        IsDelete = u.IsDelete
                    }
                    ).FirstOrDefault();


                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"User data Successfully fetched!";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"email {email} User has no Data!";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input User Email First!";
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
        public VMResponse Update(VMMUser data)
        {
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {

                    VMMUser? existingData = (VMMUser?)GetById((int)data.Id).data;

                    if (existingData != null)
                    {
                        MUser user = new MUser();

                        user.Id = existingData.Id;
                        user.BiodataId = existingData.BiodataId;
                        user.Email = existingData.Email;
                        user.Password = existingData.Password;
                        user.RoleId = existingData.RoleId;
                        user.CreateBy = existingData.CreateBy;
                        user.CreateOn = existingData.CreateOn;
                        user.ModifiedBy = existingData.Id;
                        user.ModifiedOn = DateTime.Now;
                        
                        user.DeletedBy = data.DeletedBy != null ? data.DeletedBy : existingData.DeletedBy;
                        user.DeletedOn = data.DeletedOn != null ? user.ModifiedOn : existingData.DeletedOn;
                        user.IsLocked = data.IsLocked != null ? data.IsLocked : existingData.IsLocked;
                        user.IsDelete = data.IsDelete != null ? data.IsDelete : existingData.IsDelete;

                        user.LoginAttempt = data.LoginAttempt;
                        user.LastLogin = data.LastLogin ?? null;
                        
                        db.Update(user);
                        db.SaveChanges();

                        //Save changes from Transaction to Database
                        dbTrans.Commit();

                        //Update API Response
                        response.data = data;
                        response.StatusCode = HttpStatusCode.OK;
                        response.message = $"Update with Name {data.Id} has been Succesfully Updated!";

                    }
                    else
                    {
                        response.data = data;
                        response.message = $"Requested Data Can't be Updated!";
                        response.StatusCode = HttpStatusCode.NotFound;
                    }
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();

                    response.data = data;
                    response.message = "Category has been Failed Updated! : " + ex.Message;
                }
            }

            return response;
        }
    }
}
