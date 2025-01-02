using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Business;

namespace QuizApp.WebAPI;


/// <summary>
/// Controller for managing questions.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _questionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestionsController"/> class.
    /// </summary>
    /// <param name="questionService">The question service.</param>
    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    /// <summary>
    /// Gets all questions.
    /// </summary>
    /// <returns>A list of question view models.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<QuestionViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetQuestions()
    {
        var questions = await _questionService.GetAllAsync();

        var questionViewModels = questions.Select(question => new QuestionViewModel
        {
            Id = question.Id,
            Content = question.Content,
            QuestionType = question.QuestionType,
            NumberOfAnswers = question.Answers.Count,
            IsActive = question.IsActive
        }).ToList();

        return Ok(questionViewModels);
    }

    /// <summary>
    /// Gets all questions.
    /// </summary>
    /// <returns>A list of question view models.</returns>
    [HttpGet("getQuestionsByQuizId/{id}")]
    [ProducesResponseType(typeof(List<QuizQuestionViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetQuestionsByQuizId(Guid id)
    {
        var questions = await _questionService.GetQuestionsByQuizIdAsync(id);
        return Ok(questions);
    }

    /// <summary>
    /// Searches for questions based on the provided query.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <returns>A paginated list of questions.</returns>
    [HttpGet("search")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedResult<QuestionViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Search([FromQuery] SearchQuestionQuery query)
    {
        var questions = await _questionService.SearchAsync(query);

        return Ok(questions);
    }

    /// <summary>
    /// Retrieves the answers for a specific question by its ID.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <returns>A list of answer view models.</returns>
    [HttpGet("getAnswersByQuestionId/{id}")]
    [ProducesResponseType(typeof(List<AnswerViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAnswersByQuestionId(Guid id)
    {
        var answers = await _questionService.GetAnswersByQuestionIdAsync(id);
        return Ok(answers);
    }

    /// <summary>
    /// Retrieves the answers by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the answers.</param>
    /// <returns>An IActionResult containing the answers.</returns>
    [HttpGet("answers/{id}")]
    [ProducesResponseType(typeof(AnswerViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAnswersById(Guid id)
    {
        var answers = await _questionService.GetAnswersByIdAsync(id);
        return Ok(answers);
    }

    /// <summary>
    /// Gets a question by its ID.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <returns>The question view model.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(QuestionViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetQuestion(Guid id)
    {
        var question = await _questionService.GetByIdAsync(id);

        if (question == null)
        {
            return NotFound();
        }

        var questionViewModel = new QuestionViewModel
        {
            Id = question.Id,
            Content = question.Content,
            QuestionType = question.QuestionType,
            NumberOfAnswers = question.Answers.Count,
            IsActive = question.IsActive
        };
        return Ok(questionViewModel);
    }

    /// <summary>
    /// Creates a new question with answer.
    /// </summary>
    /// <param name="questionCreateViewModel">The question create view model.</param>
    /// <returns>A boolean indicating the success of the operation.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin, Editor")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateQuestionWithAnswer(QuestionCreateViewModel questionCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _questionService.AddQuestionWithAnswerAsync(questionCreateViewModel);

        return Ok(result);
    }

    /// <summary>
    /// Adds an answer to a question.
    /// </summary>
    /// <param name="answerCreateViewModel">The view model containing the answer details.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    /// <remarks>
    /// This method is used to add an answer to a question. It requires the user to be authenticated as an admin or editor.
    /// The answer details are provided through the <paramref name="answerCreateViewModel"/> parameter.
    /// If the model state is not valid, a bad request response is returned.
    /// The method returns an <see cref="IActionResult"/> indicating the success of the operation.
    /// </remarks>
    [HttpPost("addAnswerToQuestion")]
    [Authorize(Roles = "Admin, Editor")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> AddAnswerToQuestion(AnswerCreateViewModel answerCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _questionService.AddAnswerToQuestionAsync(answerCreateViewModel);

        return Ok(result);
    }

    /// <summary>
    /// Updates an existing question with answer.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <param name="questionEditViewModel">The question edit view model.</param>
    /// <returns>A boolean indicating the success of the operation.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, Editor")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdateQuestionWithAnswer(Guid id, QuestionEditViewModel questionEditViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _questionService.UpdateQuestionWithAnswerAsync(id, questionEditViewModel);

        return Ok(result);
    }

    /// <summary>
    /// Deletes a question by its ID.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <returns>A boolean indicating the success of the operation.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin, Editor")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteQuestion(Guid id)
    {
        var result = await _questionService.DeleteAsync(id);
        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes an answer from a specific question.
    /// </summary>
    /// <param name="questionId">The ID of the question from which the answer will be deleted.</param>
    /// <param name="answerId">The ID of the answer to be deleted.</param>
    [HttpDelete("{questionId}/answers/{answerId}/")]
    [Authorize(Roles = "Admin, Editor")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAnswerFromQuestion(Guid answerId, Guid questionId)
    {
        var result = await _questionService.DeleteAnswerFromQuestionAsync(answerId, questionId);
        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}
