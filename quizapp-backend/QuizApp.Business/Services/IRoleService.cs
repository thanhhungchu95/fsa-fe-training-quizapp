using QuizApp.Data;

namespace QuizApp.Business
{
    /// <summary>
    /// Represents the interface for role-related operations.
    /// </summary>
    public interface IRoleService : IBaseService<Role>
    {
        /// <summary>
        /// Creates a new role asynchronously.
        /// </summary>
        /// <param name="roleCreateViewModel">The view model containing the role details.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a boolean indicating whether the role was created successfully.</returns>
        Task<bool> CreateRoleAsync(RoleCreateViewModel roleCreateViewModel);

        /// <summary>
        /// Updates an existing role asynchronously.
        /// </summary>
        /// <param name="id">The ID of the role to update.</param>
        /// <param name="roleEditViewModel">The view model containing the updated role details.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a boolean indicating whether the role was updated successfully.</returns>
        Task<bool> UpdateRoleAsync(Guid id, RoleEditViewModel roleEditViewModel);

        /// <summary>
        /// Searches for roles asynchronously based on the provided search criteria.
        /// </summary>
        /// <param name="request">The search query containing filter and pagination parameters.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a paginated result of role view models matching the search criteria.</returns>
        Task<PaginatedResult<RoleViewModel>> SearchAsync(SearchRoleQuery request);
    }
}
