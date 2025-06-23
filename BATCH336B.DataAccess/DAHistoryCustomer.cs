
using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;

namespace BATCH336B.DataAccess
{
    public class DAHistoryCustomer
    {
        private VMResponse response = new VMResponse();
        private readonly BATCH336BContext db;
        public DAHistoryCustomer(BATCH336BContext _db) { db = _db; }

        public VMResponse GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    VMMHistoryCustomer? data = (
                    from a in db.TAppointments
                    join c in db.MCustomers on a.CustomerId equals c.Id
                    join b in db.MBiodata on c.BiodataId equals b.Id
                    join cm in db.MCustomerMembers on c.Id equals cm.CustomerId
                    join cr in db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                    join doo in db.TDoctorOffices on a.DoctorOfficeId equals doo.Id
                    join mf in db.MMedicalFacilities on doo.MedicalFacilityId equals mf.Id
                    join mfc in db.MMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                    join ad in db.TAppointmentDones on a.Id equals ad.AppointmentId
                    join dot in db.TDoctorOfficeTreatments on a.DoctorOfficeTreatmentId equals dot.Id
                    join dt in db.TDoctorTreatments on dot.DoctorTreatmentId equals dt.Id
                    join dk in db.MDoctors on dt.DoctorId equals dk.Id
                    join cds in db.TCurrentDoctorSpecializations on dk.Id equals cds.DoctorId
                    join s in db.MSpecializations on cds.SpecializationId equals s.Id
                    join bd in db.MBiodata on dk.BiodataId equals bd.Id
                    /*join p in db.TPrescriptions on a.Id equals p.AppointmentId
                    //join mi in db.MMedicalItems on p.MedicalItemId equals mi.Id
                    //join mis in db.MMedicalItemSegmentations on mi.MedicalItemSegmentationId equals mis.Id
                    //join mic in db.MMedicalItemCategories on mi.MedicalItemCategoryId equals mic.Id*/
                    where (ad.AppointmentId == a.Id || a.AppointmentDate < DateTime.Now) && a.Id == id
                    select new VMMHistoryCustomer
                    {
                        Biodatum = new VMMBiodatum
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
                        },
                        BiodatumDoctor = new VMMBiodatum
                        {
                            Id = bd.Id,
                            Fullname = bd.Fullname,
                            MobilePhone = bd.MobilePhone,
                            Image = bd.Image,
                            ImagePath = bd.ImagePath,
                            CreateBy = bd.CreateBy,
                            CreateOn = (DateTime)bd.CreateOn!,
                            ModifiedBy = bd.ModifiedBy,
                            ModifiedOn = bd.ModifiedOn,
                            DeletedBy = bd.DeletedBy,
                            DeletedOn = bd.DeletedOn,
                            IsDelete = bd.IsDelete
                        },
                        Customer = new VMMCustomer
                        {
                            Id = c.Id,
                            BiodataId = c.BiodataId,
                            Dob = c.Dob,
                            Gender = c.Gender,
                            CreateBy = c.CreateBy,
                            CreateOn = (DateTime)c.CreateOn!,
                            ModifiedBy = c.ModifiedBy,
                            ModifiedOn = c.ModifiedOn,
                            DeletedBy = c.DeletedBy,
                            DeletedOn = c.DeletedOn,
                            IsDelete = c.IsDelete
                        },
                        CustomerMember = new VMMCustomerMember
                        {
                            Id = cm.Id,
                            ParentBiodataId = cm.ParentBiodataId,
                            CustomerId = cm.CustomerId,
                            CustomerRelationId = cm.CustomerRelationId,
                            CreatedBy = cm.CreatedBy,
                            CreatedOn = cm.CreatedOn,
                            ModifiedBy = cm.ModifiedBy,
                            ModifiedOn = cm.ModifiedOn,
                            DeletedBy = cm.DeletedBy,
                            DeletedOn = cm.DeletedOn,
                            IsDelete = cm.IsDelete
                        },
                        CustomerRelation = new VMMCustomerRelation
                        {
                            Id = cr.Id,
                            Name = cr.Name,
                            CreatedBy = cr.CreatedBy,
                            CreatedOn = cr.CreatedOn,
                            ModifiedBy = cr.ModifiedBy,
                            ModifiedOn = cr.ModifiedOn,
                            DeletedBy = cr.DeletedBy,
                            DeletedOn = cr.DeletedOn,
                            IsDelete = cr.IsDelete
                        },
                        Doctor = new VMMDoctor
                        {
                            Id = dk.Id,
                            BiodataId = dk.BiodataId,
                            Str = dk.Str,
                            CreateBy = dk.CreateBy,
                            CreateOn = (DateTime)dk.CreateOn!,
                            ModifiedBy = dk.ModifiedBy,
                            ModifiedOn = dk.ModifiedOn,
                            DeletedBy = dk.DeletedBy,
                            DeletedOn = dk.DeletedOn,
                            IsDelete = dk.IsDelete
                        },
                        CurrentDoctorSpecialization = new VMTCurrentDoctorSpecialization
                        {
                            Id = cds.Id,
                            DoctorId = cds.DoctorId,
                            SpecializationId = cds.SpecializationId,
                            CreatedBy = cds.CreatedBy,
                            CreatedOn = cds.CreatedOn,
                            ModifiedBy = cds.ModifiedBy,
                            ModifiedOn = cds.ModifiedOn,
                            DeletedBy = cds.DeletedBy,
                            DeletedOn = cds.DeletedOn,
                            IsDelete = cds.IsDelete
                        },
                        Specialization = new VMMSpecialization
                        {
                            Id = s.Id,
                            Name = s.Name,
                            CreatedBy = s.CreatedBy,
                            CreatedOn = s.CreatedOn,
                            ModifiedBy = s.ModifiedBy,
                            ModifiedOn = s.ModifiedOn,
                            DeletedBy = s.DeletedBy,
                            DeletedOn = s.DeletedOn,
                            IsDelete = s.IsDelete
                        },
                        DoctorOffice = new VMTDoctorOffice
                        {
                            Id = doo.Id,
                            DoctorId = doo.DoctorId,
                            MedicalFacilityId = doo.MedicalFacilityId,
                            Specialization = doo.Specialization,
                            StartDate = doo.StartDate,
                            EndDate = (DateTime)doo.EndDate,
                            CreatedBy = doo.CreatedBy,
                            CreatedOn = doo.CreatedOn,
                            ModifiedBy = doo.ModifiedBy,
                            ModifiedOn = doo.ModifiedOn,
                            DeletedBy = doo.DeletedBy,
                            DeletedOn = doo.DeletedOn,
                            IsDelete = doo.IsDelete
                        },
                        MedicalFacility = new VMMMedicalFacility
                        {
                            Id = mf.Id,
                            Name = mf.Name,
                            MedicalFacilityCategoryId = mf.MedicalFacilityCategoryId,
                            LocationId = mf.LocationId,
                            FullAddress = mf.FullAddress,
                            Email = mf.Email,
                            PhoneCode = mf.PhoneCode,
                            Phone = mf.Phone,
                            Fax = mf.Fax,
                            CreatedBy = mf.CreatedBy,
                            CreatedOn = mf.CreatedOn,
                            ModifiedBy = mf.ModifiedBy,
                            ModifiedOn = mf.ModifiedOn,
                            DeletedBy = mf.DeletedBy,
                            DeletedOn = mf.DeletedOn,
                            IsDelete = mf.IsDelete
                        },
                        MedicalFacilityCategory = new VMMMedicalFacilityCategory
                        {
                            Id = mfc.Id,
                            Name = mfc.Name,
                            CreatedBy = mfc.CreatedBy,
                            CreatedOn = mfc.CreatedOn,
                            ModifiedBy = mfc.ModifiedBy,
                            ModifiedOn = mfc.ModifiedOn,
                            DeletedBy = mfc.DeletedBy,
                            DeletedOn = mfc.DeletedOn,
                            IsDelete = mfc.IsDelete
                        },
                        Appointment = new VMTAppointment
                        {
                            Id = a.Id,
                            CustomerId = a.CustomerId,
                            DoctorOfficeId = a.DoctorOfficeId,
                            DoctorOfficeScheduleId = a.DoctorOfficeScheduleId,
                            DoctorOfficeTreatmentId = a.DoctorOfficeTreatmentId,
                            AppointmentDate = a.AppointmentDate,
                            CreatedBy = a.CreatedBy,
                            CreatedOn = a.CreatedOn,
                            ModifiedBy = a.ModifiedBy,
                            ModifiedOn = a.ModifiedOn,
                            DeletedBy = a.DeletedBy,
                            DeletedOn = a.DeletedOn,
                            IsDelete = a.IsDelete
                        },
                        AppointmentDone = new VMTAppointmentDone
                        {
                            Id = ad.Id,
                            AppointmentId = ad.AppointmentId,
                            Diagnosis = ad.Diagnosis,
                            CreatedBy = ad.CreatedBy,
                            CreatedOn = ad.CreatedOn,
                            ModifiedBy = ad.ModifiedBy,
                            ModifiedOn = ad.ModifiedOn,
                            DeletedBy = ad.DeletedBy,
                            DeletedOn = ad.DeletedOn,
                            IsDelete = ad.IsDelete
                        },
                        DoctorOfficeTreatment = new VMTDoctorOfficeTreatment
                        {
                            Id = dot.Id,
                            DoctorTreatmentId = dot.DoctorTreatmentId,
                            DoctorOfficeId = dot.DoctorOfficeId,
                            CreatedBy = dot.CreatedBy,
                            CreatedOn = dot.CreatedOn,
                            ModifiedBy = dot.ModifiedBy,
                            ModifiedOn = dot.ModifiedOn,
                            DeletedBy = dot.DeletedBy,
                            DeletedOn = dot.DeletedOn,
                            IsDelete = dot.IsDelete
                        },
                        DoctorTreatment = new VMTDoctorTreatment
                        {
                            Id = dt.Id,
                            DoctorId = dt.DoctorId,
                            Name = dt.Name,
                            CreatedBy = dt.CreatedBy,
                            CreatedOn = dt.CreatedOn,
                            ModifiedBy = dt.ModifiedBy,
                            ModifiedOn = dt.ModifiedOn,
                            DeletedBy = dt.DeletedBy,
                            DeletedOn = dt.DeletedOn,
                            IsDelete = dt.IsDelete
                        },
                        /*MedicalItem = new VMMMedicalItem
                        {
                            Id = mi.Id,
                            Name = mi.Name,
                            MedicalItemCategoryId = mi.MedicalItemCategoryId,
                            MedicalItemCategoryName = mic.Name,
                            Composition = mi.Composition,

                            MedicalItemSegmentationId = mi.MedicalItemSegmentationId,
                            MedicalItemSegmentationName = mis.Name,
                            Manufacturer = mi.Manufacturer,
                            Indication = mi.Indication,
                            Dosage = mi.Dosage,
                            Directions = mi.Directions,
                            Contraindication = mi.Contraindication,
                            Caution = mi.Caution,
                            Packaging = mi.Packaging,
                            PriceMax = mi.PriceMax,
                            PriceMin = mi.PriceMin,
                            Image = mi.Image,
                            ImagePath = mi.ImagePath,


                            CreatedBy = mi.CreatedBy,
                            CreatedOn = mi.CreatedOn,
                            ModifiedBy = mi.ModifiedBy,
                            ModifiedOn = mi.ModifiedOn,
                            DeletedBy = mi.DeletedBy,
                            DeletedOn = mi.DeletedOn,
                            IsDelete = mi.IsDelete
                        },
                        MedicalItemSegmentation = new VMMMedicalItemSegmentation
                        {
                            Id = mis.Id,
                            Name = mis.Name,

                            CreatedBy = mis.CreatedBy,
                            CreatedOn = mis.CreatedOn,
                            ModifiedBy = mis.ModifiedBy,
                            ModifiedOn = mis.ModifiedOn,
                            DeletedBy = mis.DeletedBy,
                            DeletedOn = mis.DeletedOn,
                            IsDelete = mis.IsDelete
                        },
                        MedicalItemCategory = new VMMMedicalItemCategory
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
                        },*/
                        Prescription = (
                            from p in db.TPrescriptions
                            join mi in db.MMedicalItems on p.MedicalItemId equals mi.Id
                            where p.IsDelete == false && p.AppointmentId == a.Id
                            select new VMTPrescription
                            {
                                Id = p.Id,
                                AppointmentId = p.AppointmentId,
                                MedicalItemId = p.MedicalItemId,
                                MedicalItemName = mi.Name,
                                Dosage = p.Dosage,
                                Directions = p.Directions,
                                Time = p.Time,
                                Notes = p.Notes,
                                CreatedBy = p.CreatedBy,
                                CreatedOn = p.CreatedOn,
                                ModifiedBy = p.ModifiedBy,
                                ModifiedOn = p.ModifiedOn,
                                DeletedBy = p.DeletedBy,
                                DeletedOn = p.DeletedOn,
                                IsDelete = p.IsDelete,
                                PrintAttempt = p.PrintAttempt
                            }
                        ).ToList(),
                        }
                    ).FirstOrDefault();
                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"History Customer data Successfully fetched!";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"id {id} History Customer has no Data!";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input History Data ID First!";
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
        public VMResponse GetByIdPres(int id)
        {
            try
            {
                if (id > 0)
                {
                    VMTPrescription? data = (
                    from p in db.TPrescriptions
                    join mi in db.MMedicalItems on p.MedicalItemId equals mi.Id
                    where p.IsDelete == false && p.Id == id
                    select new VMTPrescription
                    {
                        Id = p.Id,
                        AppointmentId = p.AppointmentId,
                        MedicalItemId = p.MedicalItemId,
                        MedicalItemName = mi.Name,
                        Dosage = p.Dosage,
                        Directions = p.Directions,
                        Time = p.Time,
                        Notes = p.Notes,
                        CreatedBy = p.CreatedBy,
                        CreatedOn = p.CreatedOn,
                        ModifiedBy = p.ModifiedBy,
                        ModifiedOn = p.ModifiedOn,
                        DeletedBy = p.DeletedBy,
                        DeletedOn = p.DeletedOn,
                        IsDelete = p.IsDelete,
                        PrintAttempt = p.PrintAttempt
                    }
                    ).FirstOrDefault();
                    if (data != null)
                    {
                        response.data = data;
                        response.message = $"Prescription data Successfully fetched!";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = $"id {id} Prescription has no Data!";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.message = $"Please Input Prescription Data ID First!";
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
        public VMResponse GetAll() => GetByFilter("");
        public VMResponse GetByFilter(string filter)
        {
            try
            {
                /*VMMBiodatum? biodata = new VMMBiodatum();
                VMMCustomer? customers = new VMMCustomer();
                VMMCustomerMember? customerMembers = new VMMCustomerMember();
                VMMCustomerRelation? customerRelations = new VMMCustomerRelation();
                VMMDoctor? doctors = new VMMDoctor();
                VMTCurrentDoctorSpecialization? currentDoctorSpecializations = new VMTCurrentDoctorSpecialization();
                VMMSpecialization? specializations = new VMMSpecialization();
                VMTDoctorOffice? doctorOffices = new VMTDoctorOffice();
                VMMMedicalFacility? medicalFacilities = new VMMMedicalFacility();
                VMMMedicalFacilityCategory? medicalFacilityCategories = new VMMMedicalFacilityCategory();
                VMTAppointment? appointments = new VMTAppointment();
                VMTAppointmentDone? appointmentDones = new VMTAppointmentDone();
                VMTDoctorOfficeTreatment? doctorOfficeTreatments = new VMTDoctorOfficeTreatment();
                VMTDoctorTreatment? doctorTreatments = new VMTDoctorTreatment();
                VMTPrescription? prescriptions = new VMTPrescription();
                VMMMedicalItem? medicalItems = new VMMMedicalItem();
                VMMMedicalItemSegmentation? medicalItemSegmentations = new VMMMedicalItemSegmentation();
                VMMMedicalItemCategory? medicalItemCategories = new VMMMedicalItemCategory();*/

                List<VMMHistoryCustomer>data = (
                    from a in db.TAppointments
                    join c in db.MCustomers on a.CustomerId equals c.Id
                    join b in db.MBiodata on c.BiodataId equals b.Id
                    join cm in db.MCustomerMembers on c.Id equals cm.CustomerId
                    join cr in db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                    join doo in db.TDoctorOffices on a.DoctorOfficeId equals doo.Id
                    join mf in db.MMedicalFacilities on doo.MedicalFacilityId equals mf.Id
                    join mfc in db.MMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                    join ad in db.TAppointmentDones on a.Id equals ad.AppointmentId
                    join dot in db.TDoctorOfficeTreatments on a.DoctorOfficeTreatmentId equals dot.Id
                    join dt in db.TDoctorTreatments on dot.DoctorTreatmentId equals dt.Id
                    join dk in db.MDoctors on dt.DoctorId equals dk.Id
                    join cds in db.TCurrentDoctorSpecializations on dk.Id equals cds.DoctorId
                    join s in db.MSpecializations on cds.SpecializationId equals s.Id
                    join bd in db.MBiodata on dk.BiodataId equals bd.Id
                    /*join p in db.TPrescriptions on a.Id equals p.AppointmentId
                    //join mi in db.MMedicalItems on p.MedicalItemId equals mi.Id
                    //join mis in db.MMedicalItemSegmentations on mi.MedicalItemSegmentationId equals mis.Id
                    //join mic in db.MMedicalItemCategories on mi.MedicalItemCategoryId equals mic.Id*/
                    where (ad.AppointmentId == a.Id || a.AppointmentDate < DateTime.Now) && (b.Fullname.Contains(filter ?? "") || bd.Fullname.Contains(filter ?? ""))
                    select new VMMHistoryCustomer
                    {
                        Biodatum = new VMMBiodatum
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
                        },
                        BiodatumDoctor = new VMMBiodatum
                        {
                            Id = bd.Id,
                            Fullname = bd.Fullname,
                            MobilePhone = bd.MobilePhone,
                            Image = bd.Image,
                            ImagePath = bd.ImagePath,
                            CreateBy = bd.CreateBy,
                            CreateOn = (DateTime)bd.CreateOn!,
                            ModifiedBy = bd.ModifiedBy,
                            ModifiedOn = bd.ModifiedOn,
                            DeletedBy = bd.DeletedBy,
                            DeletedOn = bd.DeletedOn,
                            IsDelete = bd.IsDelete
                        },
                        Customer = new VMMCustomer
                        {
                            Id = c.Id,
                            BiodataId = c.BiodataId,
                            Dob = c.Dob,
                            Gender = c.Gender,
                            CreateBy = c.CreateBy,
                            CreateOn = (DateTime)c.CreateOn!,
                            ModifiedBy = c.ModifiedBy,
                            ModifiedOn = c.ModifiedOn,
                            DeletedBy = c.DeletedBy,
                            DeletedOn = c.DeletedOn,
                            IsDelete = c.IsDelete
                        },
                        CustomerMember = new VMMCustomerMember
                        {
                            Id = cm.Id,
                            ParentBiodataId = cm.ParentBiodataId,
                            CustomerId = cm.CustomerId,
                            CustomerRelationId = cm.CustomerRelationId,
                            CreatedBy = cm.CreatedBy,
                            CreatedOn = cm.CreatedOn,
                            ModifiedBy = cm.ModifiedBy,
                            ModifiedOn = cm.ModifiedOn,
                            DeletedBy = cm.DeletedBy,
                            DeletedOn = cm.DeletedOn,
                            IsDelete = cm.IsDelete
                        },
                        CustomerRelation = new VMMCustomerRelation
                        {
                            Id = cr.Id,
                            Name = cr.Name,
                            CreatedBy = cr.CreatedBy,
                            CreatedOn = cr.CreatedOn,
                            ModifiedBy = cr.ModifiedBy,
                            ModifiedOn = cr.ModifiedOn,
                            DeletedBy = cr.DeletedBy,
                            DeletedOn = cr.DeletedOn,
                            IsDelete = cr.IsDelete
                        },
                        Doctor = new VMMDoctor
                        {
                            Id = dk.Id,
                            BiodataId = dk.BiodataId,
                            Str = dk.Str,
                            CreateBy = dk.CreateBy,
                            CreateOn = (DateTime)dk.CreateOn!,
                            ModifiedBy = dk.ModifiedBy,
                            ModifiedOn = dk.ModifiedOn,
                            DeletedBy = dk.DeletedBy,
                            DeletedOn = dk.DeletedOn,
                            IsDelete = dk.IsDelete
                        },
                        CurrentDoctorSpecialization = new VMTCurrentDoctorSpecialization
                        {
                            Id = cds.Id,
                            DoctorId = cds.DoctorId,
                            SpecializationId = cds.SpecializationId,
                            CreatedBy = cds.CreatedBy,
                            CreatedOn = cds.CreatedOn,
                            ModifiedBy = cds.ModifiedBy,
                            ModifiedOn = cds.ModifiedOn,
                            DeletedBy = cds.DeletedBy,
                            DeletedOn = cds.DeletedOn,
                            IsDelete = cds.IsDelete
                        },
                        Specialization = new VMMSpecialization
                        {
                            Id = s.Id,
                            Name = s.Name,
                            CreatedBy = s.CreatedBy,
                            CreatedOn = s.CreatedOn,
                            ModifiedBy = s.ModifiedBy,
                            ModifiedOn = s.ModifiedOn,
                            DeletedBy = s.DeletedBy,
                            DeletedOn = s.DeletedOn,
                            IsDelete = s.IsDelete
                        },
                        DoctorOffice = new VMTDoctorOffice
                        {
                            Id = doo.Id,
                            DoctorId = doo.DoctorId,
                            MedicalFacilityId = doo.MedicalFacilityId,
                            Specialization = doo.Specialization,
                            StartDate = doo.StartDate,
                            EndDate = (DateTime)doo.EndDate,
                            CreatedBy = doo.CreatedBy,
                            CreatedOn = doo.CreatedOn,
                            ModifiedBy = doo.ModifiedBy,
                            ModifiedOn = doo.ModifiedOn,
                            DeletedBy = doo.DeletedBy,
                            DeletedOn = doo.DeletedOn,
                            IsDelete = doo.IsDelete
                        },
                        MedicalFacility = new VMMMedicalFacility
                        {
                            Id = mf.Id,
                            Name = mf.Name,
                            MedicalFacilityCategoryId = mf.MedicalFacilityCategoryId,
                            LocationId = mf.LocationId,
                            FullAddress = mf.FullAddress,
                            Email = mf.Email,
                            PhoneCode = mf.PhoneCode,
                            Phone = mf.Phone,
                            Fax = mf.Fax,
                            CreatedBy = mf.CreatedBy,
                            CreatedOn = mf.CreatedOn,
                            ModifiedBy = mf.ModifiedBy,
                            ModifiedOn = mf.ModifiedOn,
                            DeletedBy = mf.DeletedBy,
                            DeletedOn = mf.DeletedOn,
                            IsDelete = mf.IsDelete
                        },
                        MedicalFacilityCategory = new VMMMedicalFacilityCategory
                        {
                            Id = mfc.Id,
                            Name = mfc.Name,
                            CreatedBy = mfc.CreatedBy,
                            CreatedOn = mfc.CreatedOn,
                            ModifiedBy = mfc.ModifiedBy,
                            ModifiedOn = mfc.ModifiedOn,
                            DeletedBy = mfc.DeletedBy,
                            DeletedOn = mfc.DeletedOn,
                            IsDelete = mfc.IsDelete
                        },
                        Appointment = new VMTAppointment
                        {
                            Id = a.Id,
                            CustomerId = a.CustomerId,
                            DoctorOfficeId = a.DoctorOfficeId,
                            DoctorOfficeScheduleId = a.DoctorOfficeScheduleId,
                            DoctorOfficeTreatmentId = a.DoctorOfficeTreatmentId,
                            AppointmentDate = a.AppointmentDate,
                            CreatedBy = a.CreatedBy,
                            CreatedOn = a.CreatedOn,
                            ModifiedBy = a.ModifiedBy,
                            ModifiedOn = a.ModifiedOn,
                            DeletedBy = a.DeletedBy,
                            DeletedOn = a.DeletedOn,
                            IsDelete = a.IsDelete
                        },
                        AppointmentDone = new VMTAppointmentDone
                        {
                            Id = ad.Id,
                            AppointmentId = ad.AppointmentId,
                            Diagnosis = ad.Diagnosis,
                            CreatedBy = ad.CreatedBy,
                            CreatedOn = ad.CreatedOn,
                            ModifiedBy = ad.ModifiedBy,
                            ModifiedOn = ad.ModifiedOn,
                            DeletedBy = ad.DeletedBy,
                            DeletedOn = ad.DeletedOn,
                            IsDelete = ad.IsDelete
                        },
                        DoctorOfficeTreatment = new VMTDoctorOfficeTreatment
                        {
                            Id = dot.Id,
                            DoctorTreatmentId = dot.DoctorTreatmentId,
                            DoctorOfficeId = dot.DoctorOfficeId,
                            CreatedBy = dot.CreatedBy,
                            CreatedOn = dot.CreatedOn,
                            ModifiedBy = dot.ModifiedBy,
                            ModifiedOn = dot.ModifiedOn,
                            DeletedBy = dot.DeletedBy,
                            DeletedOn = dot.DeletedOn,
                            IsDelete = dot.IsDelete
                        },
                        DoctorTreatment = new VMTDoctorTreatment
                        {
                            Id = dt.Id,
                            DoctorId = dt.DoctorId,
                            Name = dt.Name,
                            CreatedBy = dt.CreatedBy,
                            CreatedOn = dt.CreatedOn,
                            ModifiedBy = dt.ModifiedBy,
                            ModifiedOn = dt.ModifiedOn,
                            DeletedBy = dt.DeletedBy,
                            DeletedOn = dt.DeletedOn,
                            IsDelete = dt.IsDelete
                        },
                        /*MedicalItem = new VMMMedicalItem
                        {
                            Id = mi.Id,
                            Name = mi.Name,
                            MedicalItemCategoryId = mi.MedicalItemCategoryId,
                            MedicalItemCategoryName = mic.Name,
                            Composition = mi.Composition,

                            MedicalItemSegmentationId = mi.MedicalItemSegmentationId,
                            MedicalItemSegmentationName = mis.Name,
                            Manufacturer = mi.Manufacturer,
                            Indication = mi.Indication,
                            Dosage = mi.Dosage,
                            Directions = mi.Directions,
                            Contraindication = mi.Contraindication,
                            Caution = mi.Caution,
                            Packaging = mi.Packaging,
                            PriceMax = mi.PriceMax,
                            PriceMin = mi.PriceMin,
                            Image = mi.Image,
                            ImagePath = mi.ImagePath,


                            CreatedBy = mi.CreatedBy,
                            CreatedOn = mi.CreatedOn,
                            ModifiedBy = mi.ModifiedBy,
                            ModifiedOn = mi.ModifiedOn,
                            DeletedBy = mi.DeletedBy,
                            DeletedOn = mi.DeletedOn,
                            IsDelete = mi.IsDelete
                        },
                        MedicalItemSegmentation = new VMMMedicalItemSegmentation
                        {
                            Id = mis.Id,
                            Name = mis.Name,

                            CreatedBy = mis.CreatedBy,
                            CreatedOn = mis.CreatedOn,
                            ModifiedBy = mis.ModifiedBy,
                            ModifiedOn = mis.ModifiedOn,
                            DeletedBy = mis.DeletedBy,
                            DeletedOn = mis.DeletedOn,
                            IsDelete = mis.IsDelete
                        },
                        MedicalItemCategory = new VMMMedicalItemCategory
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
                        },*/
                        Prescription = (
                            from p in db.TPrescriptions
                            join mi in db.MMedicalItems on p.MedicalItemId equals mi.Id
                            where p.IsDelete == false && p.AppointmentId == a.Id
                            select new VMTPrescription
                            {
                                Id = p.Id,
                                AppointmentId = p.AppointmentId,
                                MedicalItemId = p.MedicalItemId,
                                MedicalItemName = mi.Name,
                                Dosage = p.Dosage,
                                Directions = p.Directions,
                                Time = p.Time,
                                Notes = p.Notes,
                                CreatedBy = p.CreatedBy,
                                CreatedOn = p.CreatedOn,
                                ModifiedBy = p.ModifiedBy,
                                ModifiedOn = p.ModifiedOn,
                                DeletedBy = p.DeletedBy,
                                DeletedOn = p.DeletedOn,
                                IsDelete = p.IsDelete,
                                PrintAttempt = p.PrintAttempt
                            }
                        ).ToList(),
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0) ? $"{data.Count} History Customer data Successfully fetched!" : "History Customer has no Data!";
                response.StatusCode = (data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }

        public VMResponse Update(VMTPrescription data)
        {
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {

                    VMTPrescription? existingData = (VMTPrescription?)GetByIdPres((int)data.Id).data;

                    if (existingData != null)
                    {
                        TPrescription prescription = new TPrescription();

                        prescription.Id = existingData.Id;
                        prescription.AppointmentId = existingData.AppointmentId;
                        prescription.MedicalItemId = existingData.MedicalItemId;
                        prescription.Dosage = existingData.Dosage;
                        prescription.Directions = existingData.Directions;
                        prescription.Time = existingData.Time;
                        prescription.Notes = existingData.Notes;
                        prescription.CreatedOn = existingData.CreatedOn;
                        prescription.CreatedBy = existingData.CreatedBy;
                        prescription.ModifiedBy = data.ModifiedBy;
                        prescription.ModifiedOn = DateTime.Now;
                        prescription.DeletedBy = existingData.DeletedBy; 
                        prescription.DeletedOn = existingData.DeletedOn;
                        prescription.IsDelete = existingData.IsDelete;
                        prescription.PrintAttempt = data.PrintAttempt;

                        db.Update(prescription);
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
                    response.message = "data History has been Failed Updated! : " + ex.Message;
                }
            }

            return response;
        }

        //public VMResponse GetAllDoctor()
        //{
        //    try
        //    {
        //        List<VMMHistoryCustomer>data = (
        //            from d in db.MDoctors
        //            join cds in db.TCurrentDoctorSpecializations on d.Id equals cds.DoctorId
        //            join s in db.MSpecializations on cds.SpecializationId equals s.Id
        //            select new VMMHistoryCustomer
        //            {
        //                Doctor = new VMMDoctor
        //                {
        //                    Id = d.Id,
        //                    BiodataId = d.BiodataId,
        //                    Str = d.Str,
        //                    CreateBy = d.CreateBy,
        //                    CreateOn = (DateTime)d.CreateOn!,
        //                    ModifiedBy = d.ModifiedBy,
        //                    ModifiedOn = d.ModifiedOn,
        //                    DeletedBy = d.DeletedBy,
        //                    DeletedOn = d.DeletedOn,
        //                    IsDelete = d.IsDelete
        //                },
        //                CurrentDoctorSpecialization = new VMTCurrentDoctorSpecialization
        //                {
        //                    Id = cds.Id,
        //                    DoctorId = cds.DoctorId,
        //                    SpecializationId = cds.SpecializationId,
        //                    CreatedBy = cds.CreatedBy,
        //                    CreatedOn = cds.CreatedOn,
        //                    ModifiedBy = cds.ModifiedBy,
        //                    ModifiedOn = cds.ModifiedOn,
        //                    DeletedBy = cds.DeletedBy,
        //                    DeletedOn = cds.DeletedOn,
        //                    IsDelete = cds.IsDelete
        //                },
        //                Specialization = new VMMSpecialization
        //                {
        //                    Id = s.Id,
        //                    Name = s.Name,
        //                    CreatedBy = s.CreatedBy,
        //                    CreatedOn = s.CreatedOn,
        //                    ModifiedBy = s.ModifiedBy,
        //                    ModifiedOn = s.ModifiedOn,
        //                    DeletedBy = s.DeletedBy,
        //                    DeletedOn = s.DeletedOn,
        //                    IsDelete = s.IsDelete
        //                },
        //            }
        //        ).ToList();

        //        response.data = data;
        //        response.message = (data.Count > 0) ? $"{data.Count} History Customer Doctor data Successfully fetched!" : "History Customer Doctor has no Data!";
        //        response.StatusCode = (data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
        //    }
        //    catch(Exception ex)
        //    {
        //        response.message = ex.Message;
        //        response.StatusCode = System.Net.HttpStatusCode.NotFound;
        //    }

        //    return response;
        //}
    }
}
