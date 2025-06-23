using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;

namespace BATCH336B.DataAccess
{
    public class DACourier
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336BContext db;
        public DACourier(BATCH336BContext _db)
        {
            db = _db;
        }

        public VMResponse GetByFilter() => GetByFilter("");

        public VMResponse GetByFilter(string filter)
        {
            try
            {
                List<VMMCourier> data = (
                    from k in db.MCouriers
                    where k.IsDelete == false
                    && k.Nama.Contains(filter ?? "")
                    select new VMMCourier
                    {
                        Id = k.Id,
                        Nama = k.Nama,
                        IsDelete = k.IsDelete,

                        CreatedBy = k.CreatedBy,
                        CreatedOn = k.CreatedOn,
                        ModifedBy = k.ModifedBy,
                        ModifedOn = k.ModifedOn,
                        DeletedBy = k.DeletedBy,
                        DeletedOn = k.DeletedOn
                    }
                    ).ToList();

                response.data = data;
                response.message = (data.Count > 0)
                        ? $"{data.Count} Courier data Successfuly fetched!"
                        : "Courier has no Data!";
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
                    VMMCourier? data = (
                    from k in db.MCouriers
                    where k.IsDelete == false
                        && k.Id == id
                    select new VMMCourier
                    {
                        Id = k.Id,
                        Nama = k.Nama,
                        IsDelete = k.IsDelete,

                        CreatedBy = k.CreatedBy,
                        CreatedOn = k.CreatedOn,
                        ModifedBy = k.ModifedBy,
                        ModifedOn = k.ModifedOn,
                        DeletedBy = k.DeletedBy,
                        DeletedOn = k.DeletedOn
                    }
                    ).FirstOrDefault();

                    if (data != null)
                    {
                        response.data = data;
                        response.message = "Courier data Successfuly fetched!";
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Courier data could not be found";
                        response.StatusCode = System.Net.HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = "Masukan Id terlebih dahulu";
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

        public VMResponse Seacrh(string name)
        {
            try
            {
                VMMCourier? data = (
                from k in db.MCouriers
                where k.IsDelete == false
                    && k.Nama.Contains(name ?? "")
                select new VMMCourier
                {
                    Id = k.Id,
                    Nama = k.Nama,
                    IsDelete = k.IsDelete,

                    CreatedBy = k.CreatedBy,
                    CreatedOn = k.CreatedOn,
                    ModifedBy = k.ModifedBy,
                    ModifedOn = k.ModifedOn,
                    DeletedBy = k.DeletedBy,
                    DeletedOn = k.DeletedOn
                }
                ).FirstOrDefault();

                if (data != null)
                {
                    response.data = data;
                    response.message = $"Courier data with id = {data.Id} Successfuly fetched!";
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.message = "Courier data could not be found";
                    response.StatusCode = System.Net.HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }


        public VMResponse Create(VMMCourier data)
        {
            if (data.Nama == null)
            {
                response.message = "Anda salah input";
                return response;
            }
            MCourier? exisistingData = findDataByName(data.Nama);
            if (exisistingData != null)
            {
                response.message = "kurir exist";
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }

            //starting db trasaction proses
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    //preapare empty data model untuk new kategory data
                    MCourier Courier = new MCourier();

                    Courier.Nama = data.Nama.Trim();
                    Courier.IsDelete = false;
                    Courier.CreatedBy = data.CreatedBy;
                    Courier.CreatedOn = DateTime.Now;

                    //proses membuat data kedalam BD tabel
                    db.Add(Courier);
                    db.SaveChanges();

                    //save table changes from database transaction
                    dbTran.Commit();

                    //update API Respon
                    response.data = Courier;
                    response.message = "New Courier has ben succsesfully created";
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

        public VMResponse Update(VMMCourier data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMCourier? existingData = (VMMCourier?)GetById(data.Id ?? 0).data;
                    if (existingData != null)
                    {
                        MCourier Courier = new MCourier()
                        {
                            Id = existingData.Id ?? 0,
                            Nama = data.Nama,
                            IsDelete = false,



                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn ?? DateTime.Now,

                            ModifedBy = data.ModifedBy,
                            ModifedOn = DateTime.Now,
                        };
                        //proses membuat data kedalam BD tabel
                        db.Update(Courier);
                        db.SaveChanges();

                        //save table changes from database transaction
                        dbTran.Commit();                //biar masuk ke database

                        //update API Respon
                        response.data = Courier;
                        response.message = $"Courier with name={data.Nama} has benn succsesfully update";
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.data = data;
                        response.message = $"Request Courier data cannot be Updated";
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
                        VMMCourier? existingData = (VMMCourier?)GetById(Id).data;
                        if (existingData != null)
                        {
                            MCourier Courier = new MCourier()
                            {
                                Id = existingData.Id ?? 0,
                                Nama = existingData.Nama,


                                IsDelete = true,


                                CreatedBy = existingData.CreatedBy,
                                CreatedOn = existingData.CreatedOn ?? DateTime.Now,

                                ModifedBy = userId,
                                ModifedOn = DateTime.Now,
                            };
                            //proses membuat data kedalam BD tabel
                            db.Update(Courier);
                            db.SaveChanges();

                            //save table changes from database transaction
                            dbTran.Commit();                //biar masuk ke database

                            //update API Respon
                            response.data = Courier;
                            response.message = $"Courier with name={existingData.Nama} has benn succsesfully Deleted";
                            response.StatusCode = System.Net.HttpStatusCode.OK;

                        }
                        else
                        {
                            response.message = $"Courier with ID{Id} cannot be found";
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
                response.message = "Please input Courier ID adn User ID first";
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            return response;
        }

        public MCourier? findDataByName(string name)
        {
            MCourier? responseData = new MCourier();
            try
            {
                responseData = (
                from k in db.MCouriers
                where k.IsDelete == false &&
                k.Nama.ToLower() == (name.ToLower()).Trim()
                select new MCourier
                {
                    Id = k.Id,
                    Nama = k.Nama,

                    IsDelete = k.IsDelete,

                    CreatedBy = k.CreatedBy,
                    CreatedOn = k.CreatedOn,
                    ModifedBy = k.ModifedBy,
                    ModifedOn = k.ModifedOn,
                    DeletedBy = k.DeletedBy,
                    DeletedOn = k.DeletedOn

                }
                ).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }

            return responseData;
        }

    }
}
