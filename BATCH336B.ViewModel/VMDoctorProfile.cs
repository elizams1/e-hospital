using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336B.ViewModel
{
    public class VMDoctorProfile
    {
        public long Id { get; set; }

        public long? BiodataId { get; set; }
        public string? Fullname { get; set; }
        public string? MobilePhone { get; set; }
        public string? ImagePath { get; set; }

        public long? SpecializationId { get; set; }
        public string? Name { get; set; }

        public string? Str { get; set; }

        public long CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public List<VMDoctorOffice>? DoctorOffice { get; set; }
    }
}
