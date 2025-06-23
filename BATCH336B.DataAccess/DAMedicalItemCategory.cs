using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;

namespace BATCH336B.DataAccess
{
    public class DAMedicalItemCategory
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336BContext db;
        public DAMedicalItemCategory(BATCH336BContext _db)
        {
            db = _db;
        }

        public VMResponse GetAll() => GetByFilter("");

        public VMResponse GetByFilter(string filter)
        {
            try
            {
                List<VMMMedicalItemCategory> data = (
                    from mic in db.MMedicalItemCategories
                    where mic.IsDelete == false
                    && mic.Name.Contains(filter ?? "")
                    select new VMMMedicalItemCategory
                    {
                        Id = mic.Id,
                        Name = mic.Name,

                        CreatedBy = mic.CreatedBy,
                        CreatedOn = mic.CreatedOn,
                        ModifiedBy = mic.ModifiedBy,
                        ModifiedOn = mic.ModifiedOn,
                        DeletedBy = mic.DeletedBy,
                        DeletedOn = mic.DeletedOn,
                        IsDelete = mic.IsDelete
                    }
                    ).ToList();

                response.data = data;
                response.message = (data.Count > 0)
                        ? $"{data.Count} Medical item data Successfuly fetched!"
                        : "Medical item has no Data!";
                response.StatusCode = (data.Count > 0)
                    ? System.Net.HttpStatusCode.OK
                    : System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMResponse GetById(long id)
        {
            try
            {
                if (id > 0)
                {
                    VMMMedicalItemCategory? data = (
                    from mic in db.MMedicalItemCategories
                    where mic.IsDelete == false
                        && mic.Id == id
                    select new VMMMedicalItemCategory
                    {
                        Id = mic.Id,
                        Name = mic.Name,


                        CreatedBy = mic.CreatedBy,
                        CreatedOn = mic.CreatedOn,
                        ModifiedBy = mic.ModifiedBy,
                        ModifiedOn = mic.ModifiedOn,
                        DeletedBy = mic.DeletedBy,
                        DeletedOn = mic.DeletedOn,
                        IsDelete = mic.IsDelete
                    }
                    ).FirstOrDefault();

                    if (data != null)
                    {
                        response.data = data;
                        response.message = "Medical Item data Successfuly fetched!";
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Medical Item data could not be found";
                        response.StatusCode = System.Net.HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = "Please input the corred ID";
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMResponse Create(VMMMedicalItemCategory data)
        {
            //starting db trasaction proses
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    //preapare empty data model for new category data
                    MMedicalItemCategory medItemCategory = new MMedicalItemCategory();

                    medItemCategory.Name = data.Name;

                    medItemCategory.IsDelete = false;
                    medItemCategory.CreatedBy = data.CreatedBy;
                    medItemCategory.CreatedOn = DateTime.Now;

                    //proses membuat data kedalam DB tabel
                    db.Add(medItemCategory);
                    db.SaveChanges();

                    //save table changes from database transaction
                    dbTran.Commit();

                    //update API Respon
                    response.data = medItemCategory;
                    response.message = "New Medical Item has been succsesfully created";
                    response.StatusCode = System.Net.HttpStatusCode.Created;
                }
                catch (Exception ex)
                {
                    //undo table changes from database transaction proses
                    dbTran.Rollback();

                    response.data = data;
                    response.message = ex.Message;
                }
            };
            return response;
        }

        public VMResponse Update(VMMMedicalItemCategory data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMMedicalItemCategory? existingData = (VMMMedicalItemCategory?)GetById(data.Id).data;

                    if (existingData != null)
                    {
                        MMedicalItemCategory medItemCategory = new MMedicalItemCategory()
                        {
                            Id = existingData.Id,

                            Name = data.Name,

                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,

                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,

                            IsDelete = false,
                        };
                        //proses membuat data kedalam BD tabel
                        db.Update(medItemCategory);
                        db.SaveChanges();

                        //save table changes from database transaction
                        dbTran.Commit();                //biar masuk ke database

                        //update API Respon
                        response.data = medItemCategory;
                        response.message = $"Medical Item with name={data.Name} has been succsesfully update";
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.data = data;
                        response.message = $"Request Medical Item data cannot be Updated";
                        response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    }

                }
                catch (Exception ex)
                {
                    //undo table changes from database transaction proses
                    dbTran.Rollback();

                    response.data = data;
                    response.message = ex.Message;
                }
            }
            return response;
        }

        public VMResponse Delete(int Id, int userId)
        {
            if (Id != 0 && userId != 0)
            {
                using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        VMMMedicalItemCategory? existingData = (VMMMedicalItemCategory?)GetById(Id).data;
                        if (existingData != null)
                        {
                            MMedicalItemCategory medItemCategory = new MMedicalItemCategory()
                            {
                                Id = existingData.Id,
                                Name = existingData.Name,

                                CreatedBy = existingData.CreatedBy,
                                CreatedOn = existingData.CreatedOn,

                                ModifiedBy = userId,
                                ModifiedOn = DateTime.Now,

                                DeletedBy = userId,
                                DeletedOn = DateTime.Now,

                                IsDelete = true,
                            };
                            //proses membuat data kedalam BD tabel
                            db.Update(medItemCategory);
                            db.SaveChanges();

                            //save table changes from database transaction
                            dbTran.Commit();                //biar masuk ke database

                            //update API Respon
                            response.data = medItemCategory;
                            response.message = $"Medical Item with name={existingData.Name} has benn succsesfully Deleted";
                            response.StatusCode = System.Net.HttpStatusCode.OK;

                        }
                        else
                        {
                            response.message = $"Medical Item with ID{Id} cannot be found";
                            response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        response.message = ex.Message;
                    }
                }
            }
            else
            {
                response.message = "Please input Medical Item ID and User ID first";
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            return response;
        }

    }
}
