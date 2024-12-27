using QuizApp.Data;

namespace QuizApp.Business
{
    /// <summary>
    /// Represents the interface for the Quiz service.
    /// </summary>
    public interface IQuizService : IBaseService<Quiz>
    {
      
        /// <summary>
        /// Prepares a quiz for a user asynchronously.
        /// </summary>
        /// <param name="prepareQuizViewModel">The view model containing the necessary information to prepare the quiz.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the prepared quiz information, or null if the quiz cannot be prepared.</returns>
        Task<QuizPrepareInfoViewModel?> PrepareQuizForUserAsync(PrepareQuizViewModel prepareQuizViewModel);

        /// <summary>
        /// Takes a quiz asynchronously based on the provided model.
        /// </summary>
        /// <param name="takeQuizViewModel">The model containing information about the quiz to be taken.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains the quiz for test view model.</returns>
        Task<QuizForTestViewModel?> TakeQuizAsync(TakeQuizViewModel takeQuizViewModel);

        /// <summary>
        /// Submits a quiz asynchronously based on the provided model.
        /// </summary>
        /// <param name="quizSubmissionViewModel">The model containing information about the quiz submission.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a boolean indicating the success of the quiz submission.</returns>
        Task<bool> SubmitQuizAsync(QuizSubmissionViewModel quizSubmissionViewModel);

        /// <summary>
        /// Gets the quiz result asynchronously based on the provided model.
        /// </summary>
        /// <param name="getQuizResultViewModel">The model containing information about the quiz result to be retrieved.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains the quiz result view model.</returns>
        Task<QuizResultViewModel?> GetQuizResultAsync(GetQuizResultViewModel getQuizResultViewModel);

        /// <summary>
        /// Updates a quiz with a question asynchronously.
        /// </summary>
        /// <param name="id">The ID of the quiz to update.</param>
        /// <param name="quizEditViewModel">The view model containing the updated quiz information.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a boolean indicating whether the update was successful.</returns>
        Task<bool> UpdateQuizWithQuestionsAsync(Guid id, QuizEditViewModel quizEditViewModel);

        /// <summary>
        /// Adds a quiz with questions asynchronously.
        /// </summary>
        /// <param name="quizCreateViewModel">The view model containing the details of the quiz to be added.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the ID of the added quiz.</returns>
        Task<bool> AddQuizWithQuestionsAsync(QuizCreateViewModel quizCreateViewModel);

        /// <summary>
        /// Deletes a question from a quiz.
        /// </summary>
        /// <param name="id">The ID of the quiz.</param>
        /// <param name="questionId">The ID of the question to delete.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the question was successfully deleted.</returns>
        Task<bool> DeleteQuestionFromQuizAsync(Guid id, Guid questionId);


        /// <summary>
        /// Adds a question to a quiz asynchronously.
        /// </summary>
        /// <param name="quizQuestionCreateViewModel">The view model containing the question details.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a boolean value indicating whether the question was successfully added to the quiz.</returns>
        Task<bool> AddQuestionToQuizAsync(QuizQuestionCreateViewModel quizQuestionCreateViewModel);

        /// <summary>
        /// Gets the average quiz score asynchronously based on the provided quiz ID.
        /// </summary>
        /// <param name="quizId">The ID of the quiz.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains the average quiz score.</returns>
        // Task<double> GetAverageQuizScoreAsync(Guid quizId);
    }
}
