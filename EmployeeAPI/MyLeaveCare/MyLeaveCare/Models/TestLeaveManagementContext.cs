using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MyLeaveCare.Models
{
    public partial class TestLeaveManagementContext : DbContext
    {
        //public TestLeaveManagementContext()
        //{
        //}

        public TestLeaveManagementContext(DbContextOptions<TestLeaveManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplyLeave> ApplyLeaves { get; set; }
        public virtual DbSet<BalanceAvailable> BalanceAvailables { get; set; }
        public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public virtual DbSet<HrloginDetail> HrloginDetails { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DELL_PC-PC\\SQLEXPRESS;Database=TestLeaveManagement;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplyLeave>(entity =>
            {
                entity.HasKey(e => e.Applyid)
                    .HasName("PK__ApplyLea__A95B2386FF0AB329");

                entity.ToTable("ApplyLeave");

                entity.Property(e => e.Applyid).HasColumnName("applyid");

                entity.Property(e => e.LeaveEndDate).HasColumnType("datetime");

                entity.Property(e => e.LeaveReason).IsUnicode(false);

                entity.Property(e => e.LeaveType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LeavestartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.ApplyLeaves)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__ApplyLeav__EmpId__25869641");

                entity.HasOne(d => d.Leave)
                    .WithMany(p => p.ApplyLeaves)
                    .HasForeignKey(d => d.LeaveId)
                    .HasConstraintName("FK__ApplyLeav__Leave__267ABA7A");
            });

            modelBuilder.Entity<BalanceAvailable>(entity =>
            {
                entity.HasKey(e => e.LeaveId)
                    .HasName("PK__BalanceA__796DB95910343ADF");

                entity.ToTable("BalanceAvailable");

                entity.Property(e => e.SickBalance).HasDefaultValueSql("((8))");

                entity.Property(e => e.VacationBalance).HasDefaultValueSql("((10))");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.BalanceAvailables)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK1");
            });

            modelBuilder.Entity<EmployeeDetail>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.Property(e => e.EmpId).ValueGeneratedNever();

                entity.Property(e => e.EmpEmail).IsRequired();

                entity.Property(e => e.EmpName).IsRequired();

                entity.Property(e => e.EmpPass)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EmpPhone).IsRequired();
            });

            modelBuilder.Entity<HrloginDetail>(entity =>
            {
                entity.HasKey(e => e.HrId);

                entity.ToTable("HRLoginDetails");

                entity.Property(e => e.HrId).ValueGeneratedNever();

                entity.Property(e => e.HrEmail).IsRequired();

                entity.Property(e => e.HrName).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
