﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BATCH336B.DataModel
{
    [Table("t_reset_password")]
    public partial class TResetPassword
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("old_password")]
        [StringLength(255)]
        [Unicode(false)]
        public string? OldPassword { get; set; }
        [Column("new_password")]
        [StringLength(255)]
        [Unicode(false)]
        public string? NewPassword { get; set; }
        [Column("reset_for")]
        [StringLength(20)]
        [Unicode(false)]
        public string? ResetFor { get; set; }
        [Column("create_by")]
        public long CreateBy { get; set; }
        [Column("modified_by")]
        public long? ModifiedBy { get; set; }
        [Column("modified_on", TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("deleted_by")]
        public long? DeletedBy { get; set; }
        [Column("deleted_on", TypeName = "datetime")]
        public DateTime? DeletedOn { get; set; }
        [Column("is_delete")]
        public bool IsDelete { get; set; }
        [Column("create_on", TypeName = "datetime")]
        public DateTime? CreateOn { get; set; }
    }
}
