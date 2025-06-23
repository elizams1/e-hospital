using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATCH336B.ViewModel
{
    public class VMMCourier
    {
        public long? Id { get; set; }

        public string? Nama { get; set; }

        public long CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? ModifedBy { get; set; }

        public DateTime? ModifedOn { get; set; }

        public long? DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDelete { get; set; }
    }
}
