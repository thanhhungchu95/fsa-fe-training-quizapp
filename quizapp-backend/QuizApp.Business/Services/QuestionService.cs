using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizApp.Data;

namespace QuizApp.Business;

public class QuestionService : BaseService<Question>, IQuestionService
{
    public QuestionService(IUnitOfWork unitOfWork, ILogger<QuestionService> logger) : base(unitOfWork, logger)
    {
    }

    public override async Task<IEnumerable<Question>> GetAllAsync()
    {
        return await _unitOfWork.QuestionRepository.GetQuery()
            .Include(q => q.Answers)
            .ToListAsync();
    }

    public async Task<IEnumerable<QuizQuestionViewModel>> GetQuestionsByQuizIdAsync(Guid quizId)
    {
        return await _unitOfWork.QuizQuestionRepository.GetQuery()
            .Where(qq => qq.QuizId == quizId)
            .Include(qq => qq.Question)
            .Select(qq => new QuizQuestionViewModel
            {
                Id = qq.Question.Id,
                Content = qq.Question.Content,
                QuestionType = qq.Question.QuestionType,
                NumberOfAnswers = qq.Question.Answers.Count,
                IsActive = qq.Question.IsActive,
                Order = qq.Order
            }).OrderBy(qq => qq.Order).ToListAsync();
    }

    public async Task<List<AnswerViewModel>> GetAnswersByQuestionIdAsync(Guid id)
    {
        return await _unitOfWork.AnswerRepository.GetQuery()
            .Where(a => a.QuestionId == id)
            .Select(a => new AnswerViewModel
            {
                Id = a.Id,
                Content = a.Content,
                IsCorrect = a.IsCorrect,
                IsActive = a.IsActive,
                QuestionId = a.QuestionId
            }).ToListAsync();
    }

    public async Task<AnswerViewModel?> GetAnswersByIdAsync(Guid id)
    {
        return await _unitOfWork.AnswerRepository.GetQuery()
            .Where(a => a.Id == id)
            .Select(a => new AnswerViewModel
            {
                Id = a.Id,
                Content = a.Content,
                IsCorrect = a.IsCorrect,
                IsActive = a.IsActive,
                QuestionId = a.QuestionId
            }).FirstOrDefaultAsync();
    }

    public override async Task<Question?> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.QuestionRepository.GetQuery()
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<bool> AddAnswerToQuestionAsync(AnswerCreateViewModel answerCreateViewModel)
    {
        var question = await _unitOfWork.QuestionRepository.GetByIdAsync(answerCreateViewModel.QuestionId);

        if (question == null)
        {
            _logger.LogError("Question not found.");
            return false;
        }

        var newAnswer = new Answer
        {
            Id = Guid.NewGuid(),
            Content = answerCreateViewModel.Content,
            IsCorrect = answerCreateViewModel.IsCorrect,
            IsActive = answerCreateViewModel.IsActive,
            QuestionId = question.Id
        };

        // add the answer to the database
        _unitOfWork.AnswerRepository.Add(newAnswer);

        // save the changes
        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            _logger.LogError("Question creation failed.");
            return false;
        }

        _logger.LogInformation("Question created successfully.");

        return result > 0;
    }

    public async Task<bool> AddQuestionWithAnswerAsync(QuestionCreateViewModel questionCreateViewModel)
    {
        // create a new question
        var question = new Question
        {
            Id = Guid.NewGuid(),
            Content = questionCreateViewModel.Content,
            QuestionType = questionCreateViewModel.QuestionType,
            IsActive = questionCreateViewModel.IsActive
        };

        // add the question to the database
        _unitOfWork.QuestionRepository.Add(question);

        // create a new answer for each answer in the view model
        foreach (var answer in questionCreateViewModel.Answers)
        {
            var newAnswer = new Answer
            {
                Id = Guid.NewGuid(),
                Content = answer.Content,
                IsCorrect = answer.IsCorrect,
                IsActive = answer.IsActive,
                QuestionId = question.Id
            };

            // add the answer to the database
            _unitOfWork.AnswerRepository.Add(newAnswer);
        }

        // save the changes
        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            _logger.LogError("Question creation failed.");
            return false;
        }

        _logger.LogInformation("Question created successfully.");

        return result > 0;
    }

    public async Task<bool> UpdateQuestionWithAnswerAsync(Guid id, QuestionEditViewModel questionEditViewModel)
    {
        // get the question by ID
        var question = await _unitOfWork.QuestionRepository.GetByIdAsync(id);
        if (question == null)
        {
            _logger.LogError("Question not found.");
            return false;
        }

        // update the question properties
        question.Content = questionEditViewModel.Content;
        question.QuestionType = questionEditViewModel.QuestionType;
        question.IsActive = questionEditViewModel.IsActive;

        var existingAnswers = await _unitOfWork.AnswerRepository.GetQuery()
            .Where(a => a.QuestionId == question.Id && a.IsActive)
            .ToListAsync();

        // update the answers
        foreach (var answer in questionEditViewModel.Answers)
        {
            var existingAnswer = existingAnswers.FirstOrDefault(a => a.Id == answer.Id);
            if (existingAnswer != null)
            {
                existingAnswer.Content = answer.Content;
                existingAnswer.IsCorrect = answer.IsCorrect;
                existingAnswer.IsActive = answer.IsActive;

                _unitOfWork.AnswerRepository.Update(existingAnswer);
            }
            else
            {
                var newAnswer = new Answer
                {
                    Id = Guid.NewGuid(),
                    Content = answer.Content,
                    IsCorrect = answer.IsCorrect,
                    IsActive = answer.IsActive,
                    QuestionId = question.Id
                };

                _unitOfWork.AnswerRepository.Add(newAnswer);
            }

            // mark the existing answer as inactive if it is not in the updated answers
            foreach (var item in existingAnswers)
            {
                if (questionEditViewModel.Answers.All(a => a.Id != item.Id))
                {
                    item.IsActive = false;
                    _unitOfWork.AnswerRepository.Update(item);
                }
            }
        }

        // save the changes
        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            _logger.LogError("Question update failed.");
            return false;
        }

        _logger.LogInformation("Question updated successfully.");

        return result > 0;
    }

    public override Task<bool> DeleteAsync(Guid id)
    {
        // Delete the answers first
        var answers = _unitOfWork.AnswerRepository.GetQuery()
            .Where(a => a.QuestionId == id)
            .ToList();

        foreach (var answer in answers)
        {
            _unitOfWork.AnswerRepository.Delete(answer);
        }

        return base.DeleteAsync(id);
    }

    public async Task<bool> DeleteAnswerFromQuestionAsync(Guid answerId, Guid questionId)
    {
        var answer = await _unitOfWork.AnswerRepository.GetByIdAsync(answerId);
        if (answer == null)
        {
            _logger.LogError("Answer not found.");
            return false;
        }

        _unitOfWork.AnswerRepository.Delete(answer);

        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            _logger.LogError("Answer deletion failed.");
            return false;
        }

        _logger.LogInformation("Answer deleted successfully.");

        return result > 0;
    }
}
