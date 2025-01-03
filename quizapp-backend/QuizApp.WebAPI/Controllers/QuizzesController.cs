using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Business;

namespace QuizApp.WebAPI;


/// <summary>
/// Controller for managing quizzes.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class QuizzesController : ControllerBase
{
    private readonly IQuizService _quizService;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuizzesController"/> class.
    /// </summary>
    /// <param name="quizService">The quiz service.</param>
    public QuizzesController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    /// <summary>
    /// Gets all quizzes.
    /// </summary>
    /// <returns>A list of quiz view models.</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<QuizViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetQuizzes()
    {
        var questions = await _quizService.GetAllAsync();

        var questionViewModels = questions.Select(question => new QuizViewModel
        {
            Id = question.Id,
            Title = question.Title,
            Description = question.Description,
            Duration = question.Duration,
            ThumbnailUrl = question.ThumbnailUrl,
            NumberOfQuestions = question.QuizQuestions.Count,
            IsActive = question.IsActive
        }).ToList();

        return Ok(questionViewModels);
    }

    /// <summary>
    /// Searches for quizzes based on the provided query.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <returns>A paginated list of quizzes.</returns>
    [HttpGet("search")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedResult<QuizViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Search([FromQuery] SearchQuizQuery query)
    {
        var quizzes = await _quizService.SearchAsync(query);

        return Ok(quizzes);
    }

    /// <summary>
    /// Gets a quiz by ID.
    /// </summary>
    /// <param name="id">The ID of the quiz.</param>
    /// <returns>The quiz view model.</returns>
    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin, Editor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(QuizViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetQuiz(Guid id)
    {
        var quiz = await _quizService.GetByIdAsync(id);

        if (quiz == null)
        {
            return NotFound();
        }

        var quizViewModel = new QuizViewModel
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Description = quiz.Description,
            Duration = quiz.Duration,
            ThumbnailUrl = quiz.ThumbnailUrl,
            NumberOfQuestions = quiz.QuizQuestions.Count,
            IsActive = quiz.IsActive
        };
        return Ok(quizViewModel);
    }

    /// <summary>
    /// Creates a new quiz with questions.
    /// </summary>
    /// <param name="quizCreateViewModel">The quiz create view model.</param>
    /// <returns>A boolean indicating if the quiz was created successfully.</returns>
    [HttpPost]
    //[Authorize(Roles = "Admin, Editor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateQuizWithQuestions(QuizCreateViewModel quizCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _quizService.AddQuizWithQuestionsAsync(quizCreateViewModel);

        return Ok(result);
    }

    /// <summary>
    /// Creates a new quiz with questions.
    /// </summary>
    /// <param name="quizQuestionCreateViewModel">The quiz create view model.</param>
    /// <returns>A boolean indicating if the quiz was created successfully.</returns>
    [HttpPost("AddQuestionToQuiz")]
    //[Authorize(Roles = "Admin, Editor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> AddQuestionToQuiz(QuizQuestionCreateViewModel quizQuestionCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _quizService.AddQuestionToQuizAsync(quizQuestionCreateViewModel);

        return Ok(result);
    }

    /// <summary>
    /// Updates an existing quiz with questions.
    /// </summary>
    /// <param name="id">The ID of the quiz.</param>
    /// <param name="quizEditViewModel">The quiz edit view model.</param>
    /// <returns>A boolean indicating if the quiz was updated successfully.</returns>
    [HttpPut("{id}")]
    // [Authorize(Roles = "Admin, Editor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdateQuizWithQuestions(Guid id, QuizEditViewModel quizEditViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _quizService.UpdateQuizWithQuestionsAsync(id, quizEditViewModel);

        return Ok(result);
    }

    /// <summary>
    /// Deletes a quiz by ID.
    /// </summary>
    /// <param name="id">The ID of the quiz.</param>
    /// <returns>A boolean indicating if the quiz was deleted successfully.</returns>
    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin, Editor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteQuiz(Guid id)
    {
        var result = await _quizService.DeleteAsync(id);
        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a question from a quiz.
    /// </summary>
    /// <param name="id">The ID of the quiz.</param>
    /// <param name="questionId">The ID of the question to delete.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    /// <remarks>
    /// This method is used to delete a specific question from a quiz. Only users with the "Admin" or "Editor" role are authorized to perform this action.
    /// If the deletion is successful, the method returns a 200 OK status code. If the provided IDs are invalid or the deletion fails, a 400 Bad Request status code is returned.
    /// </remarks>
    [HttpDelete("{id}/questions/{questionId}")]
    //[Authorize(Roles = "Admin, Editor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteQuestionFromQuiz(Guid id, Guid questionId)
    {
        var result = await _quizService.DeleteQuestionFromQuizAsync(id, questionId);
        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    /// <summary>
    /// Prepares a quiz for a user.
    /// </summary>
    /// <param name="prepareQuizViewModel">The view model containing the necessary information to prepare the quiz.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    /// <remarks>
    /// This method is used to prepare a quiz for a user. It takes a <paramref name="prepareQuizViewModel"/> as input,
    /// which contains the necessary information to prepare the quiz. If the model state is not valid, a bad request
    /// response is returned. Otherwise, the quiz is prepared using the <see cref="_quizService"/> and the result is
    /// returned as an OK response.
    /// </remarks>
    [HttpPost("PrepareQuizForUser")]
    //[Authorize(Roles = "Admin, Editor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(QuizPrepareInfoViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PrepareQuizForUser(PrepareQuizViewModel prepareQuizViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _quizService.PrepareQuizForUserAsync(prepareQuizViewModel);

        return Ok(result);
    }


    /// <summary>
    /// Takes a quiz based on the provided quiz view model.
    /// </summary>
    /// <param name="takeQuizViewModel">The quiz view model containing the necessary information to take the quiz.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the quiz taking operation.</returns>
    [HttpPost("takeQuiz")]
    //[Authorize(Roles = "Admin, Editor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(QuizForTestViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> TakeQuiz(TakeQuizViewModel takeQuizViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _quizService.TakeQuizAsync(takeQuizViewModel);

        return Ok(result);
    }

    /// <summary>
    /// Submits a quiz for evaluation.
    /// </summary>
    /// <param name="quizSubmissionViewModel">The view model containing the quiz submission data.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    /// <remarks>
    /// This method is used to submit a quiz for evaluation. It requires the user to be authenticated.
    /// If the quiz submission data is invalid, a bad request response will be returned.
    /// If an error occurs during the quiz submission process, a server error response will be returned.
    /// </remarks>
    [HttpPost("submitQuiz")]
    //[Authorize(Roles = "Admin, Editor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SubmitQuiz(QuizSubmissionViewModel quizSubmissionViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _quizService.SubmitQuizAsync(quizSubmissionViewModel);

        return Ok(result);
    }
}
