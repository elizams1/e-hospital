//using BATCH336B.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336B.ViewModel
{
    public class VMMHistoryCustomer
    {
        public VMMBiodatum? Biodatum { get; set; }
        public VMMBiodatum? BiodatumDoctor { get; set; }
        public VMMCustomer? Customer { get; set; }
        public VMMCustomerMember? CustomerMember { get; set; }
        public VMMCustomerRelation? CustomerRelation { get; set; }
        public VMMDoctor? Doctor { get; set; }
        public VMTCurrentDoctorSpecialization? CurrentDoctorSpecialization { get; set; }
        public VMMSpecialization? Specialization { get; set; }
        public VMTDoctorOffice? DoctorOffice { get; set; }
        public VMMMedicalFacility? MedicalFacility { get; set; }
        public VMMMedicalFacilityCategory? MedicalFacilityCategory { get; set; }
        public VMTAppointment? Appointment { get; set; }
        public VMTAppointmentDone? AppointmentDone { get; set; }
        public VMTDoctorOfficeTreatment? DoctorOfficeTreatment { get; set; }
        public VMTDoctorTreatment? DoctorTreatment { get; set; }
        public List<VMTPrescription>? Prescription { get; set; }
        public List<VMMMedicalItem>? MedicalItem { get; set; }
        public VMMMedicalItemSegmentation? MedicalItemSegmentation { get; set; }
        public VMMMedicalItemCategory? MedicalItemCategory { get; set; }
    }
}
