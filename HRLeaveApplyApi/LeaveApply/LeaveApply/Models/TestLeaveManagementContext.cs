using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LeaveApply.Models
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

        public virtual DbSet<ApplyLeave> ApplyLeave { get; set; }
        public virtual DbSet<BalanceAvailable> BalanceAvailable { get; set; }
        public virtual DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public virtual DbSet<HrloginDetails> HrloginDetails { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=DELL_PC-PC\\SQLEXPRESS;Database=TestLeaveManagement;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplyLeave>(entity =>
            {
                entity.HasKey(e => e.Applyid)
                    .HasName("PK__ApplyLea__A95B2386FF0AB329");

                entity.Property(e => e.Applyid).HasColumnName("applyid");

                entity.Property(e => e.LeaveEndDate).HasColumnType("datetime");

                entity.Property(e => e.LeaveReason).IsUnicode(false);

                entity.Property(e => e.LeaveType)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LeavestartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'Pending')");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.ApplyLeave)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__ApplyLeav__EmpId__25869641");

                entity.HasOne(d => d.Leave)
                    .WithMany(p => p.ApplyLeave)
                    .HasForeignKey(d => d.LeaveId)
                    .HasConstraintName("FK__ApplyLeav__Leave__267ABA7A");
            });

            modelBuilder.Entity<BalanceAvailable>(entity =>
            {
                entity.HasKey(e => e.LeaveId)
                    .HasName("PK__BalanceA__796DB95910343ADF");

                entity.Property(e => e.SickBalance).HasDefaultValueSql("((8))");

                entity.Property(e => e.VacationBalance).HasDefaultValueSql("((10))");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.BalanceAvailable)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK1");
            });

            modelBuilder.Entity<EmployeeDetails>(entity =>
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

            modelBuilder.Entity<HrloginDetails>(entity =>
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
