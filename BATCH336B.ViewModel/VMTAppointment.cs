﻿

namespace BATCH336B.ViewModel
{
    public class VMTAppointment
    {
        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public long? DoctorOfficeId { get; set; }
        public long? DoctorOfficeScheduleId { get; set; }
        public long? DoctorOfficeTreatmentId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
    }
}
