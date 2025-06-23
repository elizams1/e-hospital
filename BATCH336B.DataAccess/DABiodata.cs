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
    public class DABiodata
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;
        public DABiodata(BATCH336BContext _db) { db = _db; }

        public VMResponse GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    VMMBiodatum? data =
                    (
                    from b in db.MBiodata
                    where b.IsDelete == false && b.Id == id
                    select new VMMBiodatum
                    {
                        Id = b.Id,
                        Fullname = b.Fullname,
                        MobilePhone = b.MobilePhone,
                        Image = b.Image,
                        ImagePath = b.ImagePath,
                        CreateBy = b.CreateBy,
                        CreateOn = (DateTime)b.CreateOn!,
                        ModifiedBy = b.ModifiedBy,
                        ModifiedOn = b.ModifiedOn,
                        DeletedBy = b.DeletedBy,
                        DeletedOn = b.DeletedOn,
                        IsDelete = b.IsDelete
                    }
                    ).FirstOrDefault();

                    //response.data = (data != null) ? data : null;
                    //response.message = (data != null) ? $"Category data Successfully fetched!" : $"id {id} Category has no Data!";
                    //response.StatusCode = (data != null) ? HttpStatusCode.OK : HttpStatusCode.NoContent;

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Biodata data Successfully fetched!";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"id {id} Biodata has no Data!";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input Biodata ID First!";
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
                List<VMMBiodatum> data = (
                    from b in db.MBiodata
                    where b.IsDelete == false && b.IsDelete == false
                    select new VMMBiodatum
                    {
                        Id = b.Id,
                        Fullname = b.Fullname,
                        MobilePhone = b.MobilePhone,
                        Image = b.Image,
                        ImagePath = b.ImagePath,
                        CreateBy = b.CreateBy,
                        CreateOn = (DateTime)b.CreateOn!,
                        ModifiedBy = b.ModifiedBy,
                        ModifiedOn = b.ModifiedOn,
                        DeletedBy = b.DeletedBy,
                        DeletedOn = b.DeletedOn,
                        IsDelete = b.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Biodata data Successfully fetched!" : "Biodata has no Data!";
                response.StatusCode = (data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMResponse Update(VMMBiodatum data)
        {
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {

                    VMMBiodatum? existingData = (VMMBiodatum?)GetById((int)data.Id).data;

                    if (existingData != null)
                    {
                        MBiodatum biodata = new MBiodatum();

                        biodata.Id = existingData.Id;
                        biodata.Fullname = existingData.Fullname;
                        biodata.MobilePhone = existingData.MobilePhone;
                        biodata.CreateBy = existingData.CreateBy;
                        biodata.CreateOn = existingData.CreateOn;
                        biodata.IsDelete = data.IsDelete != null ? data.IsDelete : existingData.IsDelete;
                        biodata.ModifiedBy = data.ModifiedBy;
                        biodata.ModifiedOn = DateTime.Now;
                        biodata.IsDelete = data.IsDelete;
                        biodata.DeletedBy = data.DeletedBy;
                        biodata.DeletedOn = data.DeletedOn;
                        db.Update(biodata);
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
