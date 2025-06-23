
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;

namespace BATCH336B.DataAccess
{
    public class DAToken
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DAToken(BATCH336BContext _db) { db = _db; }

        
        public VMResponse GetAll()
        {
            try
            {
                List<VMTToken> data = (
                    from c in db.TTokens
                    where c.IsDelete == false
                    select new VMTToken
                    {
                        Id = c.Id,
                        Email = c.Email,
                        UserId = c.UserId,
                        Token = c.Token,
                        ExpiredOn = c.ExpiredOn,
                        IsExpired = c.IsExpired,
                        UsedFor = c.UsedFor,
                        CreatedBy = c.CreatedBy,
                        CreatedOn = c.CreatedOn,
                        ModifiedBy = c.ModifiedBy,
                        ModifiedOn = c.ModifiedOn,
                        DeletedBy = c.DeletedBy,
                        DeletedOn = c.DeletedOn,
                        IsDelete = c.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0 ) ? $"{data.Count} Token data Successfully fetched!" : "Token has no Data!";
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
                    VMTToken? data =
                    (
                    from c in db.TTokens
                    where c.IsDelete == false && c.Id == id
                    select new VMTToken
                    {
                        Id = c.Id,
                        Email = c.Email,
                        UserId = c.UserId,
                        Token = c.Token,
                        ExpiredOn = c.ExpiredOn,
                        IsExpired = c.IsExpired,
                        UsedFor = c.UsedFor,
                        CreatedBy = c.CreatedBy,
                        CreatedOn = c.CreatedOn,
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
                        response.message = $"Token Id data Successfully fetched!";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"id {id} Token Id has no Data!";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input Token ID First!";
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

        public VMResponse GetByUserId(int userid)
        {
            try
            {
                if (userid > 0)
                {
                    VMTToken? data =
                    (
                    from c in db.TTokens
                    where c.IsDelete == false && c.UserId == userid
                    select new VMTToken
                    {
                        Id = c.Id,
                        Email = c.Email,
                        UserId = c.UserId,
                        Token = c.Token,
                        ExpiredOn = c.ExpiredOn,
                        IsExpired = c.IsExpired,
                        UsedFor = c.UsedFor,
                        CreatedBy = c.CreatedBy,
                        CreatedOn = c.CreatedOn,
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
                        response.message = $"Token user id data Successfully fetched!";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"user id {userid} Token user id has no Data!";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input Token user id First!";
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

        public VMResponse GetByEmail(string email)
        {
            try
            {
                if (email != null)
                {
                    List<VMTToken?> data =
                    (
                    from c in db.TTokens
                    where c.IsDelete == false && c.Email == email
                    select new VMTToken
                    {
                        Id = c.Id,
                        Email = c.Email,
                        UserId = c.UserId,
                        Token = c.Token,
                        ExpiredOn = c.ExpiredOn,
                        IsExpired = c.IsExpired,
                        UsedFor = c.UsedFor,
                        CreatedBy = c.CreatedBy,
                        CreatedOn = c.CreatedOn,
                        ModifiedBy = c.ModifiedBy,
                        ModifiedOn = c.ModifiedOn,
                        DeletedBy = c.DeletedBy,
                        DeletedOn = c.DeletedOn,
                        IsDelete = c.IsDelete
                    }
                    ).ToList();

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Token data Successfully fetched!";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"email {email} Token has no Data!";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input Token Email First!";
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
        public VMResponse Create(VMTToken data)
        {
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    TToken token = new TToken();

                    token.Email = data.Email;
                    token.UserId = data.UserId;
                    token.Token = data.Token;
                    token.ExpiredOn = data.ExpiredOn;
                    token.IsExpired = data.IsExpired;
                    token.UsedFor = data.UsedFor;
                    token.CreatedBy = data.CreatedBy;
                    token.CreatedOn = data.CreatedOn;
                    token.IsDelete = data.IsDelete;

                    db.Add(token);
                    db.SaveChanges();

                    dbTrans.Commit();

                    response.data = token;
                    response.StatusCode=HttpStatusCode.Created;
                    response.message = "New Token has been Successfully Created!";

                }
                catch (Exception e)
                {
                    //Undo changes from Transaction process
                    dbTrans.Rollback();

                    response.data = data;
                    response.message = "New Token has been Failed Created! : " + e.Message;
                }
            }
                return response;
        }

        public VMResponse Update(VMTToken data)
        {
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {

                    VMTToken? existingData = (VMTToken?)GetById((int)data.Id).data;

                    if (existingData != null)
                    {
                        TToken token = new TToken();

                        token.Id = existingData.Id;
                        token.Email = existingData.Email;
                        token.UserId = existingData.UserId;
                        token.Token = existingData.Token;
                        token.ExpiredOn = existingData.ExpiredOn;
                        token.IsExpired = data.IsExpired;
                        token.UsedFor = existingData.UsedFor;
                        token.CreatedBy = existingData.CreatedBy;
                        token.CreatedOn = existingData.CreatedOn;
                        token.IsDelete = existingData.IsDelete;
                        token.ModifiedBy = existingData.Id;
                        token.ModifiedOn = DateTime.Now;

                        db.Update(token);
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
                    response.message = "Token has been Failed Updated! : " + ex.Message;
                }
            }

            return response;
        }
    }
}
