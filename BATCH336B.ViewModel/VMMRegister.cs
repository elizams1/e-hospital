//using BATCH336B.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336B.ViewModel
{
    public class VMMRegister
    {
        public VMMBiodatum Biodatum { get; set; }
        public VMMUser User { get; set; }
        public VMMAdmin? Admin { get; set; }
        public VMMCustomer? Customer { get; set; }
        public VMMDoctor? Doctor { get; set; }
        public VMMMedicalFacility? MedicalFacility { get; set; }
        //public VMTToken? Token { get; set; }
    }
}
