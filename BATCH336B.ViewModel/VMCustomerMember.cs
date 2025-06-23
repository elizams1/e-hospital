using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336B.ViewModel
{
    public class VMCustomerMember
    {
        public long? ParentBiodataId { get; set; }
        public long? BiodataId { get; set; }
        public long? CustomerId { get; set; }
        public string? Fullname { get; set; }
        //public DateTime? Dob { get; set; }
        //public string? Gender { get; set; }
        public long? CustomerRelationId { get; set; }
        public string? Name { get; set; }
    }
}
