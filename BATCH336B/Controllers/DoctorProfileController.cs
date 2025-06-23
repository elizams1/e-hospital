using BATCH336B.Models;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BATCH336B.Controllers
{
    public class DoctorProfileController : Controller
    {
        private readonly DoctorProfileModel? doctorProfile;
        private VMResponse? response = new VMResponse();
        private readonly string imageFolder;


        public DoctorProfileController(IConfiguration _config)
        {
            doctorProfile = new DoctorProfileModel(_config);
            imageFolder = _config["ImagesFolder"];
        }


        
        public IActionResult Index(long id)
        {
            ViewBag.User = HttpContext.Session.GetInt32("custId");
            return View();
        }

        public (double x, double y) GetExperience(long id)
        {
            VMDoctorProfile data = doctorProfile.GetDoctorById(id);

            List<DateTime> start = new List<DateTime>();
            List<DateTime> end = new List<DateTime>();

            List<DateTime> dateStartNoOverlap = new List<DateTime>();
            List<DateTime> dateEndNoOverlap = new List<DateTime>();

            DateTime dateStartOverlap = DateTime.Now;
            DateTime dateEndOverlap = DateTime.Now;
            foreach (VMDoctorOffice items in data.DoctorOffice)
            {

                start.Add(items.StartDate);
                if (items.EndDate >= DateTime.Now || items.EndDate == null)
                {
                    end.Add(DateTime.Now);
                }
                else
                {
                    end.Add((DateTime)items.EndDate);
                }

            }
            

            for (int i = 0; i < start.Count; i++)
            {
                if (i + 1 < start.Count)
                {
                    if (start[i] <= end[i + 1] && start[i + 1] <= end[i])
                    {
                        dateStartOverlap = start[i];
                        dateEndOverlap = end[i + 1];
                    }
                    else if (end[i] <= start[i + 1] || start[i] >= end[i + 1])
                    {
                        dateStartNoOverlap.Add(start[i]);
                        dateEndNoOverlap.Add(end[i]);
                    }
                }
                else
                {
                    if (start[i] <= dateEndOverlap && dateStartOverlap <= end[i])
                    {
                        if (dateStartOverlap <= start[i])
                        {
                            dateEndOverlap = end[i];
                        }
                        else
                        {
                            dateStartOverlap = start[i];
                            dateEndOverlap = end[i];
                        }
                        
                        
                    }
                    else
                    {
                        dateStartNoOverlap.Add(start[i]);
                        dateEndNoOverlap.Add(end[i]);
                    }
                }
            }

            var totalDNoOverlap = 0.0;
            for (int j = 0; j < dateStartNoOverlap.Count; j++)
            {
                if(dateStartNoOverlap[j]<= dateEndOverlap && dateStartOverlap <= dateEndNoOverlap[j]) {
                    if(dateStartNoOverlap[j] < dateStartOverlap)
                    {
                        totalDNoOverlap = totalDNoOverlap + (dateEndOverlap - dateStartNoOverlap[j]).TotalDays;
                    }
                    else
                    {
                        totalDNoOverlap = totalDNoOverlap + (dateEndOverlap - dateStartOverlap).TotalDays;
                    }
                    
                }
                else
                {
                    totalDNoOverlap = totalDNoOverlap + (dateEndNoOverlap[j] - dateStartNoOverlap[j]).TotalDays;
                }
            }


            //var totalDOverlap = (dateEndOverlap - dateStartOverlap).TotalDays;
            var theTotal = totalDNoOverlap;
            var tY = Math.Truncate(theTotal / 365);
            var tM = Math.Truncate((theTotal % 365) / 30.4368499);

            return (tY, tM);
        }

        public IActionResult BuatJanji(long id, long? faskesId)
        {
            ViewBag.ImgFolder = imageFolder;
            VMDoctorProfile data = doctorProfile.GetDoctorById(id);
            if (faskesId != null)
            {
                data.DoctorOffice = data.DoctorOffice.Where(a => a.MedicalFacilityId == faskesId).ToList();
            }
            ViewBag.Title = "Buat Janji Dokter";
            ViewBag.Customer = doctorProfile.GetCustomerById((int)HttpContext.Session.GetInt32("custId"));

            (double tY, double tM) = GetExperience(id);

            ViewBag.Year = tY;
            ViewBag.Month = tM;

            return View(data);
        }

        public List<VMDoctorOfficeSchedule>? GetTimeById(long id,int catId, DateTime tgl)
        {
            VMDoctorProfile? data = doctorProfile.GetDoctorById(id);
            VMDoctorOffice? datab = data.DoctorOffice.Where(a => a.Id == catId).FirstOrDefault();

            //List< VMTTAppointmnet> app = doctorProfile.GetAllAppointment(scId, tgl);
            string dateNow = tgl.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm");
            //List<VMTTAppointmnet>? a = doctorProfile.GetAllAppointment(1, dateNow);
            //int count = a.Count();

            List<VMDoctorOfficeSchedule>? datac = new List<VMDoctorOfficeSchedule>();
            if (tgl != DateTime.Now.Date) {
                    datac = datab.DoctorOfficeSchedule.Where(dc =>
                dc.Slot > (doctorProfile.GetAllAppointment(dc.Id, dateNow) == null ? 0 : doctorProfile.GetAllAppointment(dc.Id, dateNow).Count())
                ).ToList();
            }
            else
            {
                    datac = datab.DoctorOfficeSchedule.Where(dc =>
                dc.Slot > (doctorProfile.GetAllAppointment(dc.Id, dateNow) == null ? 0 : doctorProfile.GetAllAppointment(dc.Id, dateNow).Count())
                &&
                DateTime.ParseExact(dc.TimeScheduleEnd, "HH:mm", null) > DateTime.ParseExact(time, "HH:mm", null)
                ).ToList();
            }
            return datac;
        }

        public List<VMDoctorOfficeTreatment>? GetTreatmentById(long id, int catId)
        {
            VMDoctorProfile data = doctorProfile.GetDoctorById(id);
            VMDoctorOffice datab = data.DoctorOffice.Where(a => a.Id == catId).FirstOrDefault();
            List<VMDoctorOfficeTreatment> datac = datab.DoctorOfficeTreatment;


            return datac;
        }

        [HttpPost]
        public async Task<VMResponse> Create(VMTTAppointmnet data)
        {
            VMDoctorProfile? dataA = doctorProfile.GetDoctorById(data.DoctorId??0);
            VMDoctorOffice? dataB = dataA.DoctorOffice.Where(a => a.Id == data.DoctorOfficeId).FirstOrDefault();

            
            string dateNow = data.AppointmentDate?.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm");
            
            //int count = a.Count();

            VMDoctorOfficeSchedule? dataC = dataB.DoctorOfficeSchedule.Where(c => c.Id == data.DoctorOfficeScheduleId).FirstOrDefault();

            

            List<VMTTAppointmnet>? app = doctorProfile.GetAllAppointment(data.CustomerId, dateNow);

            List<VMTTAppointmnet>? appSche = doctorProfile.GetAllAppointmentBySche(data.DoctorOfficeScheduleId, dateNow);


            if (app != null)
            {
                VMTTAppointmnet? custAppOne = app.Where(b => b.DoctorOfficeScheduleId == data.DoctorOfficeScheduleId).FirstOrDefault();
                VMTTAppointmnet? custAppTwo = app.Where(b => b.TimeScheduleStart == dataC.TimeScheduleStart).FirstOrDefault();
                if (custAppOne != null || custAppTwo != null)
                {
                    response.StatusCode = System.Net.HttpStatusCode.Ambiguous;
                    return response;
                }
            }
            
            if(dataC.Slot>(appSche == null ? 0 : appSche.Count()))
            {
                response = await doctorProfile.CreateAsync(data);
            }
            else
            {
                response.message = "Jadwal Penuh!";
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }

            //VMTTAppointmnet app =  doctorProfile.GetAllAppointment(dc.Id, dateNow)


            return response;
        }

        public IActionResult Login()
        {
            return RedirectToAction("login","auth");

        }

    }
}
