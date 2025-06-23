using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336B.ViewModel
{
    public class VMDoctorOfficeTreatment
    {
        public long Id { get; set; }
        public long? DoctorTreatmentId { get; set; }
        public string? Name { get; set; }
        public long? DoctorOfficeId { get; set; }
    }
}
