using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Org.BouncyCastle.Asn1.X509;
using System.Globalization;
using System.Security.Cryptography;

namespace BATCH336B.DataAccess
{
    public class DAProfile
    {
        private VMResponse response = new VMResponse();

        private readonly BATCH336BContext db;

        public DAProfile(BATCH336BContext _db) { db = _db; }

        public VMResponse GetById(long? id)
        {
            try
            {
                if (id > 0)
                {
                    VMMProfile? data = (
                        from u in db.MUsers
                        join b in db.MBiodata
                            on u.BiodataId equals b.Id
                        join c in db.MCustomers
                            on b.Id equals c.BiodataId
                        //Apakah harus di join dengan admin dan doketr?
                        where u.Id == id
                         && u.IsDelete == false
                         && b.IsDelete == false
                         && c.IsDelete == false
                        select new VMMProfile
                        {
                            Id = u.Id,
                            BiodataId = b.Id,

                            Fullname = b.Fullname,
                            MobilePhone = b.MobilePhone,
                            Image = b.Image,
                            ImagePath = b.ImagePath,

                            Dob = c.Dob,
                            Gender = c.Gender,
                            BloodGroupId = c.BloodGroupId,
                            RhesusType = c.RhesusType,
                            Height = c.Height,
                            Weight = c.Weight,

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

                    if(data != null)
                    {
                        response.data = data;
                        response.message = "Profile data successfully fetched";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Profile data No Content";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = "Please input Profile id first";
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

        public VMResponse GetBiodataById(long? id)
        {
            try
            {
                if (id > 0)
                {
                    VMMBiodatum? data = (
                        from b in db.MBiodata
                        where b.Id == id
                         && b.IsDelete == false
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

                    if (data != null)
                    {
                        response.data = data;
                        response.message = "Biodata data successfully fetched";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Biodata data No Content";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = "Please input Biodata id first";
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

        public VMResponse GetCustomerById(long? id)
        {
            try
            {
                if (id > 0)
                {
                    VMMCustomer? data = (
                        from c in db.MCustomers
                        where c.BiodataId == id
                         && c.IsDelete == false
                        select new VMMCustomer
                        {
                            Id = c.Id,
                            BiodataId = c.BiodataId,
                            Dob = c.Dob,
                            Gender = c.Gender,
                            BloodGroupId = c.BloodGroupId,
                            RhesusType = c.RhesusType,
                            Height = c.Height,
                            Weight = c.Weight,

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
                        response.message = "Customer data successfully fetched";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Customer data No Content";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = "Please input Customer id first";
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

        public VMResponse GetUserById(long? id)
        {
            try
            {
                if (id > 0)
                {
                    VMMUser? data = (
                        from u in db.MUsers
                        where u.Id == id
                         && u.IsDelete == false
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
                        response.message = "User data successfully fetched";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "User data No Content";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = "Please input User id first";
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

        public VMResponse UpdateAkun(VMMProfile data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMProfile existData = (VMMProfile)GetById(data.Id).data;
                    VMMBiodatum existingData = (VMMBiodatum)GetBiodataById(existData.BiodataId).data;
                    VMMCustomer existingCustomer = (VMMCustomer)GetCustomerById(existData.BiodataId).data;

                    if (existingData != null)
                    {
                        MBiodatum biodata = new MBiodatum()
                        {
                            Id = (int)existingData.Id!,
                            CreateBy = existingData.CreateBy,
                            CreateOn = (DateTime)existingData.CreateOn!,
                            Image = existingData.Image,
                            ImagePath = existingData.ImagePath,
                            

                            Fullname = data.Fullname,
                            MobilePhone = data.MobilePhone,
                            
                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,

                            IsDelete = false
                        };

                        //Proses Orm (Object Relational Mapping) -> insert DATA to DATABASE
                        db.Update(biodata);
                        db.SaveChanges();

                        ////Save Changes from Transaction to Database
                        //dbTran.Commit();

                        MCustomer customer = new MCustomer()
                        {
                            Id = (int)existingCustomer.Id!,
                            CreateBy = existingCustomer.CreateBy,
                            CreateOn = (DateTime)existingCustomer.CreateOn!,
                            BiodataId = existingCustomer.BiodataId,
                            Gender = existingCustomer.Gender,
                            BloodGroupId = existingCustomer.BloodGroupId,
                            RhesusType = existingCustomer.RhesusType,
                            Height = existingCustomer.Height,
                            Weight = existingCustomer.Weight,
                            

                            Dob = data.Dob,

                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,

                            IsDelete = false
                        };

                        //Proses Orm (Object Relational Mapping) -> insert DATA to DATABASE
                        db.Update(customer);
                        db.SaveChanges();

                        //Save Changes from Transaction to Database
                        dbTran.Commit();

                        response.data = biodata;
                        response.message = $"Akun Data With name = {data.Fullname} Succesfully Updated";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.data = data;
                        response.message = "Requested Akun Data Cannot Be Updated";
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

        public VMResponse UpdateEmail(VMMProfile data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMUser existData = (VMMUser)GetUserById(data.Id).data;

                    if (existData != null)
                    {
                        MUser user = new MUser()
                        {
                            Id = existData.Id,
                            BiodataId = existData.BiodataId,
                            RoleId = existData.RoleId,
                            Password = existData.Password,
                            LoginAttempt = existData.LoginAttempt,
                            IsLocked = existData.IsLocked,
                            LastLogin = existData.LastLogin,

                            CreateBy = existData.CreateBy,
                            CreateOn = (DateTime)existData.CreateOn!,
                            

                            IsDelete = false,

                            Email = data.Email,
                            

                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now
                        };

                        //Proses Orm (Object Relational Mapping) -> insert DATA to DATABASE
                        db.Update(user);
                        db.SaveChanges();


                        //Save Changes from Transaction to Database
                        dbTran.Commit();

                        response.data = user;
                        response.message = $"Email With address = {data.Email} Succesfully Updated";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.data = data;
                        response.message = "Requested Email Data Cannot Be Updated";
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

        public VMResponse UpdatePassword(VMMProfile data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMUser existData = (VMMUser)GetUserById(data.Id).data;

                    if (existData != null)
                    {
                        MUser user = new MUser()
                        {
                            Id = existData.Id,
                            BiodataId = existData.BiodataId,
                            RoleId = existData.RoleId,
                            Email = existData.Email,
                            
                            LoginAttempt = existData.LoginAttempt,
                            IsLocked = existData.IsLocked,
                            LastLogin = existData.LastLogin,

                            CreateBy = existData.CreateBy,
                            CreateOn = (DateTime)existData.CreateOn!,
                            

                            IsDelete = false,

                            Password = data.Password,


                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now
                        };

                        //Proses Orm (Object Relational Mapping) -> insert DATA to DATABASE
                        db.Update(user);
                        db.SaveChanges();

                        TResetPassword password = new TResetPassword()
                        {
                            OldPassword = existData.Password,
                            NewPassword = data.Password,
                            ResetFor = "update",
                            CreateOn = DateTime.Now,
                            CreateBy = data.Id

                        };

                        db.Add(password);
                        db.SaveChanges();
                        
                         
                        //Save Changes from Transaction to Database
                        dbTran.Commit();

                        response.data = user;
                        response.message = $"Password With address = {existData.Email} Succesfully Updated";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.data = data;
                        response.message = "Requested Password Data Cannot Be Updated";
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

        public VMResponse GetByEmailOtp(string? email)
        {
            try
            {
                VMTToken? token = (
                    from t in db.TTokens
                    where t.Email == email && t.IsDelete == false && t.IsExpired ==false
                    orderby t.CreatedOn descending
                    select new VMTToken
                    
                    {
                        Id = t.Id,
                        Email = t.Email,
                        UserId = t.UserId??0,
                        Token = t.Token,
                        ExpiredOn = (DateTime)t.ExpiredOn,
                        IsExpired = t.IsExpired??false,
                        UsedFor = t.UsedFor,
                        CreatedBy = t.CreatedBy,
                        CreatedOn = t.CreatedOn
                    }
                ).FirstOrDefault();

                if (token != null)
                {
                    response.data = token;
                    response.message = "Email is already registered, please register with another email!";
                    response.StatusCode = System.Net.HttpStatusCode.Ambiguous;
                }
                else
                {
                    
                    response.message = "Email not registered, please register using this email!";
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception e)
            {
                
                response.message = e.Message;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMResponse SendOtp(int userId, string email, int otp, string expiredToken)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {

                    VMTToken existToken = (VMTToken)GetByEmailOtp(email).data;
                    if (existToken == null)
                    {
                        TToken token = new TToken();

                        token.Email = email;
                        token.UserId = userId;
                        token.Token = otp.ToString();
                        token.ExpiredOn = DateTime.ParseExact(expiredToken, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                        token.IsExpired = false;
                        token.UsedFor = "update";
                        token.CreatedBy = userId;
                        token.CreatedOn = DateTime.Now;
                        token.IsDelete = false;


                        db.Add(token);
                        db.SaveChanges();
                        
                    }
                    else
                    {
                        //Update Is expired sebelumnya
                        TToken tokenUpdate = new TToken()
                        {
                            Id = existToken.Id,
                            Email = existToken.Email,
                            UserId = existToken.UserId,
                            Token = existToken.Token,
                            ExpiredOn = existToken.ExpiredOn,
                            IsExpired = true,
                            UsedFor = existToken.UsedFor,
                            CreatedBy = existToken.CreatedBy,
                            CreatedOn = existToken.CreatedOn!,                            
                            ModifiedBy = userId,
                            ModifiedOn = DateTime.Now
                        };

                        //Proses Orm (Object Relational Mapping) -> insert DATA to DATABASE
                        db.Update(tokenUpdate);
                        db.SaveChanges();


                        //Add token baru dengan email yang sama
                        TToken tokenAdd = new TToken();

                        tokenAdd.Email = email;
                        tokenAdd.UserId = userId;
                        tokenAdd.Token = otp.ToString();
                        tokenAdd.ExpiredOn = DateTime.ParseExact(expiredToken, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                        tokenAdd.IsExpired = false;
                        tokenAdd.UsedFor = "update";
                        tokenAdd.CreatedBy = userId;
                        tokenAdd.CreatedOn = DateTime.Now;
                        tokenAdd.IsDelete = false;

                        db.Add(tokenAdd);
                        db.SaveChanges();
                    }

                    dbTran.Commit();


                    var messageEmail = new MimeMessage();

                    //Dikirim dari email
                    messageEmail.From.Add(MailboxAddress.Parse("margaretta.emmerich24@ethereal.email"));
                    //Dikirim ke ?
                    messageEmail.To.Add(MailboxAddress.Parse(email));
                    //Isi Email
                    messageEmail.Subject = "Kode OTP UPDATE EMAIL Batch336B Apps";
                    messageEmail.Body = new TextPart(TextFormat.Html)
                    {
                        Text = "" +
                        "<h3>Kode Verifikasi OTP <b>UPDATE EMAIL</b> </h3><br>" +
                        "<p>Kode OTP akan kedaluwarsa dalam 10 Menit </p><br>" +
                        "<h5>OTP Code : </h5><br>" +
                        $"<div><h3><b>{otp}</b></h3></div>"
                    };

                    // Simple Mail Transfer Protocol
                    using var smtp = new SmtpClient();
                    smtp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    smtp.Authenticate("margaretta.emmerich24@ethereal.email", "AJbmuz8t72RegHNpj8");
                    smtp.Send(messageEmail);
                    smtp.Disconnect(true);

                    response.data = existToken;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.message = "Successfully for sending email!";

                }
                catch (Exception e)
                {
                    response.message = e.Message;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
            }
            
            return response;
        }

        public VMResponse UpdateToken(string email, int userId)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMTToken existToken = (VMTToken)GetByEmailOtp(email).data;
                    if (existToken != null)
                    {
                        //Update Is expired sebelumnya
                        TToken tokenUpdate = new TToken()
                        {
                            Id = existToken.Id,
                            Email = existToken.Email,
                            UserId = existToken.UserId,
                            Token = existToken.Token,
                            ExpiredOn = existToken.ExpiredOn,
                            IsExpired = true,
                            UsedFor = existToken.UsedFor,
                            CreatedBy = existToken.CreatedBy,
                            CreatedOn = existToken.CreatedOn!,
                            
                            ModifiedBy = userId,
                            ModifiedOn = DateTime.Now
                        };

                        //Proses Orm (Object Relational Mapping) -> insert DATA to DATABASE
                        db.Update(tokenUpdate);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.data = tokenUpdate;
                        response.message = $"Token Data With otp = {tokenUpdate.Token} Succesfully Updated";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                }
                catch (Exception e)
                {
                    dbTran.Rollback();

                    response.data = null;
                    response.message = e.Message;
                }
                return response;
            }
        }

        public VMResponse UpdatePhotoProfile(VMMProfile data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMProfile existData = (VMMProfile)GetById(data.Id).data;
                    VMMBiodatum existingData = (VMMBiodatum)GetBiodataById(existData.BiodataId).data;

                    if (existingData != null)
                    {
                        MBiodatum biodata = new MBiodatum()
                        {
                            Id = (int)existingData.Id!,
                            CreateBy = existingData.CreateBy,
                            CreateOn = (DateTime)existingData.CreateOn!,
                            Image = data.Image,
                            ImagePath = data.ImagePath,

                            Fullname = existingData.Fullname,
                            MobilePhone = existingData.MobilePhone,

                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,

                            IsDelete = false
                        };
                        //Proses Orm (Object Relational Mapping) -> insert DATA to DATABASE
                        db.Update(biodata);
                        db.SaveChanges();
                        //Save Changes from Transaction to Database
                        dbTran.Commit();

                        response.data = biodata;
                        response.message = $"Akun Data With name = {data.Fullname} Succesfully Updated";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.data = data;
                        response.message = "Requested Akun Data Cannot Be Updated";
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
    }
}
