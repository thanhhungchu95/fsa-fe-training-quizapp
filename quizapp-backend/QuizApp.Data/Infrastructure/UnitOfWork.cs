namespace QuizApp.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly QuizAppDbContext _context;

    private IGenericRepository<Quiz>? _quizRepository;

    private IGenericRepository<Question>? _questionRepository;

    private IGenericRepository<UserQuiz>? _userQuizRepository;

    private IGenericRepository<UserAnswer>? _userAnswerRepository;

    private IGenericRepository<QuizQuestion>? _quizQuestionRepository;

    private IGenericRepository<User>? _userRepository;

    private IGenericRepository<Role>? _roleRepository;

    private IGenericRepository<Answer>? _answerRepository;

    public UnitOfWork(QuizAppDbContext context)
    {
        _context = context;
    }

    public QuizAppDbContext Context => _context;

    public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class => new GenericRepository<TEntity>(_context);

    public IGenericRepository<Quiz> QuizRepository => _quizRepository ??= new GenericRepository<Quiz>(_context);

    public IGenericRepository<Question> QuestionRepository => _questionRepository ??= new GenericRepository<Question>(_context);

    public IGenericRepository<UserQuiz> UserQuizRepository => _userQuizRepository ??= new GenericRepository<UserQuiz>(_context);

    public IGenericRepository<UserAnswer> UserAnswerRepository => _userAnswerRepository ??= new GenericRepository<UserAnswer>(_context);

    public IGenericRepository<QuizQuestion> QuizQuestionRepository => _quizQuestionRepository ??= new GenericRepository<QuizQuestion>(_context);

    public IGenericRepository<User> UserRepository => _userRepository ??= new GenericRepository<User>(_context);

    public IGenericRepository<Role> RoleRepository => _roleRepository ??= new GenericRepository<Role>(_context);

    public IGenericRepository<Answer> AnswerRepository => _answerRepository ??= new GenericRepository<Answer>(_context);


    /// <summary>
    /// Saves all changes made in the unit of work to the underlying data store.
    /// </summary>
    /// <returns>The number of objects written to the underlying data store.</returns>
    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    /// <summary>
    /// Saves all changes made in this unit of work to the underlying database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Begins a new transaction asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// Commits the current transaction asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    /// <summary>
    /// Rolls back the current transaction asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}