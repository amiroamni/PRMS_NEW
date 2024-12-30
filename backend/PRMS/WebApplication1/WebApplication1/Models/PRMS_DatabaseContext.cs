using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PRMS_BackendAPI.Models
{
    public partial class PRMS_DatabaseContext : DbContext
    {
        public PRMS_DatabaseContext()
        {
        }

        public PRMS_DatabaseContext(DbContextOptions<PRMS_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<CenteralCompany> CenteralCompanies { get; set; } = null!;
        public virtual DbSet<ChatMessage> ChatMessages { get; set; } = null!;
        public virtual DbSet<Clinic> Clinics { get; set; } = null!;
        public virtual DbSet<ClinicStaff> ClinicStaffs { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<Hospital> Hospitals { get; set; } = null!;
        public virtual DbSet<HospitalStaff> HospitalStaffs { get; set; } = null!;
        public virtual DbSet<MedicalRecord> MedicalRecords { get; set; } = null!;
        public virtual DbSet<Pateient> Pateients { get; set; } = null!;
        public virtual DbSet<Referral> Referrals { get; set; } = null!;
        public virtual DbSet<Specialization> Specializations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserChat> UserChats { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-UGVSS12;Database=PRMS_Database;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.AppointmentId).HasColumnName("Appointment_Id");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Appointment_Date");

                entity.Property(e => e.AppointmentDescription)
                    .HasMaxLength(150)
                    .HasColumnName("Appointment_Description");

                entity.Property(e => e.AppointmentName)
                    .HasMaxLength(50)
                    .HasColumnName("Appointment_Name");

                entity.Property(e => e.DoctorId).HasColumnName("Doctor_Id");

                entity.Property(e => e.HospitalId).HasColumnName("Hospital_Id");

                entity.Property(e => e.PatientId).HasColumnName("Patient_Id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Appointment_Doctor");

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.HospitalId)
                    .HasConstraintName("FK_Appointment_Hospital");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Appointment_Pateient");
            });

            modelBuilder.Entity<CenteralCompany>(entity =>
            {
                entity.HasKey(e => e.SystemAdminId);

                entity.ToTable("CenteralCompany");

                entity.Property(e => e.SystemAdminId).HasColumnName("SystemAdmin_Id");

                entity.Property(e => e.ClinicId).HasColumnName("Clinic_Id");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.HospitalId).HasColumnName("Hospital_Id");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.CenteralCompanies)
                    .HasForeignKey(d => d.ClinicId)
                    .HasConstraintName("FK_CenteralCompany_Clinic");

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.CenteralCompanies)
                    .HasForeignKey(d => d.HospitalId)
                    .HasConstraintName("FK_CenteralCompany_Hospital");
            });

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.Property(e => e.ReceiverId).HasMaxLength(450);

                entity.Property(e => e.SenderId).HasMaxLength(450);

                entity.Property(e => e.SentAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.ChatMessageReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChatMessages_Users_ReceiverId");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.ChatMessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChatMessages_Users_SenderId");
            });

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.ToTable("Clinic");

                entity.Property(e => e.ClinicId).HasColumnName("Clinic_Id");

                entity.Property(e => e.ClinicEmail)
                    .HasMaxLength(50)
                    .HasColumnName("Clinic_Email");

                entity.Property(e => e.ClinicLocation)
                    .HasMaxLength(100)
                    .HasColumnName("Clinic_Location");

                entity.Property(e => e.ClinicName)
                    .HasMaxLength(50)
                    .HasColumnName("Clinic_Name");

                entity.Property(e => e.ClinicPhone)
                    .HasMaxLength(20)
                    .HasColumnName("Clinic_phone");

                entity.Property(e => e.ClinicType)
                    .HasMaxLength(50)
                    .HasColumnName("Clinic_Type");
            });

            modelBuilder.Entity<ClinicStaff>(entity =>
            {
                entity.ToTable("ClinicStaff");

                entity.Property(e => e.ClinicStaffId).HasColumnName("ClinicStaff_Id");

                entity.Property(e => e.ClinicId).HasColumnName("Clinic_id");

                entity.Property(e => e.ClinicPhone)
                    .HasMaxLength(20)
                    .HasColumnName("Clinic_phone");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.ClinicStaffs)
                    .HasForeignKey(d => d.ClinicId)
                    .HasConstraintName("FK_ClinicStaff_Clinic");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.DoctorId).HasColumnName("Doctor_Id");

                entity.Property(e => e.ClinicId).HasColumnName("Clinic_Id");

                entity.Property(e => e.DoctorEmail)
                    .HasMaxLength(50)
                    .HasColumnName("Doctor_Email");

                entity.Property(e => e.DoctorFirstName)
                    .HasMaxLength(50)
                    .HasColumnName("Doctor_FirstName");

                entity.Property(e => e.DoctorLastName)
                    .HasMaxLength(50)
                    .HasColumnName("Doctor_LastName");

                entity.Property(e => e.DoctorMiddleName)
                    .HasMaxLength(50)
                    .HasColumnName("Doctor_MiddleName");

                entity.Property(e => e.DoctorPhone)
                    .HasMaxLength(20)
                    .HasColumnName("Doctor_Phone");

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.HospitalId).HasColumnName("Hospital_Id");

                entity.Property(e => e.SpecializationId)
                    .HasMaxLength(50)
                    .HasColumnName("specialization_Id");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.ClinicId)
                    .HasConstraintName("FK_Doctor_Clinic");

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.HospitalId)
                    .HasConstraintName("FK_Doctor_Hospital");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.SpecializationId)
                    .HasConstraintName("FK_Doctor_Specializations");
            });

            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.ToTable("Hospital");

                entity.Property(e => e.HospitalId).HasColumnName("Hospital_Id");

                entity.Property(e => e.HospitalEmail)
                    .HasMaxLength(50)
                    .HasColumnName("Hospital_Email");

                entity.Property(e => e.HospitalLocation)
                    .HasMaxLength(100)
                    .HasColumnName("Hospital_Location");

                entity.Property(e => e.HospitalName)
                    .HasMaxLength(50)
                    .HasColumnName("Hospital_Name");

                entity.Property(e => e.HospitalPhone)
                    .HasMaxLength(20)
                    .HasColumnName("Hospital_phone");

                entity.Property(e => e.HospitalType)
                    .HasMaxLength(50)
                    .HasColumnName("Hospital_Type");
            });

            modelBuilder.Entity<HospitalStaff>(entity =>
            {
                entity.ToTable("HospitalStaff");

                entity.Property(e => e.HospitalStaffId).HasColumnName("HospitalStaff_Id");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.HospitalId).HasColumnName("Hospital_Id");

                entity.Property(e => e.HospitalStaffPhone)
                    .HasMaxLength(20)
                    .HasColumnName("HospitalStaff_phone");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.HospitalStaffs)
                    .HasForeignKey(d => d.HospitalId)
                    .HasConstraintName("FK_HospitalStaff_Hospital");
            });

            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.Property(e => e.MedicalRecordId).HasColumnName("MedicalRecord_Id");

                entity.Property(e => e.DateMediaclRecord).HasColumnType("datetime");

                entity.Property(e => e.Diagnosis).HasMaxLength(50);

                entity.Property(e => e.DoctorId).HasColumnName("Doctor_Id");

                entity.Property(e => e.Medications).HasMaxLength(50);

                entity.Property(e => e.PatientId).HasColumnName("patient_Id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MedicalRecords)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_MedicalRecords_Doctor");
            });

            modelBuilder.Entity<Pateient>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.ToTable("Pateient");

                entity.Property(e => e.PatientId).HasColumnName("Patient_ID");

                entity.Property(e => e.Address).HasMaxLength(120);

                entity.Property(e => e.Age).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.MedicalRecordId).HasColumnName("MedicalRecord_id");

                entity.Property(e => e.PatientEmailAddress)
                    .HasMaxLength(50)
                    .HasColumnName("Patient_EmailAddress");

                entity.Property(e => e.PatientFirstName)
                    .HasMaxLength(50)
                    .HasColumnName("Patient_FirstName");

                entity.Property(e => e.PatientLastName)
                    .HasMaxLength(50)
                    .HasColumnName("Patient_LastName");

                entity.Property(e => e.PatientMiddleName)
                    .HasMaxLength(50)
                    .HasColumnName("Patient_MiddleName");

                entity.Property(e => e.PatientPhoneNumeber)
                    .HasMaxLength(20)
                    .HasColumnName("Patient_PhoneNumeber");
            });

            modelBuilder.Entity<Referral>(entity =>
            {
                entity.ToTable("Referral");

                entity.Property(e => e.ReferralId).HasColumnName("Referral_Id");

                entity.Property(e => e.AppointmentId).HasColumnName("Appointment_id");

                entity.Property(e => e.Attachment).HasMaxLength(100);

                entity.Property(e => e.ClinicFindings)
                    .HasMaxLength(250)
                    .HasColumnName("Clinic_Findings");

                entity.Property(e => e.ClinicId).HasColumnName("Clinic_Id");

                entity.Property(e => e.DateOfReferral).HasColumnType("datetime");

                entity.Property(e => e.Department).HasMaxLength(50);

                entity.Property(e => e.DiagnosisResult).HasMaxLength(150);

                entity.Property(e => e.DoctorId).HasColumnName("Doctor_Id");

                entity.Property(e => e.HopsitalId).HasColumnName("Hopsital_Id");

                entity.Property(e => e.PatientId).HasColumnName("Patient_Id");

                entity.Property(e => e.ReasonsForReferrals).HasMaxLength(100);

                entity.Property(e => e.ReferralDescription)
                    .HasMaxLength(150)
                    .HasColumnName("Referral_description");

                entity.Property(e => e.RxGiven)
                    .HasMaxLength(50)
                    .HasColumnName("rxGiven");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Referrals)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_Referral_Appointment1");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Referrals)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Referral_Doctor1");

                entity.HasOne(d => d.Hopsital)
                    .WithMany(p => p.Referrals)
                    .HasForeignKey(d => d.HopsitalId)
                    .HasConstraintName("FK_Referral_Hospital1");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Referrals)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Referral_Pateient1");
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.Property(e => e.SpecializationId)
                    .HasMaxLength(50)
                    .HasColumnName("Specialization_Id");

                entity.Property(e => e.SpecializationDescription).HasColumnName("Specialization_Description");

                entity.Property(e => e.SpecializationName)
                    .HasMaxLength(50)
                    .HasColumnName("Specialization_Name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.Property(e => e.ClinicId).HasColumnName("Clinic_id");

                entity.Property(e => e.ClinicName)
                    .HasMaxLength(50)
                    .HasColumnName("Clinic_Name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorId).HasColumnName("Doctor_id");

                entity.Property(e => e.HopitalName)
                    .HasMaxLength(50)
                    .HasColumnName("Hopital_Name");

                entity.Property(e => e.HospitalId).HasColumnName("Hospital_id");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Role)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ClinicId)
                    .HasConstraintName("FK_Users_Clinic");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Users_doctorId");

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.HospitalId)
                    .HasConstraintName("FK_Users_Hopitals");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Users_PatientId");
            });

            modelBuilder.Entity<UserChat>(entity =>
            {
                entity.ToTable("UserChat");

                entity.HasIndex(e => e.Username, "UQ__UserChat__536C85E4E47A7ECB")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Username).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
