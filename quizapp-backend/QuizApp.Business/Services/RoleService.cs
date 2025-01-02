using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizApp.Data;

namespace QuizApp.Business
{
    /// <summary>
    /// Represents a service for managing roles.
    /// </summary>
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="logger">The logger.</param>
        public RoleService(IUnitOfWork unitOfWork, ILogger<RoleService> logger, RoleManager<Role> roleManager) : base(unitOfWork, logger)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// Creates a new role asynchronously.
        /// </summary>
        /// <param name="roleCreateViewModel">The view model containing role creation data.</param>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the role was created successfully.</returns>
        public async Task<bool> CreateRoleAsync(RoleCreateViewModel roleCreateViewModel)
        {
            if(roleCreateViewModel == null)
            {
                _logger.LogError("Role create view model is null.");
                return false;
            }

            // create a new role entity
            var role = new Role
            {
                Name = roleCreateViewModel.Name,
                Description = roleCreateViewModel.Description,
                IsActive = roleCreateViewModel.IsActive
            };

            // create the role in the database
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Role created: {role.Name}");
                return true;
            }

            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
            }

            return false;
        }

        /// <summary>
        /// Updates an existing role asynchronously.
        /// </summary>
        /// <param name="id">The ID of the role to update.</param>
        /// <param name="roleEditViewModel">The view model containing role update data.</param>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the role was updated successfully.</returns>
        public async Task<bool> UpdateRoleAsync(Guid id, RoleEditViewModel roleEditViewModel)
        {
            if(roleEditViewModel == null)
            {
                _logger.LogError("Role edit view model is null.");
                return false;
            }

            // get the role entity
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);

            if (role == null)
            {
                _logger.LogWarning($"Role not found: {id}");
                return false;
            }

            // update the role entity
            role.Name = roleEditViewModel.Name;
            role.Description = roleEditViewModel.Description;
            role.IsActive = roleEditViewModel.IsActive;

            // update the role in the database
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Role updated: {role.Name}");
                return true;
            }

            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
            }

            return false;
        }

        public async Task<PaginatedResult<RoleViewModel>> SearchAsync(SearchRoleQuery request)
        {
            // Get query
            var query = _unitOfWork.RoleRepository.GetQuery().Where(r => r.IsActive == request.IsActive);

            // Filter by name
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(r => (r.Name ?? string.Empty).Contains(request.Name));
            }

            // Order by
            query = !string.IsNullOrEmpty(request.OrderBy)
                ? query.OrderByExtensition(request.OrderBy, request.OrderDirection.ToString())
                : query.OrderBy(o => o.Name);

            // Get total count
            var totalCount = await query.CountAsync();

            // Paginate
            query = query.Skip((request.Page - 1) * request.Size).Take(request.Size);

            // Map Role to RoleViewModel
            var result = await query.Select(role => new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name ?? string.Empty,
                Description = role.Description,
                IsActive = role.IsActive
            }).ToListAsync();

            return new PaginatedResult<RoleViewModel>(result, totalCount, request.Page, request.Size);
        }
    }
}
