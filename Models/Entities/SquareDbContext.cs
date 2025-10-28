using Microsoft.EntityFrameworkCore;

namespace SquarePixel.Models.Entities;

public class SquareDbContext: DbContext
{
    public SquareDbContext(DbContextOptions<SquareDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Photo> Photos { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(x => x.Id);
            
            entity.Property(p => p.FilePath)
                .IsRequired()
                .HasMaxLength(512);
            
            entity.Property(p => p.UploadedAt)
                .IsRequired()
                .HasMaxLength(512);
            
            entity.Property(p => p.Caption)
                .HasMaxLength(512);
            
            entity.Property(p => p.DeletionDate)
                .HasMaxLength(512);
            
            entity.Property(p => p.Tags)
                .IsRequired()
                .HasMaxLength(512);
        });
    }
    
}