using Microsoft.EntityFrameworkCore;

namespace TMS.Domain.Models;

public partial class TMSContext : DbContext
{
    public TMSContext()
    {
    }

    public TMSContext(DbContextOptions<TMSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeTask> EmployeeTasks { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<TaskAttachment> TaskAttachments { get; set; }

    public virtual DbSet<TaskNote> TaskNotes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-UAOEV7U;Initial Catalog=TMS;Trusted_Connection=SSPI;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<EmployeeTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Task");

            entity.ToTable("EmployeeTask");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DueDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsCompleted).HasDefaultValue(false);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeTasks)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Tasks_Employee");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.ToTable("ErrorLog");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TaskAttachment>(entity =>
        {
            entity.ToTable("TaskAttachment");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileExtension)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FileName).HasMaxLength(100);

            entity.HasOne(d => d.Task).WithMany(p => p.TaskAttachments)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_TaskAttachment_EmployeeTask");
        });

        modelBuilder.Entity<TaskNote>(entity =>
        {
            entity.Property(e => e.Note).HasMaxLength(500);

            entity.HasOne(d => d.Task).WithMany(p => p.TaskNotes)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_TaskNotes_EmployeeTask");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
