using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Business;

namespace QuizApp.WebAPI;


/// <summary>
/// Controller for managing users.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersController"/> class.
    /// </summary>
    /// <param name="userService">The role service.</param>
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Retrieves a list of all users.
    /// </summary>
    /// <returns>A list of UserViewModel objects.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin, Editor")]
    [ProducesResponseType(typeof(List<UserViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllAsync();

        var userViewModels = users.Select(user => new UserViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            DateOfBirth = user.DateOfBirth,
            Avatar = user.Avatar ?? string.Empty,
            IsActive = user.IsActive,
        }).ToList();

        return Ok(userViewModels);
    }

    /// <summary>
    /// Searches for users based on the provided query.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <returns>A paginated list of users.</returns>
    [HttpGet("search")]
    //[Authorize(Roles = "Admin, Editor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedResult<UserViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Search([FromQuery] SearchUserQuery query)
    {
        var users = await _userService.SearchAsync(query);

        return Ok(users);
    }

    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The UserViewModel object.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Editor")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var userViewModel = new UserViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            DateOfBirth = user.DateOfBirth,
            Avatar = user.Avatar ?? string.Empty,
            IsActive = user.IsActive
        };
        return Ok(userViewModel);
    }

    /// <summary>
    /// Changes the password for a user.
    /// </summary>
    /// <param name="changePasswordViewModel">The ChangePasswordViewModel object containing the new password.</param>
    /// <returns>A boolean indicating whether the password change was successful.</returns>
    [AllowAnonymous]
    [HttpPost("/changePassword")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool result = await _userService.ChangePasswordAsync(changePasswordViewModel);

        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="userCreateViewModel">The UserCreateViewModel object containing the user details.</param>
    /// <returns>A boolean indicating whether the user creation was successful.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateUser(UserCreateViewModel userCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool result = await _userService.CreateUserAsync(userCreateViewModel);

        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="userEditViewModel">The UserEditViewModel object containing the updated user details.</param>
    /// <returns>A boolean indicating whether the user update was successful.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, Editor")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdateUser(Guid id, UserEditViewModel userEditViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool result = await _userService.UpdateUserAsync(id, userEditViewModel);

        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>A boolean indicating whether the user deletion was successful.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        bool result = await _userService.DeleteAsync(id);

        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    /// <summary>
    /// Adds a role to a user.
    /// </summary>
    /// <param name="userId">The ID of the user to add the role to.</param>
    /// <param name="roleName">The role to add to the user.</param>
    /// <returns>A boolean indicating whether the role was added successfully.</returns>
    [HttpPost("{userId}/addRole/{role}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddRoleToUser(
        [FromRoute] Guid userId,
        [FromRoute] string roleName)
    {
        bool result = await _userService.AddRoleToUserAsync(userId, roleName);

        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    /// <summary>
    /// Removes a role from a user.
    /// </summary>
    /// <param name="userId">The ID of the user to remove the role from.</param>
    /// <param name="roleName">The role to remove from the user.</param>
    /// <returns>A boolean indicating whether the role was removed successfully.</returns>
    [HttpDelete("{userId}/removeRole/{roleName}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveRoleFromUser(
        [FromRoute] Guid userId,
        [FromRoute] string roleName)
    {
        bool result = await _userService.RemoveRoleFromUserAsync(userId, roleName);

        if (!result)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}
