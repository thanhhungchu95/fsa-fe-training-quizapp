using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Business;

namespace QuizApp.WebAPI;

/// <summary>
/// Controller for managing roles.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]

public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RolesController"/> class.
    /// </summary>
    /// <param name="roleService">The role service.</param>
    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// Gets all roles.
    /// </summary>
    /// <returns>A list of role view models.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin, Editor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<RoleViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleService.GetAllAsync();

        var roleViewModels = roles.Select(question => new RoleViewModel
        {
            Id = question.Id,
            Name = question.Name ?? string.Empty,
            Description = question.Description,
            IsActive = question.IsActive,
        }).ToList();

        return Ok(roleViewModels);
    }

    /// <summary>
    /// Searches for roles based on the provided query.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <returns>A paginated list of roles.</returns>
    [HttpGet("search")]
    [Authorize(Roles = "Admin, Editor")]
    [ProducesResponseType(typeof(PaginatedResult<RoleViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Search([FromQuery] SearchRoleQuery query)
    {
        var roles = await _roleService.SearchAsync(query);

        return Ok(roles);
    }

    /// <summary>
    /// Gets a role by its ID.
    /// </summary>
    /// <param name="id">The ID of the role.</param>
    /// <returns>The role view model.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Editor")]
    [ProducesResponseType(typeof(RoleViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRole(Guid id)
    {
        var role = await _roleService.GetByIdAsync(id);

        if (role == null)
        {
            return NotFound();
        }

        var roleViewModel = new RoleViewModel
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            Description = role.Description,
            IsActive = role.IsActive
        };
        return Ok(roleViewModel);
    }

    /// <summary>
    /// Creates a new role.
    /// </summary>
    /// <param name="roleCreateViewModel">The role create view model.</param>
    /// <returns>A boolean indicating whether the role was created successfully.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateRole(RoleCreateViewModel roleCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool result = await _roleService.CreateRoleAsync(roleCreateViewModel);

        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    /// <summary>
    /// Updates an existing role.
    /// </summary>
    /// <param name="id">The ID of the role to update.</param>
    /// <param name="roleEditViewModel">The role edit view model.</param>
    /// <returns>A boolean indicating whether the role was updated successfully.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, Editor")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdateRole(Guid id, RoleEditViewModel roleEditViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool result = await _roleService.UpdateRoleAsync(id, roleEditViewModel);

        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a role by its ID.
    /// </summary>
    /// <param name="id">The ID of the role to delete.</param>
    /// <returns>A boolean indicating whether the role was deleted successfully.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteRole(Guid id)
    {
        bool result = await _roleService.DeleteAsync(id);

        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}
