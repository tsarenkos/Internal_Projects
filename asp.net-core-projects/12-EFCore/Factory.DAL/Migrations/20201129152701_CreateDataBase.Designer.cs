﻿// <auto-generated />
using System;
using Factory.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Factory.DAL.Migrations
{
    [DbContext(typeof(FactoryContext))]
    [Migration("20201129152701_CreateDataBase")]
    partial class CreateDataBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Factory.DAL.Entities.Breakage", b =>
                {
                    b.Property<int>("BreakageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BreakageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CriticalLevelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<int?>("RequestId")
                        .HasColumnType("int");

                    b.Property<int>("ShiftNumber")
                        .HasColumnType("int");

                    b.HasKey("BreakageId");

                    b.HasIndex("CriticalLevelId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("MachineId");

                    b.HasIndex("RequestId");

                    b.ToTable("Breakages");
                });

            modelBuilder.Entity("Factory.DAL.Entities.CriticalLevelType", b =>
                {
                    b.Property<int>("CriticalLevelTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CriticalLevelId")
                        .UseIdentityColumn();

                    b.Property<string>("CriticalLevelValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CriticalLevelTypeId");

                    b.ToTable("CriticalLevelTypes");
                });

            modelBuilder.Entity("Factory.DAL.Entities.Deliverer", b =>
                {
                    b.Property<int>("DelivererId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DelivererName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DelivererId");

                    b.ToTable("Deliverers");
                });

            modelBuilder.Entity("Factory.DAL.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Factory.DAL.Entities.Machine", b =>
                {
                    b.Property<int>("MachineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateOfDelivery")
                        .HasColumnType("date");

                    b.Property<int>("DelivererId")
                        .HasColumnType("int");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("MachineId");

                    b.HasIndex("DelivererId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("Factory.DAL.Entities.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateOfCreate")
                        .HasColumnType("date");

                    b.Property<int?>("InnerRequestId")
                        .HasColumnType("int");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<int>("RequestCreatorId")
                        .HasColumnType("int");

                    b.Property<int?>("RequestHandlerId")
                        .HasColumnType("int");

                    b.Property<int>("RequestStatusId")
                        .HasColumnType("int");

                    b.HasKey("RequestId");

                    b.HasIndex("InnerRequestId");

                    b.HasIndex("MachineId");

                    b.HasIndex("RequestCreatorId");

                    b.HasIndex("RequestHandlerId");

                    b.HasIndex("RequestStatusId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Factory.DAL.Entities.RequestStatusType", b =>
                {
                    b.Property<int>("RequestStatusTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RequestStatusCode")
                        .UseIdentityColumn();

                    b.Property<string>("RequestStatusValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RequestStatusTypeId");

                    b.ToTable("RequestStatusTypes");
                });

            modelBuilder.Entity("Factory.DAL.Entities.Breakage", b =>
                {
                    b.HasOne("Factory.DAL.Entities.CriticalLevelType", "CriticalLevelType")
                        .WithMany("Breakages")
                        .HasForeignKey("CriticalLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Factory.DAL.Entities.Employee", "Employee")
                        .WithMany("Breakages")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Factory.DAL.Entities.Machine", "Machine")
                        .WithMany("Breakages")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Factory.DAL.Entities.Request", "Request")
                        .WithMany("Breakages")
                        .HasForeignKey("RequestId");

                    b.Navigation("CriticalLevelType");

                    b.Navigation("Employee");

                    b.Navigation("Machine");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("Factory.DAL.Entities.Machine", b =>
                {
                    b.HasOne("Factory.DAL.Entities.Deliverer", "Deliverer")
                        .WithMany("Machines")
                        .HasForeignKey("DelivererId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deliverer");
                });

            modelBuilder.Entity("Factory.DAL.Entities.Request", b =>
                {
                    b.HasOne("Factory.DAL.Entities.Request", "InnerRequest")
                        .WithMany()
                        .HasForeignKey("InnerRequestId");

                    b.HasOne("Factory.DAL.Entities.Machine", "Machine")
                        .WithMany("Requests")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Factory.DAL.Entities.Employee", "RequestCreator")
                        .WithMany("RequestsCreated")
                        .HasForeignKey("RequestCreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Factory.DAL.Entities.Employee", "RequestHandler")
                        .WithMany("RequestsHandled")
                        .HasForeignKey("RequestHandlerId");

                    b.HasOne("Factory.DAL.Entities.RequestStatusType", "RequestStatusType")
                        .WithMany("Requests")
                        .HasForeignKey("RequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InnerRequest");

                    b.Navigation("Machine");

                    b.Navigation("RequestCreator");

                    b.Navigation("RequestHandler");

                    b.Navigation("RequestStatusType");
                });

            modelBuilder.Entity("Factory.DAL.Entities.CriticalLevelType", b =>
                {
                    b.Navigation("Breakages");
                });

            modelBuilder.Entity("Factory.DAL.Entities.Deliverer", b =>
                {
                    b.Navigation("Machines");
                });

            modelBuilder.Entity("Factory.DAL.Entities.Employee", b =>
                {
                    b.Navigation("Breakages");

                    b.Navigation("RequestsCreated");

                    b.Navigation("RequestsHandled");
                });

            modelBuilder.Entity("Factory.DAL.Entities.Machine", b =>
                {
                    b.Navigation("Breakages");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("Factory.DAL.Entities.Request", b =>
                {
                    b.Navigation("Breakages");
                });

            modelBuilder.Entity("Factory.DAL.Entities.RequestStatusType", b =>
                {
                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}