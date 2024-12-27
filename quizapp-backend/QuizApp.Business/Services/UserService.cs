using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using QuizApp.Data;

namespace QuizApp.Business
{
    /// <summary>
    /// Represents a service for managing user-related operations.
    /// </summary>
    public class UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, UserManager<User> userManager, RoleManager<Role> roleManager) :
        BaseService<User>(unitOfWork, logger), IUserService
    {
        private readonly UserManager<User> _userManager = userManager;

        private readonly RoleManager<Role> _roleManager = roleManager;

        public async Task<bool> AddRoleToUserAsync(Guid userId, string roleName)
        {
            // Check if the user exists
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                _logger.LogWarning($"User not found: {userId}");
                return false;
            }

            // Check if the role exists
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                _logger.LogWarning($"Role not found: {roleName}");
                return false;
            }

            // Add the role to the user
            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Role added to user: {user.UserName}");
                return true;
            }

            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
            }

            return false;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel)
        {
            // Check if the view model is null
            if (changePasswordViewModel == null)
            {
                _logger.LogError("Change password view model is null.");
                return false;
            }

            // Get the user entity
            var user = await _userManager.FindByNameAsync(changePasswordViewModel.UserName);

            if (user == null)
            {
                _logger.LogWarning($"User not found: {changePasswordViewModel.UserName}");
                return false;
            }

            if (changePasswordViewModel.NewPassword != changePasswordViewModel.ConfirmPassword)
            {
                _logger.LogWarning("New password and confirm password do not match.");
                return false;
            }

            // Change the user's password
            var result = await _userManager.ChangePasswordAsync(user, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Password changed for user: {user.UserName}");
                return true;
            }

            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
            }

            return false;
        }

        /// <summary>
        /// Creates a new user asynchronously.
        /// </summary>
        /// <param name="userCreateViewModel">The view model containing user creation data.</param>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the user was created successfully.</returns>
        public async Task<bool> CreateUserAsync(UserCreateViewModel userCreateViewModel)
        {
            // create a new user entity
            var user = new User
            {
                FirstName = userCreateViewModel.FirstName,
                LastName = userCreateViewModel.LastName,
                Email = userCreateViewModel.Email,
                UserName = userCreateViewModel.UserName,
                PhoneNumber = userCreateViewModel.PhoneNumber,
                DateOfBirth = userCreateViewModel.DateOfBirth,
                Avatar = userCreateViewModel.Avatar,
                IsActive = userCreateViewModel.IsActive
            };

            // create the user in the database
            var result = await _userManager.CreateAsync(user, userCreateViewModel.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User created: {user.UserName}");
                return true;
            }

            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
            }

            return false;
        }

        public async Task<bool> RemoveRoleFromUserAsync(Guid userId, string roleName)
        {
            // Check if the user exists
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                _logger.LogWarning($"User not found: {userId}");
                return false;
            }

            // Check if the role exists
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                _logger.LogWarning($"Role not found: {roleName}");
                return false;
            }

            // Remove the role from the user
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Role removed from user: {user.UserName}");
                return true;
            }

            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
            }

            return false;
        }

        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="userEditViewModel">The view model containing user update data.</param>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the user was updated successfully.</returns>
        public async Task<bool> UpdateUserAsync(Guid id, UserEditViewModel userEditViewModel)
        {
            // get the user entity
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                _logger.LogWarning($"User not found: {id}");
                return false;
            }

            // update the user entity
            user.FirstName = userEditViewModel.FirstName;
            user.LastName = userEditViewModel.LastName;
            user.PhoneNumber = userEditViewModel.PhoneNumber;
            user.DateOfBirth = userEditViewModel.DateOfBirth;
            user.Avatar = userEditViewModel.Avatar;
            user.IsActive = userEditViewModel.IsActive;

            // update the user in the database
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User updated: {user.UserName}");
                return true;
            }

            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
            }

            return false;
        }
    }
}
