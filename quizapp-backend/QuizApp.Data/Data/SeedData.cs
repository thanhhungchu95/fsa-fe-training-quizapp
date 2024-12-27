using Microsoft.AspNetCore.Identity;

namespace QuizApp.Data;

public static class SeedData
{
    public static void Initialize(QuizAppDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        // Seed Users and Roles - Optional
        var admin = new User { Id = Guid.NewGuid(), UserName = "admin", Email = "admin@domain.com", FirstName = "Admin", LastName = "User", PhoneNumber = "1234567890", DateOfBirth = DateTime.Now.AddDays(-3650), Avatar = "avatar-1.png", IsActive = true };
        var editor = new User { Id = Guid.NewGuid(), UserName = "editor", Email = "editor@domain.com", FirstName = "Editor", LastName = "User", PhoneNumber = "1234567890", DateOfBirth = DateTime.Now.AddDays(-4650), Avatar = "avatar-2.png", IsActive = true };
        var cong = new User { Id = Guid.NewGuid(), UserName = "congdinh", Email = "cong@domain.com", FirstName = "Cong", LastName = "Dinh", PhoneNumber = "1234567890", DateOfBirth = DateTime.Now.AddDays(-5650), Avatar = "avatar-3.png", IsActive = true };
        var van = new User { Id = Guid.NewGuid(), UserName = "vannguyen", Email = "van@domain.com", FirstName = "Van", LastName = "Nguyen", PhoneNumber = "1234567890", DateOfBirth = DateTime.Now.AddDays(-6650), Avatar = "avatar-4.png", IsActive = true };
        var quynh = new User { Id = Guid.NewGuid(), UserName = "quynhdinh", Email = "quynh@domain.com", FirstName = "Quynh", LastName = "Dinh", PhoneNumber = "1234567890", DateOfBirth = DateTime.Now.AddDays(-7650), Avatar = "avatar-5.png", IsActive = true };

        if (!context.Users.Any())
        {
            var roles = new List<Role>
            {
                new Role { Id= Guid.NewGuid(), Name = "Admin", Description = "Administrator", IsActive = true },
                new Role { Id= Guid.NewGuid(), Name = "Editor", Description = "Editor", IsActive = true },
                new Role { Id= Guid.NewGuid(), Name = "User", Description = "User", IsActive = true }
            };

            foreach (var role in roles)
            {
                roleManager.CreateAsync(role).Wait();
            }

            userManager.CreateAsync(admin, "P@ssw0rd").Wait();
            userManager.AddToRoleAsync(admin, "Admin").Wait();

            userManager.CreateAsync(editor, "P@ssw0rd").Wait();
            userManager.AddToRoleAsync(editor, "Editor").Wait();

            userManager.CreateAsync(cong, "P@ssw0rd").Wait();
            userManager.AddToRoleAsync(cong, "User").Wait();

            userManager.CreateAsync(van, "P@ssw0rd").Wait();
            userManager.AddToRoleAsync(van, "User").Wait();

            userManager.CreateAsync(quynh, "P@ssw0rd").Wait();
            userManager.AddToRoleAsync(quynh, "User").Wait();
        }

        // Seed Common entities
        // Seed Questions
        List<Question> questions = [
            new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "What is the capital of Viet Nam?",
                    QuestionType = QuestionType.SingleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "London", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Ha Noi", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Berlin", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Madrid", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "What is the capital of France?",
                    QuestionType = QuestionType.SingleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "London", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Paris", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Berlin", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Madrid", IsCorrect = false, IsActive = true },
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "What is the capital of South Korea?",
                    QuestionType = QuestionType.SingleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "London", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Seoul", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Berlin", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Madrid", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "What is the capital of Germany?",
                    QuestionType = QuestionType.SingleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "London", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Paris", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Berlin", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Madrid", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "What is the capital of the United Kingdom?",
                    QuestionType = QuestionType.SingleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "London", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Paris", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Berlin", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Madrid", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "What is the capital of Italy?",
                    QuestionType = QuestionType.SingleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "London", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Paris", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Rome", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Madrid", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "What is the capital of Portugal?",
                    QuestionType = QuestionType.SingleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Lisbon", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Paris", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Berlin", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Madrid", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "What is the capital of the Japan?",
                    QuestionType = QuestionType.SingleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "London", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Tokyo", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Berlin", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Madrid", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "What is the capital of Belgium?",
                    QuestionType = QuestionType.SingleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "London", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Brussels", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Berlin", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Madrid", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "What is the capital of Switzerland?",
                    QuestionType = QuestionType.SingleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "London", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Paris", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Berlin", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Bern", IsCorrect = true, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Where is Viet Nam?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Asia", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Europe", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Africa", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "America", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Where is France?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Asia", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Europe", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Africa", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "America", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Where is Japan?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Asia", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Europe", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Africa", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "America", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Where is Germany?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Asia", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Europe", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Africa", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "America", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Where is Australia?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Asia", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Europe", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Africa", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Australia", IsCorrect = true, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Where is China?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Asia", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Europe", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Africa", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "America", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Where is the United States?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Asia", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Europe", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Africa", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "America", IsCorrect = true, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Where is Brazil?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Asia", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Europe", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Africa", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "America", IsCorrect = true, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Where is Russia?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Asia", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Europe", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Africa", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "America", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Where is Canada?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Asia", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Europe", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Africa", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "America", IsCorrect = true, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Who is the inventor of the telephone?",
                    IsActive = true,
                    QuestionType = QuestionType.MultipleChoice,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Thomas Edison", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Alexander Graham Bell", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Nikola Tesla", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Albert Einstein", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Who is the inventor of the light bulb?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Thomas Edison", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Alexander Graham Bell", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Nikola Tesla", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Albert Einstein", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Who is the inventor of the theory of relativity?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Thomas Edison", IsCorrect = false , IsActive = true},
                        new Answer { Id = Guid.NewGuid(), Content = "Alexander Graham Bell", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Nikola Tesla", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Albert Einstein", IsCorrect = true, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Who is the inventor of the alternating current?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Thomas Edison", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Alexander Graham Bell", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Nikola Tesla", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Albert Einstein", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Who is the inventor of the ATM?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Thomas Edison", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Alexander Graham Bell", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "John Shepherd-Barron", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Albert Einstein", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Who is the inventor of the World Wide Web?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Tim Berners-Lee", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Alexander Graham Bell", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Nikola Tesla", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Albert Einstein", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Who is the inventor of the computer?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Charles Babbage", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Alexander Graham Bell", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Nikola Tesla", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Albert Einstein", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Who is the inventor of the airplane?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Charles Babbage", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Alexander Graham Bell", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Wright brothers", IsCorrect = true, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Albert Einstein", IsCorrect = false, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Who is the inventor of the washing machine?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Charles Babbage", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Alexander Graham Bell", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Wright brothers", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "James King", IsCorrect = true, IsActive = true }
                    }
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Content = "Who is the inventor of the bicycle?",
                    QuestionType = QuestionType.MultipleChoice,
                    IsActive = true,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = Guid.NewGuid(), Content = "Charles Babbage", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Alexander Graham Bell", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Wright brothers", IsCorrect = false, IsActive = true },
                        new Answer { Id = Guid.NewGuid(), Content = "Kirkpatrick Macmillan", IsCorrect = true, IsActive = true }
                    }
                },
            ];

        if (!context.Questions.Any())
        {
            context.Questions.AddRange(questions);
            context.SaveChanges();
        }

        // Seed Quizzes
        List<Quiz> quizzes = [
            new Quiz
                {
                    Id = Guid.NewGuid(),
                    Title = "Capitals of Country",
                    Description = "Test your knowledge of country capitals",
                    Duration = 15,
                    ThumbnailUrl = "map.jpeg",
                    IsActive = true
                },
                new Quiz
                {
                    Id = Guid.NewGuid(),
                    Title = "Countries of the World",
                    Description = "Test your knowledge of countries",
                    Duration = 15,
                    ThumbnailUrl = "capitals.jpeg",
                    IsActive = true
                },
                new Quiz
                {
                    Id = Guid.NewGuid(),
                    Title = "Inventors and Inventions",
                    Description = "Test your knowledge of inventors and their inventions",
                    Duration = 20,
                    ThumbnailUrl = "inventions.webp",
                    IsActive = true
                }
        ];

        if (!context.Quizzes.Any())
        {
            context.Quizzes.AddRange(quizzes);
            context.SaveChanges();
        }

        // Seed QuizQuestions
        List<QuizQuestion> quizQuestions = [
            new QuizQuestion { QuizId = quizzes[0].Id, QuestionId = questions[0].Id, Order = 1 },
            new QuizQuestion { QuizId = quizzes[0].Id, QuestionId = questions[1].Id, Order = 2 },
            new QuizQuestion { QuizId = quizzes[0].Id, QuestionId = questions[2].Id, Order = 3 },
            new QuizQuestion { QuizId = quizzes[0].Id, QuestionId = questions[3].Id, Order = 4 },
            new QuizQuestion { QuizId = quizzes[0].Id, QuestionId = questions[4].Id, Order = 5 },
            new QuizQuestion { QuizId = quizzes[0].Id, QuestionId = questions[5].Id, Order = 6 },
            new QuizQuestion { QuizId = quizzes[0].Id, QuestionId = questions[6].Id, Order = 7 },
            new QuizQuestion { QuizId = quizzes[0].Id, QuestionId = questions[7].Id, Order = 8 },
            new QuizQuestion { QuizId = quizzes[0].Id, QuestionId = questions[8].Id, Order = 9 },
            new QuizQuestion { QuizId = quizzes[0].Id, QuestionId = questions[9].Id, Order = 10 },
            new QuizQuestion { QuizId = quizzes[1].Id, QuestionId = questions[10].Id, Order = 1 },
            new QuizQuestion { QuizId = quizzes[1].Id, QuestionId = questions[11].Id, Order = 2 },
            new QuizQuestion { QuizId = quizzes[1].Id, QuestionId = questions[12].Id, Order = 3 },
            new QuizQuestion { QuizId = quizzes[1].Id, QuestionId = questions[13].Id, Order = 4 },
            new QuizQuestion { QuizId = quizzes[1].Id, QuestionId = questions[14].Id, Order = 5 },
            new QuizQuestion { QuizId = quizzes[1].Id, QuestionId = questions[15].Id, Order = 6 },
            new QuizQuestion { QuizId = quizzes[1].Id, QuestionId = questions[16].Id, Order = 7 },
            new QuizQuestion { QuizId = quizzes[1].Id, QuestionId = questions[17].Id, Order = 8 },
            new QuizQuestion { QuizId = quizzes[1].Id, QuestionId = questions[18].Id, Order = 9 },
            new QuizQuestion { QuizId = quizzes[1].Id, QuestionId = questions[19].Id, Order = 10 },
            new QuizQuestion { QuizId = quizzes[2].Id, QuestionId = questions[20].Id, Order = 1 },
            new QuizQuestion { QuizId = quizzes[2].Id, QuestionId = questions[21].Id, Order = 2 },
            new QuizQuestion { QuizId = quizzes[2].Id, QuestionId = questions[22].Id, Order = 3 },
            new QuizQuestion { QuizId = quizzes[2].Id, QuestionId = questions[23].Id, Order = 4 },
            new QuizQuestion { QuizId = quizzes[2].Id, QuestionId = questions[24].Id, Order = 5 },
            new QuizQuestion { QuizId = quizzes[2].Id, QuestionId = questions[25].Id, Order = 6 },
            new QuizQuestion { QuizId = quizzes[2].Id, QuestionId = questions[26].Id, Order = 7 },
            new QuizQuestion { QuizId = quizzes[2].Id, QuestionId = questions[27].Id, Order = 8 },
            new QuizQuestion { QuizId = quizzes[2].Id, QuestionId = questions[28].Id, Order = 9 },
            new QuizQuestion { QuizId = quizzes[2].Id, QuestionId = questions[29].Id, Order = 10 },
        ];

        if (!context.QuizQuestions.Any())
        {
            context.QuizQuestions.AddRange(quizQuestions);
            context.SaveChanges();
        }

        // Seed UserQuizzes
        List<UserQuiz> userQuizzes = [
            new UserQuiz
                {
                    UserId = cong.Id,
                    QuizId = quizzes[0].Id,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now,
                    UserAnswers = new List<UserAnswer>
                    {
                        new UserAnswer { QuestionId = questions[0].Id, AnswerId = questions[0].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[1].Id, AnswerId = questions[1].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[2].Id, AnswerId = questions[2].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[3].Id, AnswerId = questions[3].Answers.ToList()[2].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[4].Id, AnswerId = questions[4].Answers.ToList()[0].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[5].Id, AnswerId = questions[5].Answers.ToList()[2].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[6].Id, AnswerId = questions[6].Answers.ToList()[0].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[7].Id, AnswerId = questions[7].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[8].Id, AnswerId = questions[8].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[9].Id, AnswerId = questions[9].Answers.ToList()[0].Id, IsCorrect = true }
                    }
                },
                new UserQuiz
                {
                    UserId = cong.Id,
                    QuizId = quizzes[1].Id,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now,
                    UserAnswers = new List<UserAnswer>
                    {
                        new UserAnswer { QuestionId = questions[10].Id, AnswerId = questions[10].Answers.ToList()[0].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[11].Id, AnswerId = questions[11].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[12].Id, AnswerId = questions[12].Answers.ToList()[0].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[13].Id, AnswerId = questions[13].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[14].Id, AnswerId = questions[14].Answers.ToList()[0].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[15].Id, AnswerId = questions[15].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[16].Id, AnswerId = questions[16].Answers.ToList()[0].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[17].Id, AnswerId = questions[17].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[18].Id, AnswerId = questions[18].Answers.ToList()[0].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[19].Id, AnswerId = questions[19].Answers.ToList()[3].Id, IsCorrect = true }
                    }
                },
                new UserQuiz
                {
                    UserId = cong.Id,
                    QuizId = quizzes[2].Id,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now,
                    UserAnswers = new List<UserAnswer>
                    {
                        new UserAnswer { QuestionId = questions[20].Id, AnswerId = questions[20].Answers.ToList()[0].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[21].Id, AnswerId = questions[21].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[22].Id, AnswerId = questions[22].Answers.ToList()[0].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[23].Id, AnswerId = questions[23].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[24].Id, AnswerId = questions[24].Answers.ToList()[0].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[25].Id, AnswerId = questions[25].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[26].Id, AnswerId = questions[26].Answers.ToList()[0].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[27].Id, AnswerId = questions[27].Answers.ToList()[1].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[28].Id, AnswerId = questions[28].Answers.ToList()[0].Id, IsCorrect = true },
                        new UserAnswer { QuestionId = questions[29].Id, AnswerId = questions[29].Answers.ToList()[1].Id, IsCorrect = true }
                    }
                }
        ];

        if (!context.UserQuizzes.Any())
        {
            context.UserQuizzes.AddRange(userQuizzes);
            context.SaveChanges();
        }
    }
}