using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanpheEMR.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelTenCuaBan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transfers");

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "Wards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CancelledByUserId",
                table: "Wards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChiefComplaint",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Visits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "QueueNo",
                table: "Visits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserIdCancel",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VisitCode",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Transfers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TransferStatus",
                table: "Transfers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "cancelReason",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "voidedByUserId",
                table: "Transfers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CancelledByUserId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "SubCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CancelledByUserId",
                table: "SubCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CancelledByUserId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "PrescriptionItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PrescriptionItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserIdCancel",
                table: "PrescriptionItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "Prescription",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Prescription",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserIdCancel",
                table: "Prescription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IdCardNumber",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "voidReason",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "voidedByUserId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "PatientGuarantees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CancelledByUserId",
                table: "PatientGuarantees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OTSchedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "cancelReason",
                table: "OTSchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "cancelledByUserId",
                table: "OTSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CancelUserId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "ItemCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserIdCancel",
                table: "ItemCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "GoodsReceipts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "GoodsReceipts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CancelledByUserId",
                table: "GoodsReceipts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "GoodsReceipts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Discharges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "voidReason",
                table: "Discharges",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "voidedByUserId",
                table: "Discharges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VoidedBy",
                table: "Diagnosis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isDelete",
                table: "Diagnosis",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "Diagnosis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDelete",
                table: "ClinicalNote",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "voidReason",
                table: "ClinicalNote",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "voidedByUserId",
                table: "ClinicalNote",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BedCode",
                table: "Beds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "Beds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CancelledByUserId",
                table: "Beds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Beds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "BedFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CancelledByUserId",
                table: "BedFeatures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CancelUserId",
                table: "Admissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Admissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReasonCancel",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BloodGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodGroupName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    cancelledByUserId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    cancelReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorOrder_Employees_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorOrder_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HeartRate = table.Column<int>(type: "int", nullable: false),
                    BloodPressure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: false),
                    SpO2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BMI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    voidReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    voidedByUserId = table.Column<int>(type: "int", nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vitals_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vitals_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BloodDonor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    IsPermanentlyDeferred = table.Column<bool>(type: "bit", nullable: false),
                    TotalDonations = table.Column<int>(type: "int", nullable: false),
                    LastDonatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BloodGroupId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodDonor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodDonor_BloodGroup_BloodGroupId",
                        column: x => x.BloodGroupId,
                        principalTable: "BloodGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonor_BloodGroupId",
                table: "BloodDonor",
                column: "BloodGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorOrder_ProviderId",
                table: "DoctorOrder",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorOrder_VisitId",
                table: "DoctorOrder",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Vitals_PatientId",
                table: "Vitals",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Vitals_VisitId",
                table: "Vitals",
                column: "VisitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodDonor");

            migrationBuilder.DropTable(
                name: "DoctorOrder");

            migrationBuilder.DropTable(
                name: "Vitals");

            migrationBuilder.DropTable(
                name: "BloodGroup");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "Wards");

            migrationBuilder.DropColumn(
                name: "CancelledByUserId",
                table: "Wards");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "ChiefComplaint",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "QueueNo",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "UserIdCancel",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "VisitCode",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "TransferStatus",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "cancelReason",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "voidedByUserId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CancelledByUserId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "CancelledByUserId",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CancelledByUserId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "PrescriptionItem");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PrescriptionItem");

            migrationBuilder.DropColumn(
                name: "UserIdCancel",
                table: "PrescriptionItem");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "UserIdCancel",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "IdCardNumber",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "voidReason",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "voidedByUserId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "PatientGuarantees");

            migrationBuilder.DropColumn(
                name: "CancelledByUserId",
                table: "PatientGuarantees");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OTSchedules");

            migrationBuilder.DropColumn(
                name: "cancelReason",
                table: "OTSchedules");

            migrationBuilder.DropColumn(
                name: "cancelledByUserId",
                table: "OTSchedules");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CancelUserId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "ItemCategories");

            migrationBuilder.DropColumn(
                name: "UserIdCancel",
                table: "ItemCategories");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "GoodsReceipts");

            migrationBuilder.DropColumn(
                name: "CancelledByUserId",
                table: "GoodsReceipts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "GoodsReceipts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Discharges");

            migrationBuilder.DropColumn(
                name: "voidReason",
                table: "Discharges");

            migrationBuilder.DropColumn(
                name: "voidedByUserId",
                table: "Discharges");

            migrationBuilder.DropColumn(
                name: "VoidedBy",
                table: "Diagnosis");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "Diagnosis");

            migrationBuilder.DropColumn(
                name: "reason",
                table: "Diagnosis");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "ClinicalNote");

            migrationBuilder.DropColumn(
                name: "voidReason",
                table: "ClinicalNote");

            migrationBuilder.DropColumn(
                name: "voidedByUserId",
                table: "ClinicalNote");

            migrationBuilder.DropColumn(
                name: "BedCode",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "CancelledByUserId",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "BedFeatures");

            migrationBuilder.DropColumn(
                name: "CancelledByUserId",
                table: "BedFeatures");

            migrationBuilder.DropColumn(
                name: "CancelUserId",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "ReasonCancel",
                table: "Admissions");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "GoodsReceipts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
