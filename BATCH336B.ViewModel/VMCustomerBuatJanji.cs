using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336B.ViewModel
{
    public class VMCustomerBuatJanji
    {
        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public long? BiodataId { get; set; }
        public string? Fullname { get; set; }

        public List<VMCustomerMember>? CustomerMembers { get; set; }

    }
}
