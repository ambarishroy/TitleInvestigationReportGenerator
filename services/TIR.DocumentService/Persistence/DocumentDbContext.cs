using Microsoft.EntityFrameworkCore;
namespace TIR.DocumentService.Persistence
{
    public sealed class DocumentDbContext: DbContext
    {
        public DbSet<DocumentRecord> Documents => Set<DocumentRecord>();
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options): base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentRecord>()
                .HasKey(d => d.DocumentId);

            modelBuilder.Entity<DocumentRecord>()
                .HasIndex(d => d.ProjectId);

            modelBuilder.Entity<DocumentRecord>()
                .Property(d => d.Sha256Hash)
                .IsRequired();
        }
    }
}
