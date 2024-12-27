using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizApp.Data;

namespace QuizApp.Business
{
    /// <summary>
    /// Represents a service for managing quizzes.
    /// </summary>
    public class QuizService : BaseService<Quiz>, IQuizService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuizService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public QuizService(IUnitOfWork unitOfWork, ILogger<QuizService> logger) : base(unitOfWork, logger)
        {
        }

        public override async Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return await _unitOfWork.QuizRepository.GetQuery()
                .Include(q => q.QuizQuestions)
                .ToListAsync();
        }

        public override async Task<Quiz?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.QuizRepository.GetQuery()
                .Include(q => q.QuizQuestions)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        /// <summary>
        /// Gets the result of a quiz asynchronously.
        /// </summary>
        /// <param name="model">The model containing the quiz result parameters.</param>
        /// <returns>The view model representing the quiz result.</returns>
        public Task<QuizResultViewModel?> GetQuizResultAsync(GetQuizResultViewModel model)
        {
            var quizResultViewModel = _unitOfWork.UserQuizRepository.GetQuery()
                .Where(uq => uq.UserId == model.UserId && uq.QuizId == model.QuizId)
                .Select(uq => new QuizResultViewModel
                {
                    QuizId = uq.QuizId,
                    UserId = uq.UserId,
                    CorrectAnswers = uq.UserAnswers.Count(ua => ua.IsCorrect),
                    TotalQuestions = uq.UserAnswers.Count,
                    Score = uq.UserAnswers.Count(ua => ua.IsCorrect) / uq.UserAnswers.Count
                })
                .FirstOrDefaultAsync();

            _logger.LogInformation("Quiz result retrieved successfully.");

            return quizResultViewModel;
        }

        /// <summary>
        /// Submits a quiz asynchronously.
        /// </summary>
        /// <param name="model">The model containing the quiz submission data.</param>
        /// <returns>A boolean indicating whether the quiz submission was successful.</returns>
        public async Task<bool> SubmitQuizAsync(QuizSubmissionViewModel model)
        {
            // Update the user quiz
            var userQuiz = await _unitOfWork.UserQuizRepository.GetQuery()
                .Where(uq => uq.UserId == model.UserId && uq.QuizId == model.QuizId && uq.QuizCode == model.QuizCode)
                .FirstOrDefaultAsync();

            if (userQuiz == null)
            {
                _logger.LogError("Quiz submission failed.");
                return false;
            }

            userQuiz.FinishedAt = DateTime.Now;

            var answersInQuiz = await _unitOfWork.QuizQuestionRepository.GetQuery()
                .Where(qq => qq.QuizId == model.QuizId)
                .Select(qq => qq.Question.Answers)
                .ToListAsync();

            // Update the user answers
            foreach (var userAnswerViewModel in model.UserAnswers)
            {
                var userAnswer = new UserAnswer
                {
                    Id = Guid.NewGuid(),
                    UserQuizId = userQuiz.Id,
                    QuestionId = userAnswerViewModel.QuestionId,
                    AnswerId = userAnswerViewModel.AnswerId,
                    IsCorrect = answersInQuiz
                        .Any(a => a.Any(answer => answer.Id == userAnswerViewModel.AnswerId && answer.IsCorrect))
                };

                _unitOfWork.UserAnswerRepository.Add(userAnswer);
            }

            var result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
            {
                _logger.LogError("Quiz submission failed.");
                return false;
            }

            _logger.LogInformation("Quiz submitted successfully.");

            // Save changes
            return result > 0;
        }

        /// <summary>
        /// Prepares a quiz for a user asynchronously.
        /// </summary>
        /// <param name="prepareQuizViewModel">The view model containing the necessary information to prepare the quiz.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the prepared quiz information, or null if the quiz cannot be prepared.</returns>
        public async Task<QuizPrepareInfoViewModel?> PrepareQuizForUserAsync(PrepareQuizViewModel prepareQuizViewModel)
        {
            if (!prepareQuizViewModel.UserId.HasValue)
            {
                _logger.LogError("User ID is required.");
                throw new ArgumentException("User ID is required.");
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(prepareQuizViewModel.UserId.Value) ?? throw new ArgumentException("User not found.");

            Quiz quiz;
            string quizCode = Guid.NewGuid().ToString();
            if (prepareQuizViewModel.QuizCode.HasValue)
            {
                var existingUserQuiz = await _unitOfWork.UserQuizRepository.GetQuery()
                    .Where(uq => uq.QuizCode == prepareQuizViewModel.QuizCode.Value.ToString())
                    .FirstOrDefaultAsync();

                if (existingUserQuiz != null)
                {
                    quiz = await _unitOfWork.QuizRepository.GetByIdAsync(existingUserQuiz.QuizId) ?? throw new ArgumentException("Quiz not found.");
                    quizCode = existingUserQuiz.QuizCode ?? string.Empty;
                }
                else
                {
                    quiz = await PrepareNewQuizAsync(prepareQuizViewModel, quizCode);
                }
            }
            else
            {
                quiz = await PrepareNewQuizAsync(prepareQuizViewModel, quizCode);
            }

            var quizPrepareViewModel = new QuizPrepareInfoViewModel
            {
                Id = quiz.Id,
                QuizCode = quizCode,
                Title = quiz.Title ?? string.Empty,
                Description = quiz.Description ?? string.Empty,
                Duration = quiz.Duration,
                ThumbnailUrl = quiz.ThumbnailUrl,
                User = new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email ?? string.Empty,
                    UserName = user.UserName ?? string.Empty,
                    PhoneNumber = user.PhoneNumber ?? string.Empty,
                    Avatar = user.Avatar,
                }
            };

            _logger.LogInformation("Quiz prepared successfully.");

            return quizPrepareViewModel;
        }

        private async Task<Quiz> PrepareNewQuizAsync(PrepareQuizViewModel prepareQuizViewModel, string quizCode)
        {
            if (!prepareQuizViewModel.QuizId.HasValue)
            {
                _logger.LogError("Quiz ID is required.");
                throw new ArgumentException("Quiz ID is required.");
            }

            var quiz = await _unitOfWork.QuizRepository.GetByIdAsync(prepareQuizViewModel.QuizId.Value) ?? throw new ArgumentException("Quiz not found.");

            var userQuiz = new UserQuiz
            {
                UserId = prepareQuizViewModel.UserId.Value,
                QuizId = prepareQuizViewModel.QuizId.Value,
                QuizCode = quizCode,
                StartedAt = DateTime.Now
            };

            _unitOfWork.UserQuizRepository.Add(userQuiz);

            var result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
            {
                _logger.LogError("Quiz preparation failed.");
                throw new Exception("Quiz preparation failed.");
            }

            return quiz;
        }

        /// <summary>
        /// Takes a quiz asynchronously.
        /// </summary>
        /// <param name="model">The model containing the quiz data.</param>
        /// <returns>The view model representing the quiz for testing.</returns>
        public async Task<QuizForTestViewModel?> TakeQuizAsync(TakeQuizViewModel model)
        {
            var quiz = await _unitOfWork.UserQuizRepository.GetQuery()
                .Where(uq => uq.UserId == model.UserId && uq.QuizId == model.QuizId && uq.QuizCode == model.QuizCode)
                .FirstOrDefaultAsync();

            if (quiz == null)
            {
                _logger.LogError("Quiz not found.");
                throw new ArgumentException("Quiz not found.");
            }

            var quizForTestViewModel = await _unitOfWork.QuizRepository.GetQuery()
                .Where(q => q.Id == model.QuizId)
                .Select(q => new QuizForTestViewModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    QuizCode = quiz.QuizCode,
                    Duration = q.Duration,
                    Questions = q.QuizQuestions.Select(qq => new QuestionForTestViewModel
                    {
                        Id = qq.Question.Id,
                        Content = qq.Question.Content,
                        Answers = qq.Question.Answers.Select(a => new AnswerForTestViewModel
                        {
                            Id = a.Id,
                            Content = a.Content
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (quizForTestViewModel == null)
            {
                _logger.LogError("Quiz not found.");
                throw new ArgumentException("Quiz not found.");
            }

            _logger.LogInformation("Quiz retrieved successfully.");

            return quizForTestViewModel;
        }

        /// <summary>
        /// Adds a quiz with questions asynchronously.
        /// </summary>
        /// <param name="quizCreateViewModel">The view model containing the quiz details.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the ID of the added quiz.</returns>
        public async Task<bool> AddQuizWithQuestionsAsync(QuizCreateViewModel quizCreateViewModel)
        {
            // Create a new quiz
            var quiz = new Quiz
            {
                Title = quizCreateViewModel.Title,
                Description = quizCreateViewModel.Description,
                Duration = quizCreateViewModel.Duration,
                ThumbnailUrl = quizCreateViewModel.ThumbnailUrl,
                IsActive = quizCreateViewModel.IsActive
            };

            _unitOfWork.QuizRepository.Add(quiz);

            // Add questions to the quiz

            if (quizCreateViewModel.QuestionIdWithOrders.Any())
            {
                foreach (var questionId in quizCreateViewModel.QuestionIdWithOrders)
                {
                    var quizQuestion = new QuizQuestion
                    {
                        QuizId = quiz.Id,
                        QuestionId = questionId.QuestionId,
                        Order = questionId.Order
                    };

                    _unitOfWork.QuizQuestionRepository.Add(quizQuestion);
                }
            }

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Updates a quiz with a question asynchronously.
        /// </summary>
        /// <param name="id">The ID of the quiz to update.</param>
        /// <param name="quizEditViewModel">The view model containing the updated quiz data.</param>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the update was successful or not.</returns>
        public async Task<bool> UpdateQuizWithQuestionsAsync(Guid id, QuizEditViewModel quizEditViewModel)
        {
            // Get the quiz
            var quiz = _unitOfWork.QuizRepository.GetQuery()
                .Where(q => q.Id == id)
                .Include(q => q.QuizQuestions)
                .FirstOrDefault();

            if (quiz == null)
            {
                _logger.LogError("Quiz not found.");
                return false;
            }

            // Update the quiz
            quiz.Title = quizEditViewModel.Title;
            quiz.Description = quizEditViewModel.Description;
            quiz.Duration = quizEditViewModel.Duration;
            quiz.ThumbnailUrl = quizEditViewModel.ThumbnailUrl;
            quiz.IsActive = quizEditViewModel.IsActive;

            // Update the questions
            var existingQuestionIds = quiz.QuizQuestions.Where(x => x.QuizId == id).Select(qq => qq.QuestionId).ToList();

            var newQuestionIds = quizEditViewModel.QuestionIdWithOrders.Select(qq => qq.QuestionId).ToList();

            var questionsToRemove = existingQuestionIds.Except(newQuestionIds).ToList();

            var questionsToAdd = newQuestionIds.Except(existingQuestionIds).ToList();

            foreach (var questionId in questionsToRemove)
            {
                var quizQuestion = quiz.QuizQuestions.FirstOrDefault(qq => qq.QuestionId == questionId);

                if (quizQuestion != null)
                {
                    _unitOfWork.QuizQuestionRepository.Delete(quizQuestion);
                }
            }

            foreach (var questionId in questionsToAdd)
            {
                var quizQuestion = new QuizQuestion
                {
                    QuizId = quiz.Id,
                    QuestionId = questionId,
                    Order = quizEditViewModel.QuestionIdWithOrders.FirstOrDefault(qq => qq.QuestionId == questionId)?.Order ?? 0
                };

                _unitOfWork.QuizQuestionRepository.Add(quizQuestion);
            }

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Deletes a question from a quiz.
        /// </summary>
        /// <param name="id">The ID of the quiz.</param>
        /// <param name="questionId">The ID of the question to delete.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the question was successfully deleted.</returns>
        public async Task<bool> DeleteQuestionFromQuizAsync(Guid id, Guid questionId)
        {
            var quizQuestion = await _unitOfWork.QuizQuestionRepository.GetQuery()
                .Where(qq => qq.QuizId == id && qq.QuestionId == questionId)
                .FirstOrDefaultAsync();

            if (quizQuestion == null)
            {
                _logger.LogError("Quiz question not found.");
                return false;
            }

            _unitOfWork.QuizQuestionRepository.Delete(quizQuestion);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddQuestionToQuizAsync(QuizQuestionCreateViewModel quizCreateViewModel)
        {
            var quiz = await _unitOfWork.QuizRepository.GetByIdAsync(quizCreateViewModel.QuizId);

            if (quiz == null)
            {
                _logger.LogError("Quiz not found.");
                return false;
            }

            var question = await _unitOfWork.QuestionRepository.GetByIdAsync(quizCreateViewModel.QuestionId);

            if (question == null)
            {
                _logger.LogError("Question not found.");
                return false;
            }

            var quizQuestion = new QuizQuestion
            {
                QuizId = quiz.Id,
                QuestionId = question.Id,
                Order = quizCreateViewModel.Order
            };

            _unitOfWork.QuizQuestionRepository.Add(quizQuestion);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
