using QuizApp.Data;

namespace QuizApp.Business
{
    /// <summary>
    /// Represents the interface for the Question service.
    /// </summary>
    public interface IQuestionService : IBaseService<Question>
    {
        Task<bool> AddAnswerToQuestionAsync(AnswerCreateViewModel answerCreateViewModel);

        /// <summary>
        /// Adds a question with its answer asynchronously.
        /// </summary>
        /// <param name="questionCreateViewModel">The view model containing the question and answer details.</param>
        /// <returns>The ID of the added question.</returns>
        Task<bool> AddQuestionWithAnswerAsync(QuestionCreateViewModel questionCreateViewModel);


        /// <summary>
        /// Retrieves answers by question ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the question.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<List<AnswerViewModel>> GetAnswersByQuestionIdAsync(Guid id);


        /// <summary>
        /// Retrieves the answer view model by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the answer.</param>
        /// <returns>The answer view model if found, otherwise null.</returns>
        Task<AnswerViewModel?> GetAnswersByIdAsync(Guid id);

        /// <summary>
        /// Retrieves a collection of question view models based on the specified quiz ID.
        /// </summary>
        /// <param name="quizId">The ID of the quiz.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of question view models.</returns>
        Task<IEnumerable<QuizQuestionViewModel>> GetQuestionsByQuizIdAsync(Guid quizId);

        /// <summary>
        /// Updates a question with the provided answer asynchronously.
        /// </summary>
        /// <param name="id">The ID of the question to update.</param>
        /// <param name="questionEditViewModel">The view model containing the updated question and answer.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the number of affected records.</returns>
        Task<bool> UpdateQuestionWithAnswerAsync(Guid id, QuestionEditViewModel questionEditViewModel);


        /// <summary>
        /// Deletes an answer from a question asynchronously.
        /// </summary>
        /// <param name="answerId">The ID of the answer to delete.</param>
        /// <param name="questionId">The ID of the question from which to delete the answer.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteAnswerFromQuestionAsync(Guid answerId, Guid questionId);

        /// <summary>
        /// Searches for questions asynchronously based on the provided search criteria.
        /// </summary>
        /// <param name="request">The search query containing filter and pagination parameters.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a paginated result of question view models matching the search criteria.</returns>
        Task<PaginatedResult<QuestionViewModel>> SearchAsync(SearchQuestionQuery request);
    }
}