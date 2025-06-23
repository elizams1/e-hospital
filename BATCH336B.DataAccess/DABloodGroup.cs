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
    public class DABloodGroup
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;

        public DABloodGroup(BATCH336BContext _db) { db = _db; }

        public VMResponse Get() => Get("");


        public VMResponse Get(string? filter)
        {
            try
            {
                List<VMMBloodGroup> data = (
                    from b in db.MBloodGroups
                    join u in db.MUsers
                        on b.ModifiedBy equals u.Id into uJoin
                    from u in uJoin.DefaultIfEmpty()
                    join bio in db.MBiodata
                        on u.BiodataId equals bio.Id into bioJoin
                    from bio in bioJoin.DefaultIfEmpty()
                    where b.IsDelete == false
                        && (b.Code.Contains(filter ?? "") || b.Description.Contains(filter ?? ""))

                    select new VMMBloodGroup
                    {
                        Id = b.Id,
                        Code = b.Code,
                        Description = b.Description,

                        CreateBy = b.CreateBy,
                        CreateOn = (DateTime)b.CreateOn,
                        ModifiedBy = b.ModifiedBy,
                        ModifiedOn = b.ModifiedOn,
                        ModifiedName = bio != null ? bio.Fullname : null,
                        DeletedBy = b.DeletedBy,
                        DeletedOn = b.DeletedOn,
                        IsDelete = b.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Blood Group data successfully fetched" : "Blood Group has no data !";
                response.StatusCode = (data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception e)
            {
                response.message = e.Message;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }


        public VMResponse GetById(long? id)
        {
            try
            {
                if (id > 0)
                {
                    VMMBloodGroup? data = (
                         from b in db.MBloodGroups
                         join u in db.MUsers
                             on b.ModifiedBy equals u.Id into uJoin
                         from u in uJoin.DefaultIfEmpty()
                         join bio in db.MBiodata
                             on u.BiodataId equals bio.Id into bioJoin
                         from bio in bioJoin.DefaultIfEmpty()
                         where b.IsDelete == false
                          && b.Id == id

                         select new VMMBloodGroup
                         {
                             Id = b.Id,
                             Code = b.Code,
                             Description = b.Description,

                             CreateBy = b.CreateBy,
                             CreateOn = (DateTime)b.CreateOn,
                             ModifiedBy = b.ModifiedBy,
                             ModifiedOn = b.ModifiedOn,
                             ModifiedName = bio != null ? bio.Fullname : null,
                             DeletedBy = b.DeletedBy,
                             DeletedOn = b.DeletedOn,
                             IsDelete = b.IsDelete
                         }
                       ).FirstOrDefault();

                    if (data != null)
                    {
                        response.data = data;
                        response.message = "Blood Group data successfully fetched";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Blood Group data No Content";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = "Please input bloodGroup id first";
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            catch (Exception e)
            {
                response.message = e.Message;
                response.StatusCode = HttpStatusCode.NotFound;

            }
            return response;
        }

        public VMResponse Create(VMMBloodGroup data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    MBloodGroup bloodGroup = new MBloodGroup();

                    bloodGroup.Code = data.Code;
                    bloodGroup.Description = data.Description;
                    bloodGroup.IsDelete = false;
                    bloodGroup.CreateBy = data.CreateBy;
                    bloodGroup.CreateOn = DateTime.Now;

                    //Proses Orm (Object Relational Mapping) -> insert DATA to DATABASE
                    db.Add(bloodGroup);
                    db.SaveChanges();

                    //Save Changes from Transaction to Database
                    dbTran.Commit();

                    response.data = bloodGroup;
                    response.message = "New Blood Group Data Succesfully Created";
                    response.StatusCode = HttpStatusCode.Created;
                }
                catch (Exception e)
                {

                    //Undo Changes from Transaction
                    dbTran.Rollback();

                    response.data = data;
                    response.message = e.Message;
                    //karena defaultnya sudah internal server error maka tidak udah diberikan assign untuk StatusCode
                }
            }

            return response;
        }

        public VMResponse Update(VMMBloodGroup data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMBloodGroup existingData = (VMMBloodGroup)GetById(data.Id).data;

                    if (existingData != null)
                    {
                        MBloodGroup bloodGroup = new MBloodGroup()
                        {
                            Id = (int)existingData.Id!,
                            CreateBy = existingData.CreateBy,
                            CreateOn = (DateTime)existingData.CreateOn!,


                            Code = data.Code,
                            Description = data.Description,
                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,
                            IsDelete = false
                        };

                        //Proses Orm (Object Relational Mapping) -> insert DATA to DATABASE
                        db.Update(bloodGroup);
                        db.SaveChanges();

                        //Save Changes from Transaction to Database
                        dbTran.Commit();

                        response.data = bloodGroup;
                        response.message = $"Blood Group Data With ID = {data.Id} Succesfully Updated";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.data = data;
                        response.message = "Requested Blood Group Data Cannot Be Updated";
                        response.StatusCode = HttpStatusCode.NotFound;
                    }

                }
                catch (Exception e)
                {
                    dbTran.Rollback();

                    response.data = data;
                    response.message = e.Message;
                }

                return response;
            }
        }

        public VMResponse Delete(int id, int userId)
        {
            if (id != 0 && userId != 0)
            {
                using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        VMMBloodGroup existingData = (VMMBloodGroup)GetById(id).data;
                        if (existingData != null)
                        {
                            MBloodGroup bloodGroup = new MBloodGroup()
                            {
                                Id = (int)existingData.Id!,
                                CreateBy = existingData.CreateBy,
                                CreateOn = (DateTime)existingData.CreateOn!,
                                ModifiedBy = existingData.ModifiedBy,
                                ModifiedOn = (DateTime)existingData.ModifiedOn,
                                Code = existingData.Code,
                                Description = existingData.Description,

                                DeletedBy = userId,
                                DeletedOn = DateTime.Now,

                                IsDelete = true
                            };
                            //Proses Orm (Object Relational Mapping) -> insert DATA to DATABASE
                            db.Update(bloodGroup);
                            db.SaveChanges();

                            //Save Changes from Transaction to Database
                            dbTran.Commit();

                            response.data = bloodGroup;
                            response.message = $"Blood Group Data With ID = {existingData.Id} Succesfully Remove / Deleted";
                            response.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            response.message = $"Blood Group data with ID {id} not found";
                            response.StatusCode = HttpStatusCode.NotFound;
                        }
                    }
                    catch (Exception e)
                    {
                        dbTran.Rollback();

                        response.message = e.Message;
                    }
                }
            }
            else
            {
                response.message = "Please input bloodGroup id and user id first";
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        public VMResponse GetByCode(string? code)
        {
            try
            {
                VMMBloodGroup? data = (
                    from b in db.MBloodGroups
                    where b.Code == code && b.IsDelete == false
                    select new VMMBloodGroup
                    {
                        Id = b.Id,
                        Code = b.Code,
                        Description = b.Description
                    }
                ).FirstOrDefault();
                if (data != null)
                {
                    response.data = data;
                    response.message = "BloodGroup is already exist, please type another bloodgroup with another code!";
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.message = "BloodGroup does not exist!";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;

            }

            return response;
        }
    }
}
