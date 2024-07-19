using TranslationsTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Debug;

namespace TranslationsTask.Data
{
    namespace TranslationsTask.Data
    {
        public class TranslationsContext : DbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=%CONTENTROOTPATH%\\Data\\Translations.mdf;Integrated Security=True;Connect Timeout=30".Replace("%CONTENTROOTPATH%", Directory.GetCurrentDirectory()));
            }

            public TranslationsContext() { }

            public virtual DbSet<TranslationProject> Projects { get; set; }
            public virtual DbSet<TranslationTask> Tasks { get; set; }
            public virtual DbSet<Translator> Translators { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<TranslationProject>()
                    .HasMany(x => x.Tasks)
                    .WithOne(x => x.Project)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<TranslationTask>()
                    .HasOne(x => x.Assignee)
                    .WithMany(x => x.Tasks)
                    .OnDelete(DeleteBehavior.SetNull);
            }
        }
    }

}
