using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BATCH336B.DataModel
{
    public partial class BATCH336BContext : DbContext
    {
        public BATCH336BContext()
        {
        }

        public BATCH336BContext(DbContextOptions<BATCH336BContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MAdmin> MAdmins { get; set; } = null!;
        public virtual DbSet<MBiodataAddress> MBiodataAddresses { get; set; } = null!;
        public virtual DbSet<MBiodatum> MBiodata { get; set; } = null!;
        public virtual DbSet<MBloodGroup> MBloodGroups { get; set; } = null!;
        public virtual DbSet<MCourier> MCouriers { get; set; } = null!;
        public virtual DbSet<MCustomer> MCustomers { get; set; } = null!;
        public virtual DbSet<MCustomerMember> MCustomerMembers { get; set; } = null!;
        public virtual DbSet<MCustomerRelation> MCustomerRelations { get; set; } = null!;
        public virtual DbSet<MDoctor> MDoctors { get; set; } = null!;
        public virtual DbSet<MDoctorEducation> MDoctorEducations { get; set; } = null!;
        public virtual DbSet<MEducationLevel> MEducationLevels { get; set; } = null!;
        public virtual DbSet<MLocation> MLocations { get; set; } = null!;
        public virtual DbSet<MLocationLevel> MLocationLevels { get; set; } = null!;
        public virtual DbSet<MMedicalFacility> MMedicalFacilities { get; set; } = null!;
        public virtual DbSet<MMedicalFacilityCategory> MMedicalFacilityCategories { get; set; } = null!;
        public virtual DbSet<MMedicalFacilitySchedule> MMedicalFacilitySchedules { get; set; } = null!;
        public virtual DbSet<MMedicalItem> MMedicalItems { get; set; } = null!;
        public virtual DbSet<MMedicalItemCategory> MMedicalItemCategories { get; set; } = null!;
        public virtual DbSet<MMedicalItemSegmentation> MMedicalItemSegmentations { get; set; } = null!;
        public virtual DbSet<MMenu> MMenus { get; set; } = null!;
        public virtual DbSet<MMenuRole> MMenuRoles { get; set; } = null!;
        public virtual DbSet<MRole> MRoles { get; set; } = null!;
        public virtual DbSet<MSpecialization> MSpecializations { get; set; } = null!;
        public virtual DbSet<MUser> MUsers { get; set; } = null!;
        public virtual DbSet<TAppointment> TAppointments { get; set; } = null!;
        public virtual DbSet<TAppointmentDone> TAppointmentDones { get; set; } = null!;
        public virtual DbSet<TCurrentDoctorSpecialization> TCurrentDoctorSpecializations { get; set; } = null!;
        public virtual DbSet<TDoctorOffice> TDoctorOffices { get; set; } = null!;
        public virtual DbSet<TDoctorOfficeSchedule> TDoctorOfficeSchedules { get; set; } = null!;
        public virtual DbSet<TDoctorOfficeTreatment> TDoctorOfficeTreatments { get; set; } = null!;
        public virtual DbSet<TDoctorTreatment> TDoctorTreatments { get; set; } = null!;
        public virtual DbSet<TPrescription> TPrescriptions { get; set; } = null!;
        public virtual DbSet<TResetPassword> TResetPasswords { get; set; } = null!;
        public virtual DbSet<TToken> TTokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=BATCH336B;user id=sa;Password=P@ssw0rd; connection timeout=3600;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MCustomerMember>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MCustomerRelation>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MDoctorEducation>(entity =>
            {
                entity.Property(e => e.IsLastEducation).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MMedicalFacilitySchedule>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TDoctorOffice>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TDoctorOfficeSchedule>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TToken>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
