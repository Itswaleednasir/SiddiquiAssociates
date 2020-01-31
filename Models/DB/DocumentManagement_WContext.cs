using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyClientCoreProject.ViewModel;

namespace MyClientCoreProject.Models.DB
{
    public partial class DocumentManagement_WContext : DbContext
    {
        public DocumentManagement_WContext()
        {
        }

        public DocumentManagement_WContext(DbContextOptions<DocumentManagement_WContext> options): base(options){}

        public DbQuery<EmployeeRoleViewModel> EmployeeRoleViewModel { get; set; }
        public DbQuery<HouseAddressViewModel> HouseAddressViewModel { get; set; }
        public DbQuery<ReceiptPaymentViewModel> ReceiptPaymentViewModel { get; set; }
        public DbQuery<GetReceiptViewModel> GetReceiptViewModel { get; set; }


        public virtual DbSet<TblEmployee> TblEmployee { get; set; }
        public virtual DbSet<TblEmployeeRole> TblEmployeeRole { get; set; }
        public virtual DbSet<TblFile> TblFile { get; set; }
        public virtual DbSet<TblHouseAddress> TblHouseAddress { get; set; }
        public virtual DbSet<TblMessrs> TblMessrs { get; set; }
        public virtual DbSet<TblPayments> TblPayments { get; set; }
        public virtual DbSet<TblReceipt> TblReceipt { get; set; }
        public virtual DbSet<TblRegisterMessrs> TblRegisterMessrs { get; set; }
        public virtual DbSet<TblSectors> TblSectors { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=192.168.10.231;Database=DocumentManagement_W;user id=sa;password=marvin2;;MultipleActiveResultSets=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.ToTable("tblEmployee");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblEmployee)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("tblEmployeeRole_To_tblEmployee");
            });

            modelBuilder.Entity<TblEmployeeRole>(entity =>
            {
                entity.ToTable("tblEmployeeRole");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFile>(entity =>
            {
                entity.ToTable("tblFile");

                entity.Property(e => e.FileNo)
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblHouseAddress>(entity =>
            {
                entity.ToTable("tblHouseAddress");

                entity.Property(e => e.HouseNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SectorId).HasColumnName("SectorID");

                entity.HasOne(d => d.Sector)
                    .WithMany(p => p.TblHouseAddress)
                    .HasForeignKey(d => d.SectorId)
                    .HasConstraintName("tblSector_To_tblHouseAddress");
            });

            modelBuilder.Entity<TblMessrs>(entity =>
            {
                entity.ToTable("tblMessrs");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPayments>(entity =>
            {
                entity.ToTable("tblPayments");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.PaymentDate)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.TblPayments)
                    .HasForeignKey(d => d.ReceiptId)
                    .HasConstraintName("tblReceipt_To_tblPayments");
            });

            modelBuilder.Entity<TblReceipt>(entity =>
            {
                entity.ToTable("tblReceipt");

                entity.Property(e => e.CaseTotalCost).HasColumnType("money");

                entity.Property(e => e.ChallanAmount).HasColumnType("money");

                entity.Property(e => e.ChallanDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DiaryDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.DiaryNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DispatchDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.DispatchNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.FileExpenditure).HasColumnType("money");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.HouseId).HasColumnName("HouseID");

                entity.Property(e => e.MessrsId).HasColumnName("MessrsID");

                entity.Property(e => e.ReceiptDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterAmount).HasColumnType("money");

                entity.Property(e => e.RegisterDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterMessrsId).HasColumnName("RegisterMessrsID");

                entity.Property(e => e.Sno)
                    .IsRequired()
                    .HasColumnName("SNo")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.StampDutyAmount).HasColumnType("money");

                entity.Property(e => e.StampDutyDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TblReceipt)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("tblEmployee_to_tblReceipt");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.TblReceipt)
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("tblFile_to_tblReceipt");

                entity.HasOne(d => d.House)
                    .WithMany(p => p.TblReceipt)
                    .HasForeignKey(d => d.HouseId)
                    .HasConstraintName("tblHouseAddress_to_tblReceipt");

                entity.HasOne(d => d.Messrs)
                    .WithMany(p => p.TblReceipt)
                    .HasForeignKey(d => d.MessrsId)
                    .HasConstraintName("tblMessrs_to_tblReceipt");

                entity.HasOne(d => d.RegisterMessrs)
                    .WithMany(p => p.TblReceipt)
                    .HasForeignKey(d => d.RegisterMessrsId)
                    .HasConstraintName("tblRegisterMessrs_to_tblReceipt");
            });

            modelBuilder.Entity<TblRegisterMessrs>(entity =>
            {
                entity.ToTable("tblRegisterMessrs");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSectors>(entity =>
            {
                entity.ToTable("tblSectors");

                entity.Property(e => e.SectorNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
