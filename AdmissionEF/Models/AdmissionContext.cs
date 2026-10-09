using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AdmissionEF.Models;

public partial class AdmissionContext : DbContext
{
    public AdmissionContext()
    {
    }

    public AdmissionContext(DbContextOptions<AdmissionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Applicant> Applicants { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<EntranceTest> EntranceTests { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderEnrollment> OrderEnrollments { get; set; }

    public virtual DbSet<Specialty> Specialties { get; set; }

    public virtual DbSet<SpecialtyTest> SpecialtyTests { get; set; }

    public virtual DbSet<TestResult> TestResults { get; set; }

    public virtual DbSet<VAdmissionPlanReport> VAdmissionPlanReports { get; set; }

    public virtual DbSet<VApplicantResult> VApplicantResults { get; set; }

    public virtual DbSet<VApplicationDynamic> VApplicationDynamics { get; set; }

    public virtual DbSet<VApplicationFullInfo> VApplicationFullInfos { get; set; }

    public virtual DbSet<VCompetitiveList> VCompetitiveLists { get; set; }

    public virtual DbSet<VEnrollmentFullInfo> VEnrollmentFullInfos { get; set; }

    public virtual DbSet<VSpecialtyFullInfo> VSpecialtyFullInfos { get; set; }

    public virtual DbSet<VSpecialtyTest> VSpecialtyTests { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.ToTable("Applicant");

            entity.HasIndex(e => e.FullName, "IX_Applicant_FullName");

            entity.HasIndex(e => e.Passport, "UQ_Applicant_Passport").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.AverageScore).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.Benefits).HasMaxLength(300);
            entity.Property(e => e.EducationDocument).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Passport).HasMaxLength(30);
            entity.Property(e => e.Phone).HasMaxLength(30);
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.ToTable("Application");

            entity.HasIndex(e => e.ApplicantId, "IX_Application_ApplicantId");

            entity.HasIndex(e => e.SpecialtyId, "IX_Application_SpecialtyId");

            entity.HasIndex(e => e.SubmissionDateTime, "IX_Application_SubmissionDateTime");

            entity.HasIndex(e => new { e.ApplicantId, e.SpecialtyId }, "UQ_Application_ApplicantSpec").IsUnique();

            entity.HasIndex(e => e.ApplicationNumber, "UQ_Application_Number").IsUnique();

            entity.Property(e => e.ApplicationNumber).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValue("на рассмотрении", "DF_Application_Status");
            entity.Property(e => e.SubmissionDateTime)
                .HasDefaultValueSql("(getdate())", "DF_Application_SubmissionDateTime")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Applicant).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ApplicantId)
                .HasConstraintName("FK_Application_Applicant");

            entity.HasOne(d => d.Specialty).WithMany(p => p.Applications)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_Specialty");
        });

        modelBuilder.Entity<EntranceTest>(entity =>
        {
            entity.HasKey(e => e.TestId);

            entity.ToTable("EntranceTest");

            entity.HasIndex(e => e.Name, "UQ_EntranceTest_Name").IsUnique();

            entity.Property(e => e.Form).HasMaxLength(50);
            entity.Property(e => e.MinScore).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.ToTable("Faculty");

            entity.HasIndex(e => e.Name, "UQ_Faculty_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasIndex(e => e.OrderNumber, "UQ_Order_Number").IsUnique();

            entity.Property(e => e.OrderNumber).HasMaxLength(30);
        });

        modelBuilder.Entity<OrderEnrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId);

            entity.ToTable("OrderEnrollment");

            entity.HasIndex(e => e.OrderId, "IX_OrderEnrollment_OrderId");

            entity.HasIndex(e => e.ApplicationId, "UQ_OrderEnrollment_Application").IsUnique();

            entity.Property(e => e.FundingForm).HasMaxLength(20);

            entity.HasOne(d => d.Application).WithOne(p => p.OrderEnrollment)
                .HasForeignKey<OrderEnrollment>(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderEnrollment_Application");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderEnrollments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderEnrollment_Order");
        });

        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.ToTable("Specialty");

            entity.HasIndex(e => e.Code, "UQ_Specialty_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.DurationYears).HasColumnType("decimal(3, 1)");
            entity.Property(e => e.EducationForm).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Faculty).WithMany(p => p.Specialties)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Specialty_Faculty");
        });

        modelBuilder.Entity<SpecialtyTest>(entity =>
        {
            entity.HasKey(e => new { e.SpecialtyId, e.TestId });

            entity.ToTable("SpecialtyTest");

            entity.Property(e => e.Weight)
                .HasDefaultValue(1.00m, "DF_SpecialtyTest_Weight")
                .HasColumnType("decimal(4, 2)");

            entity.HasOne(d => d.Specialty).WithMany(p => p.SpecialtyTests)
                .HasForeignKey(d => d.SpecialtyId)
                .HasConstraintName("FK_SpecialtyTest_Specialty");

            entity.HasOne(d => d.Test).WithMany(p => p.SpecialtyTests)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SpecialtyTest_Test");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.ResultId);

            entity.ToTable("TestResult");

            entity.HasIndex(e => e.ApplicantId, "IX_TestResult_ApplicantId");

            entity.HasIndex(e => new { e.ApplicantId, e.TestId }, "UQ_TestResult_ApplicantTest").IsUnique();

            entity.Property(e => e.CertificateNumber).HasMaxLength(50);
            entity.Property(e => e.Score).HasColumnType("decimal(5, 1)");

            entity.HasOne(d => d.Applicant).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.ApplicantId)
                .HasConstraintName("FK_TestResult_Applicant");

            entity.HasOne(d => d.Test).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestResult_Test");
        });

        modelBuilder.Entity<VAdmissionPlanReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_AdmissionPlanReport");

            entity.Property(e => e.EducationForm).HasMaxLength(30);
            entity.Property(e => e.FacultyName).HasMaxLength(200);
            entity.Property(e => e.SpecialtyCode).HasMaxLength(20);
            entity.Property(e => e.SpecialtyName).HasMaxLength(200);
        });

        modelBuilder.Entity<VApplicantResult>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_ApplicantResults");

            entity.Property(e => e.ApplicantName).HasMaxLength(200);
            entity.Property(e => e.CertificateNumber).HasMaxLength(50);
            entity.Property(e => e.Score).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.TestForm).HasMaxLength(50);
            entity.Property(e => e.TestName).HasMaxLength(150);
        });

        modelBuilder.Entity<VApplicationDynamic>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_ApplicationDynamics");

            entity.Property(e => e.CompetitionRatio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EducationForm).HasMaxLength(30);
            entity.Property(e => e.FacultyName).HasMaxLength(200);
            entity.Property(e => e.SpecialtyCode).HasMaxLength(20);
            entity.Property(e => e.SpecialtyName).HasMaxLength(200);
        });

        modelBuilder.Entity<VApplicationFullInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_ApplicationFullInfo");

            entity.Property(e => e.ApplicantName).HasMaxLength(200);
            entity.Property(e => e.ApplicationNumber).HasMaxLength(20);
            entity.Property(e => e.AverageScore).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.EducationForm).HasMaxLength(30);
            entity.Property(e => e.FacultyName).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.SpecialtyCode).HasMaxLength(20);
            entity.Property(e => e.SpecialtyName).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.SubmissionDateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<VCompetitiveList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_CompetitiveList");

            entity.Property(e => e.ApplicantName).HasMaxLength(200);
            entity.Property(e => e.ApplicationNumber).HasMaxLength(20);
            entity.Property(e => e.AverageScore).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.EducationForm).HasMaxLength(30);
            entity.Property(e => e.FacultyName).HasMaxLength(200);
            entity.Property(e => e.SpecialtyCode).HasMaxLength(20);
            entity.Property(e => e.SpecialtyName).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.TestsSum).HasColumnType("decimal(38, 1)");
            entity.Property(e => e.TotalScore).HasColumnType("decimal(7, 2)");
        });

        modelBuilder.Entity<VEnrollmentFullInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_EnrollmentFullInfo");

            entity.Property(e => e.ApplicantName).HasMaxLength(200);
            entity.Property(e => e.ApplicationNumber).HasMaxLength(20);
            entity.Property(e => e.EducationForm).HasMaxLength(30);
            entity.Property(e => e.FacultyName).HasMaxLength(200);
            entity.Property(e => e.FundingForm).HasMaxLength(20);
            entity.Property(e => e.OrderNumber).HasMaxLength(30);
            entity.Property(e => e.SpecialtyCode).HasMaxLength(20);
            entity.Property(e => e.SpecialtyName).HasMaxLength(200);
        });

        modelBuilder.Entity<VSpecialtyFullInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_SpecialtyFullInfo");

            entity.Property(e => e.DurationYears).HasColumnType("decimal(3, 1)");
            entity.Property(e => e.EducationForm).HasMaxLength(30);
            entity.Property(e => e.FacultyName).HasMaxLength(200);
            entity.Property(e => e.SpecialtyCode).HasMaxLength(20);
            entity.Property(e => e.SpecialtyName).HasMaxLength(200);
        });

        modelBuilder.Entity<VSpecialtyTest>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_SpecialtyTests");

            entity.Property(e => e.MinScore).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.SpecialtyCode).HasMaxLength(20);
            entity.Property(e => e.SpecialtyName).HasMaxLength(200);
            entity.Property(e => e.TestForm).HasMaxLength(50);
            entity.Property(e => e.TestName).HasMaxLength(150);
            entity.Property(e => e.Weight).HasColumnType("decimal(4, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
