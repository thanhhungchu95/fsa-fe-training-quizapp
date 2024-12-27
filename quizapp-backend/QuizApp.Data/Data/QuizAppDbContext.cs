using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuizApp.Data;

public class QuizAppDbContext : IdentityDbContext<User, Role, Guid>
{
    public QuizAppDbContext(DbContextOptions<QuizAppDbContext> options) : base(options)
    {
    }

    public DbSet<Quiz> Quizzes { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Answer> Answers { get; set; }

    public DbSet<UserQuiz> UserQuizzes { get; set; }

    public DbSet<QuizQuestion> QuizQuestions { get; set; }

    public DbSet<UserAnswer> UserAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<QuizQuestion>()
            .HasKey(qq => new { qq.QuizId, qq.QuestionId });

        builder.Entity<Answer>()
            .HasOne(a => a.Question)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<QuizQuestion>()
            .HasOne(qq => qq.Quiz)
            .WithMany(q => q.QuizQuestions)
            .HasForeignKey(qq => qq.QuizId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<QuizQuestion>()
            .HasOne(qq => qq.Question)
            .WithMany(q => q.QuizQuestions)
            .HasForeignKey(qq => qq.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UserQuiz>()
            .HasOne(uq => uq.Quiz)
            .WithMany(q => q.UserQuizzes)
            .HasForeignKey(uq => uq.QuizId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UserQuiz>()
            .HasOne(uq => uq.User)
            .WithMany(u => u.UserQuizzes)
            .HasForeignKey(uq => uq.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Answer>().Property(a => a.IsCorrect).HasDefaultValue(false);

        builder.Entity<Answer>().Property(s => s.IsActive).HasDefaultValue(true);

        builder.Entity<Quiz>().Property(s => s.IsActive).HasDefaultValue(true);

        builder.Entity<Question>().Property(s => s.IsActive).HasDefaultValue(true);

        builder.Entity<User>().Property(s => s.IsActive).HasDefaultValue(true);

        builder.Entity<Role>().Property(s => s.IsActive).HasDefaultValue(true);
    }
}
