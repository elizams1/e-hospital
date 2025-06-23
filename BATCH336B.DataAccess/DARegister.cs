using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;

namespace BATCH336B.DataAccess
{
    public class DARegister
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336BContext db;
        public DARegister(BATCH336BContext _db) { db = _db; }
        public VMResponse Create(VMMRegister data)
        {
            using(IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    if (data != null) 
                    {
                        MBiodatum biodata = new MBiodatum();
                        MUser user = new MUser();
                        MAdmin? admin = new MAdmin();
                        MDoctor? doctor = new MDoctor();
                        MCustomer? customer = new MCustomer();
                        MMedicalFacility? facility = new MMedicalFacility();

                        biodata.Fullname = data.Biodatum.Fullname;
                        biodata.MobilePhone = data.Biodatum.MobilePhone;
                        biodata.CreateBy = data.Biodatum.CreateBy;
                        biodata.CreateOn = DateTime.Now;
                        biodata.IsDelete = false;
                        db.Add(biodata);
                        db.SaveChanges();

                        user.BiodataId = biodata.Id;
                        user.Email = data.User.Email;
                        user.Password = data.User.Password;
                        user.RoleId = data.User.RoleId;
                        user.CreateBy = biodata.CreateBy;
                        user.CreateOn = (DateTime)biodata.CreateOn;
                        user.IsDelete = biodata.IsDelete;
                        db.Add(user);
                        db.SaveChanges();

                        if (user.BiodataId == biodata.Id && user.RoleId == 1)
                        {
                            admin.BiodataId = biodata.Id;
                            admin.CreateBy = biodata.CreateBy;
                            admin.CreateOn = (DateTime)biodata.CreateOn!;
                            admin.IsDelete = biodata.IsDelete;
                            admin.Code = "ADM-"+biodata.Id;

                            db.Add(admin);
                            db.SaveChanges();
                        }

                        else if(user.BiodataId == biodata.Id && user.RoleId == 2)
                        {
                            doctor.BiodataId = biodata.Id;
                            doctor.CreateBy = biodata.CreateBy;
                            //doctor.CreateOn = biodata.CreateOn;
                            doctor.IsDelete = biodata.IsDelete;

                            db.Add(doctor);
                            db.SaveChanges();
                        }
                        else if(user.BiodataId == biodata.Id && user.RoleId == 3)
                        {
                            facility.Name = biodata.Fullname;
                            facility.Email = user.Email;
                            facility.Phone = biodata.MobilePhone;
                            facility.CreatedBy = biodata.CreateBy;
                            facility.CreatedOn = (DateTime)biodata.CreateOn;
                            facility.IsDelete = biodata.IsDelete;

                            db.Add(facility);
                            db.SaveChanges();
                        }
                        else if (user.BiodataId == biodata.Id && user.RoleId == 4)
                        {
                            customer.BiodataId = biodata.Id;
                            customer.CreateBy = biodata.CreateBy;
                            customer.CreateOn = (DateTime)biodata.CreateOn;
                            customer.IsDelete = biodata.IsDelete;

                            db.Add(customer);
                            db.SaveChanges();
                        }

                    }
                    else
                    {
                        throw new ArgumentNullException("Fail To Regist Account because Biodata or User Null!");
                    }
                    dbTrans.Commit();
                    response.StatusCode = System.Net.HttpStatusCode.Created;
                    response.data = data;
                    response.message = "Successfully for Create Account, Please Login!";
                }
                catch(Exception ex)
                {
                    dbTrans.Rollback();
                    response.message = ex.Message;
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            return response;
        }


        public VMResponse GetByEmail(string? email)
        {
            try
            {
                VMMUser? data = (
                    from u in db.MUsers
                    where u.Email == email && u.IsDelete == false
                    select new VMMUser
                    {
                        Id = u.Id,
                        BiodataId = u.BiodataId,
                        Email = u.Email,
                        Password = u.Password,
                        RoleId = u.RoleId
                    }
                ).FirstOrDefault();
                if (data != null)
                {
                    response.data = data;
                    response.message = "Email is already registered, please register with another email!";
                    response.StatusCode = System.Net.HttpStatusCode.Ambiguous;
                }
                else
                {
                    response.message = "Email not registered, please register using this email!";
                    response.StatusCode = System.Net.HttpStatusCode.OK;
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
