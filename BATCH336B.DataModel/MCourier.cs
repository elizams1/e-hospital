using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BATCH336B.DataModel
{
    [Table("m_courier")]
    public partial class MCourier
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Nama { get; set; }
        [Column("created_by")]
        public long CreatedBy { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("modifed_by")]
        public long? ModifedBy { get; set; }
        [Column("modifed_on", TypeName = "datetime")]
        public DateTime? ModifedOn { get; set; }
        [Column("deleted_by")]
        public long? DeletedBy { get; set; }
        [Column("deleted_on", TypeName = "datetime")]
        public DateTime? DeletedOn { get; set; }
        [Column("is_delete")]
        public bool IsDelete { get; set; }
    }
}
