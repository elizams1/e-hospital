﻿
namespace BATCH336B.ViewModel
{
    public class VMTAppointmentDone
    {
        public long Id { get; set; }
        public long? AppointmentId { get; set; }
        public string Diagnosis { get; set; } = null!;
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
    }
}
