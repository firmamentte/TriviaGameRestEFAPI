using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TriviaGameRestEFAPI.Data.Entities
{
    public partial class TriviaGameDBContext : DbContext
    {
        public TriviaGameDBContext()
        {
        }

        public TriviaGameDBContext(DbContextOptions<TriviaGameDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Choice> Choice { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Question> Question { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Utilities.DatabaseHelper.ConnectionString);
                optionsBuilder.UseLazyLoadingProxies(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.AnswerId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Choice)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.ChoiceId)
                    .HasConstraintName("FK_Answer_Choice");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answer_Game");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answer_Question");
            });

            modelBuilder.Entity<Choice>(entity =>
            {
                entity.Property(e => e.ChoiceId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ChoiceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Choice)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Choice_Question");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.GameId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_Genre");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.GenreName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.QuestionDescription)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_Genre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
