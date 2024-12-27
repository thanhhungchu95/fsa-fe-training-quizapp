﻿namespace QuizApp.Data;

public interface IUnitOfWork : IDisposable
{
    QuizAppDbContext Context { get; }

    IGenericRepository<Quiz> QuizRepository { get; }

    IGenericRepository<Question> QuestionRepository { get; }

    IGenericRepository<UserQuiz> UserQuizRepository { get; }

    IGenericRepository<QuizQuestion> QuizQuestionRepository { get; }

    IGenericRepository<UserAnswer> UserAnswerRepository { get; }

    IGenericRepository<User> UserRepository { get; }

    IGenericRepository<Role> RoleRepository { get; }

    IGenericRepository<Answer> AnswerRepository { get; }

    IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;

    /// <summary>
    /// Saves all changes made in the unit of work to the underlying data store.
    /// </summary>
    /// <returns>The number of objects written to the underlying data store.</returns>
    int SaveChanges();

    /// <summary>
    /// Saves all changes made in this unit of work to the underlying database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new transaction asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task BeginTransactionAsync();

    /// <summary>
    /// Commits the current transaction asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CommitTransactionAsync();

    /// <summary>
    /// Rolls back the current transaction asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RollbackTransactionAsync();
}
