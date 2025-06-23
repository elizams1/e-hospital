using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336B.ViewModel
{
    public class VMDoctorOffice
    {
        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public long? MedicalFacilityId { get; set; }
        public string? Name { get; set; }
        public string Specialization { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int LamaBekerja { get; set; }
        public List<VMDoctorOfficeSchedule>? DoctorOfficeSchedule { get; set; }
        public List<VMDoctorOfficeTreatment>? DoctorOfficeTreatment { get; set; }

    }
}
