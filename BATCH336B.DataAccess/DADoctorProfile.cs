using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336B.DataAccess
{
    public class DADoctorProfile
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336BContext db;
        public DADoctorProfile(BATCH336BContext _db) { db=_db; }

        public VMResponse GetDoctorById(long? id)
        {
            try
            {
                if (id > 0)
                {
                    VMDoctorProfile? data = (
                        from d in db.MDoctors
                        join b in db.MBiodata
                            on d.BiodataId equals b.Id
                        join cd in db.TCurrentDoctorSpecializations 
                            on d.Id equals cd.DoctorId
                        join s in db.MSpecializations
                            on cd.SpecializationId equals s.Id
                        where d.Id == id && d.IsDelete == false && b.IsDelete == false
                        select new VMDoctorProfile
                        {
                            Id = d.Id,
                            BiodataId = d.BiodataId,
                            Fullname = b.Fullname,
                            MobilePhone = b.MobilePhone,
                            ImagePath = b.ImagePath,

                            SpecializationId = cd.SpecializationId,
                            Name = s.Name,

                            Str = d.Str,
                            CreateBy = d.CreateBy,
                            CreateOn = d.CreateOn,
                            ModifiedBy = d.ModifiedBy,
                            ModifiedOn = d.ModifiedOn,
                            DeletedBy = d.DeletedBy,
                            DeletedOn = d.DeletedOn,
                            IsDelete = false,

                            DoctorOffice=(
                                from df in db.TDoctorOffices
                                join mf in db.MMedicalFacilities
                                  on df.MedicalFacilityId equals mf.Id
                                where df.DoctorId == d.Id && mf.IsDelete == false
                                select new VMDoctorOffice
                                {
                                    Id = df.Id,
                                    DoctorId = df.DoctorId,
                                    MedicalFacilityId = df.MedicalFacilityId,
                                    Name = mf.Name,
                                    Specialization = df.Specialization,
                                    StartDate = df.StartDate,
                                    EndDate = df.EndDate,
                                    LamaBekerja = DateTime.Now > df.StartDate ? (new DateTime(1, 1, 1) + (DateTime.Now - (DateTime)df.StartDate!)).Year - 1 : 0,

                                    DoctorOfficeSchedule =(
                                        from dos in db.TDoctorOfficeSchedules
                                        join mfs in db.MMedicalFacilitySchedules
                                            on dos.MedicalFacilityScheduleId equals mfs.Id
                                        
                                        where dos.DoctorId == df.DoctorId && mfs.MedicalFacilityId == df.MedicalFacilityId  && mfs.IsDelete == false && dos.IsDelete ==false
                                        select new VMDoctorOfficeSchedule
                                        {
                                            Id = dos.Id,
                                            DoctorId=dos.DoctorId,
                                            Slot = dos.Slot,
                                            MedicalFacilityScheduleId = dos.MedicalFacilityScheduleId,
                                            MedicalFacilityId = mfs.MedicalFacilityId,
                                            Day = mfs.Day,
                                            TimeScheduleStart = mfs.TimeScheduleStart,
                                            TimeScheduleEnd = mfs.TimeScheduleEnd,
                                            CreatedBy = dos.CreatedBy,
                                            CreatedOn = dos.CreatedOn,
                                            ModifiedBy = dos.ModifiedBy,
                                            ModifiedOn = dos.ModifiedOn,
                                            DeletedBy = dos.DeletedBy,
                                            DeletedOn = dos.DeletedOn,
                                            IsDelete = false
                                        }
                                    ).ToList(),

                                    DoctorOfficeTreatment = (
                                        from dot in db.TDoctorOfficeTreatments
                                        join dt in db.TDoctorTreatments
                                            on dot.DoctorTreatmentId equals dt.Id
                                        where dot.DoctorOfficeId == df.Id
                                        select new VMDoctorOfficeTreatment
                                        {
                                            Id= dot.Id,
                                            DoctorTreatmentId = dot.DoctorTreatmentId,
                                            Name = dt.Name,
                                            DoctorOfficeId = dot.DoctorOfficeId
                                        }
                                    ).ToList()

                                }
                            ).ToList()
                        }

                        ).FirstOrDefault();

                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Doctor data with id = {id} successfully fetched";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"Doctor data with id = {id} No Content";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }

                    data = null;
                }
                else
                {
                    response.message = "Please input Profile id first";
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
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
                    VMCustomerBuatJanji? data = (
                        from c in db.MCustomers
                        join b in db.MBiodata
                            on c.BiodataId equals b.Id
                        join u in  db.MUsers
                            on b.Id equals u.BiodataId
                        where u.Id == id && c.IsDelete == false && b.IsDelete == false && u.IsDelete == false
                        select new VMCustomerBuatJanji
                        {
                            Id = u.Id,
                            CustomerId = c.Id,
                            BiodataId = b.Id,
                            Fullname = b.Fullname,
                            CustomerMembers = (
                                
                                from cm in db.MCustomerMembers
                                join cr in db.MCustomerRelations
                                    on cm.CustomerRelationId equals cr.Id
                                join cus in db.MCustomers
                                    on cm.CustomerId equals cus.Id
                                join bio in db.MBiodata
                                    on cus.BiodataId equals bio.Id
                                where cm.ParentBiodataId == b.Id && cm.IsDelete == false && cr.IsDelete == false
                                select new VMCustomerMember
                                {
                                    ParentBiodataId = cm.ParentBiodataId,
                                    BiodataId = bio.Id,
                                    CustomerId = cm.CustomerId,
                                    Fullname = bio.Fullname,
                                    CustomerRelationId = cm.CustomerRelationId,
                                    Name = cr.Name
                                }
                            ).ToList()
                        }).FirstOrDefault();
                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Customer data with id = {id} successfully fetched";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"Customer data with id = {id} No Content";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }

                    data = null;
                }
                else
                {
                    response.message = "Please input Profile id first";
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            catch(Exception e)
            {
                response.message = e.Message;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMResponse GetAllAppointmentBySche(long? id, string? tgl)
        {
            try
            {

                List<VMTTAppointmnet>? data = (
                    from a in db.TAppointments
                    join dof in db.TDoctorOffices
                        on a.DoctorOfficeId equals dof.Id
                    join dos in db.TDoctorOfficeSchedules
                        on a.DoctorOfficeScheduleId equals dos.Id
                    join mfs in db.MMedicalFacilitySchedules
                        on dos.MedicalFacilityScheduleId equals mfs.Id
                    where a.DoctorOfficeScheduleId == id && a.AppointmentDate == DateTime.ParseExact(tgl, "yyyy-MM-dd", CultureInfo.InvariantCulture) && a.IsDelete == false
                    select new VMTTAppointmnet
                    {
                        Id = a.Id,
                        DoctorId = dof.DoctorId,
                        CustomerId = a.CustomerId,
                        DoctorOfficeId = a.DoctorOfficeId,
                        DoctorOfficeScheduleId = a.DoctorOfficeScheduleId,
                        DoctorOfficeTreatmentId = a.DoctorOfficeTreatmentId,
                        AppointmentDate = a.AppointmentDate,

                        MedicalFacilityId = mfs.MedicalFacilityId,
                        Day = mfs.Day,
                        TimeScheduleStart = mfs.TimeScheduleStart,
                        TimeScheduleEnd = mfs.TimeScheduleEnd,

                        CreatedBy = a.CreatedBy,
                        CreatedOn = a.CreatedOn,
                        ModifiedBy = a.ModifiedBy,
                        ModifiedOn = a.ModifiedOn,
                        DeletedBy = a.DeletedBy,
                        DeletedOn = a.DeletedOn,
                        IsDelete = a.IsDelete
                    }
                    ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Appointment data successfully fetched" : "Appointment has no data !";
                response.StatusCode = (data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception e)
            {
                response.message = e.Message;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }
        public VMResponse GetAllAppointment(long? id, string? tgl)
        {
            try
            {
                
                List<VMTTAppointmnet>? data = (
                    from a in db.TAppointments
                    join dof in db.TDoctorOffices
                        on a.DoctorOfficeId equals dof.Id
                    join dos in db.TDoctorOfficeSchedules
                        on a.DoctorOfficeScheduleId equals dos.Id
                    join mfs in db.MMedicalFacilitySchedules
                        on dos.MedicalFacilityScheduleId equals mfs.Id
                    where a.CustomerId == id && a.AppointmentDate == DateTime.ParseExact(tgl, "yyyy-MM-dd", CultureInfo.InvariantCulture) && a.IsDelete == false
                    select new VMTTAppointmnet
                    {
                        Id = a.Id,
                        DoctorId = dof.DoctorId,
                        CustomerId = a.CustomerId,
                        DoctorOfficeId = a.DoctorOfficeId,
                        DoctorOfficeScheduleId = a.DoctorOfficeScheduleId,
                        DoctorOfficeTreatmentId = a.DoctorOfficeTreatmentId,
                        AppointmentDate = a.AppointmentDate,

                        MedicalFacilityId = mfs.MedicalFacilityId,
                        Day = mfs.Day,
                        TimeScheduleStart = mfs.TimeScheduleStart,
                        TimeScheduleEnd = mfs.TimeScheduleEnd,

                        CreatedBy = a.CreatedBy,
                        CreatedOn = a.CreatedOn,
                        ModifiedBy = a.ModifiedBy,
                        ModifiedOn = a.ModifiedOn,
                        DeletedBy = a.DeletedBy,
                        DeletedOn = a.DeletedOn,
                        IsDelete = a.IsDelete
                    }
                    ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} Appointment data successfully fetched" : "Appointment has no data !";
                response.StatusCode = (data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch(Exception e)
            {
                response.message = e.Message;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMResponse Create(VMTTAppointmnet data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    TAppointment appointment = new TAppointment();

                    appointment.CustomerId = data.CustomerId;
                    appointment.DoctorOfficeId = data.DoctorOfficeId;
                    appointment.DoctorOfficeScheduleId = data.DoctorOfficeScheduleId;
                    appointment.DoctorOfficeTreatmentId = data.DoctorOfficeTreatmentId;
                    appointment.AppointmentDate = data.AppointmentDate;
                    appointment.CreatedBy = data.CreatedBy;
                    appointment.CreatedOn = DateTime.Now;

                    //Proses Orm (Object Relational Mapping) -> insert DATA to DATABASE
                    db.Add(appointment);
                    db.SaveChanges();

                    //Save Changes from Transaction to Database
                    dbTran.Commit();

                    response.data = appointment;
                    response.message = "New Appointment Data Succesfully Created";
                    response.StatusCode = HttpStatusCode.Created;

                }
                catch (Exception e)
                {
                    dbTran.Rollback();

                    response.data = data;
                    response.message = e.Message;
                }
            }
            return response;
        }
        
    }
}
