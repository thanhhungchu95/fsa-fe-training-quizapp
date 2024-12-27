using QuizApp.Data;

namespace QuizApp.Business
{
    /// <summary>
    /// Represents a service for managing user-related operations.
    /// </summary>
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// Changes the password for the user asynchronously.
        /// </summary>
        /// <param name="changePasswordViewModel">The view model containing the new password.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a boolean indicating whether the password change was successful.</returns>
        Task<bool> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel);

        /// <summary>
        /// Creates a new user asynchronously.
        /// </summary>
        /// <param name="userCreateViewModel">The view model containing user creation data.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if the user was created successfully; otherwise, false.</returns>
        Task<bool> CreateUserAsync(UserCreateViewModel userCreateViewModel);

        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="userEditViewModel">The view model containing user update data.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if the user was updated successfully; otherwise, false.</returns>
        Task<bool> UpdateUserAsync(Guid id, UserEditViewModel userEditViewModel);

        /// <summary>
        /// Adds a role to a user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to add the role to.</param>
        /// <param name="roleId">The role to add to the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if the role was added successfully; otherwise, false.</returns>
        Task<bool> AddRoleToUserAsync(Guid userId, string roleName);

        /// <summary>
        /// Removes a role from a user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to remove the role from.</param>
        /// <param name="role">The role to remove from the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if the role was removed successfully; otherwise, false.</returns>
        Task<bool> RemoveRoleFromUserAsync(Guid userId, string roleName);
    }
}
