﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VetClinic.Data.Data;

#nullable disable

namespace VetClinic.Data.Migrations
{
    [DbContext(typeof(VetClinicContext))]
    [Migration("20230527222020_M10")]
    partial class M10
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PositionServiceGroup", b =>
                {
                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceGroupId")
                        .HasColumnType("int");

                    b.HasKey("PositionId", "ServiceGroupId");

                    b.HasIndex("ServiceGroupId");

                    b.ToTable("PositionServiceGroup");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Clinic.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"), 1L, 1);

                    b.Property<DateTime>("AppointmentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("HasArrived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("IssueDescription")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("OwnerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppointmentId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Clinic.ClinicSchedule", b =>
                {
                    b.Property<int>("ClinicScheduleDayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClinicScheduleDayId"), 1L, 1);

                    b.Property<TimeSpan?>("CloseTime")
                        .HasColumnType("time");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<TimeSpan?>("OpenTime")
                        .HasColumnType("time");

                    b.HasKey("ClinicScheduleDayId");

                    b.ToTable("ClinicSchedule");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Clinic.ClinicScheduleException", b =>
                {
                    b.Property<int>("ClinicScheduleExceptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClinicScheduleExceptionId"), 1L, 1);

                    b.Property<TimeSpan?>("CloseTime")
                        .HasColumnType("time");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<TimeSpan?>("OpenTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("ScheduleDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ClinicScheduleExceptionId");

                    b.ToTable("ClinicScheduleException");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Clinic.Info", b =>
                {
                    b.Property<int>("InfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InfoId"), 1L, 1);

                    b.Property<string>("InfoDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InfoIsActive")
                        .HasColumnType("bit");

                    b.Property<string>("InfoTitle")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("InfoId");

                    b.ToTable("Info");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Clinic.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"), 1L, 1);

                    b.Property<string>("ServiceDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServiceGroupId")
                        .HasColumnType("int");

                    b.Property<bool>("ServiceIsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ServiceId");

                    b.HasIndex("ServiceGroupId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Clinic.ServiceGroup", b =>
                {
                    b.Property<int>("ServiceGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceGroupId"), 1L, 1);

                    b.Property<string>("ServiceGroupDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceGroupIconName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ServiceGroupIsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ServiceGroupName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ServiceGroupId");

                    b.ToTable("ServiceGroup");
                });

            modelBuilder.Entity("VetClinic.Data.Data.CMS.InfoPage", b =>
                {
                    b.Property<int>("InfoPageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InfoPageId"), 1L, 1);

                    b.Property<string>("InfoContent")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<DateTime>("InfoLastEdited")
                        .HasColumnType("datetime2");

                    b.Property<string>("InfoLinkTitle")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("InfoPageTitle")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("InfoPageId");

                    b.ToTable("InfoPage");
                });

            modelBuilder.Entity("VetClinic.Data.Data.CMS.News", b =>
                {
                    b.Property<int>("NewsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NewsId"), 1L, 1);

                    b.Property<string>("AddedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("LinkTitle")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("NewsIsNotArchived")
                        .HasColumnType("bit");

                    b.Property<string>("PageTitle")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("NewsId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("VetClinic.Data.Data.CMS.Page", b =>
                {
                    b.Property<int>("PageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PageId"), 1L, 1);

                    b.Property<string>("AddedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LinkTitle")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PageTitle")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("PageId");

                    b.ToTable("Page");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Staff.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"), 1L, 1);

                    b.Property<string>("EmployeeBio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeEducation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EmployeeIsActive")
                        .HasColumnType("bit");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<byte[]>("EmployeePhoto")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("EmployeeSurname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<int>("TitleId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("PositionId");

                    b.HasIndex("TitleId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Staff.EmployeeSchedule", b =>
                {
                    b.Property<int>("EmployeeScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeScheduleId"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasColumnType("varchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("EndTime")
                        .HasColumnType("time");

                    b.Property<bool>("IsWorking")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ScheduleDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan?>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("EmployeeScheduleId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeSchedule");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Staff.EmployeeServiceGroup", b =>
                {
                    b.Property<int>("EmployeeServiceGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeServiceGroupId"), 1L, 1);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceGroupId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeServiceGroupId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ServiceGroupId");

                    b.ToTable("EmployeeServiceGroup");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Staff.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PositionId"), 1L, 1);

                    b.Property<bool>("PositionIsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PositionId");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Staff.Title", b =>
                {
                    b.Property<int>("TitleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TitleId"), 1L, 1);

                    b.Property<string>("TitleAbbrev")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("TitleIsActive")
                        .HasColumnType("bit");

                    b.Property<string>("TitleName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("TitleId");

                    b.ToTable("Title");
                });

            modelBuilder.Entity("PositionServiceGroup", b =>
                {
                    b.HasOne("VetClinic.Data.Data.Staff.Position", null)
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetClinic.Data.Data.Clinic.ServiceGroup", null)
                        .WithMany()
                        .HasForeignKey("ServiceGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VetClinic.Data.Data.Clinic.Appointment", b =>
                {
                    b.HasOne("VetClinic.Data.Data.Staff.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Clinic.Service", b =>
                {
                    b.HasOne("VetClinic.Data.Data.Clinic.ServiceGroup", "ServiceGroup")
                        .WithMany("Services")
                        .HasForeignKey("ServiceGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceGroup");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Staff.Employee", b =>
                {
                    b.HasOne("VetClinic.Data.Data.Staff.Position", "Position")
                        .WithMany("Employee")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetClinic.Data.Data.Staff.Title", "Title")
                        .WithMany("Employee")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Staff.EmployeeSchedule", b =>
                {
                    b.HasOne("VetClinic.Data.Data.Staff.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Staff.EmployeeServiceGroup", b =>
                {
                    b.HasOne("VetClinic.Data.Data.Staff.Employee", "Employee")
                        .WithMany("EmployeeServiceGroups")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetClinic.Data.Data.Clinic.ServiceGroup", "ServiceGroup")
                        .WithMany("EmployeeServiceGroups")
                        .HasForeignKey("ServiceGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("ServiceGroup");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Clinic.ServiceGroup", b =>
                {
                    b.Navigation("EmployeeServiceGroups");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Staff.Employee", b =>
                {
                    b.Navigation("EmployeeServiceGroups");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Staff.Position", b =>
                {
                    b.Navigation("Employee");
                });

            modelBuilder.Entity("VetClinic.Data.Data.Staff.Title", b =>
                {
                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
