using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizApp.Business;
using QuizApp.Data;

namespace QuizApp.UnitTesting;

/// <summary>
/// This class contains the unit tests for the QuizService class.
/// QuizService has constructor that takes a IUnitOfWork object as a parameter. IUnitOfWork has IGenericRepository<Quiz> property.
/// </summary>
public class TestQuizService
{
    private QuizAppDbContext _context;
    private IUnitOfWork _unitOfWork;
    private IGenericRepository<Quiz> _quizRepository;
    private QuizService _quizService;

    private ILogger<QuizService> _logger;
    [SetUp]
    public void Setup()
    {
        // Get DbContextOptions object for sql server database
        var options = new DbContextOptionsBuilder<QuizAppDbContext>()
            .UseSqlServer("Server=.;Database=QuizAppDb02;User Id=sa;Password=abcd@1234;TrustServerCertificate=true;MultipleActiveResultSets=true")
            .Options;

        _logger = new Logger<QuizService>(new LoggerFactory());

        // Create instance of DbContext
        _context = new QuizAppDbContext(options);
        _unitOfWork = new UnitOfWork(_context);
        _quizRepository = _unitOfWork.QuizRepository;
        _quizService = new QuizService(_unitOfWork, _logger);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
        _unitOfWork.Dispose();
    }

    [Test]
    public void TestGetAllAsync()
    {
        // Act
        var quizzes = _quizService.GetAllAsync().Result;

        // Assert
        Assert.That(quizzes.Count(), Is.EqualTo(3));
    }

    [Test]
    public void TestGetByIdAsync()
    {
        var quizId = Guid.Parse("dceca50f-84fe-4442-a816-3ec9a6f8b602");
        // Act
        var quiz = _quizService.GetByIdAsync(quizId).Result;

        // Assert
        Assert.That(quiz?.Id, Is.EqualTo(quizId));
        Assert.That(quiz?.Title, Is.EqualTo("Capitals of Country"));
    }

    [Test]
    public void TestAddAsync()
    {
        // Arrange
        var quiz = new Quiz
        {
            Id = Guid.NewGuid(),
            Title = "Test Quiz",
            Description = "Test Quiz Description",
            Duration = 60,
        };

        // Act
        var result = _quizService.AddAsync(quiz).Result;

        // Get the added quiz
        var addedQuiz = _quizRepository.GetByIdAsync(quiz.Id).Result;

        // Assert
        Assert.That(addedQuiz?.Id, Is.EqualTo(quiz.Id));
        Assert.That(addedQuiz.Title, Is.EqualTo(quiz.Title));
        Assert.That(addedQuiz.Description, Is.EqualTo(quiz.Description));
        Assert.That(addedQuiz.Duration, Is.EqualTo(quiz.Duration));

        // Clean up
        _quizRepository.Delete(addedQuiz);
        _unitOfWork.SaveChanges();
    }
}