using Microsoft.AspNetCore.Mvc;
using QuizApp.Business;

namespace QuizApp.WebAPI;


/// <summary>
/// Controller responsible for handling authentication related requests.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="authService">The authentication service.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Handles the login request.
    /// </summary>
    /// <param name="loginViewModel">The login view model.</param>
    /// <returns>The login response view model.</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(List<LoginResponseViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authService.LoginAsync(loginViewModel);

        if (result == null)
        {
            return Unauthorized();
        }

        return Ok(result);
    }

    /// <summary>
    /// Handles the register request.
    /// </summary>
    /// <param name="registerViewModel">The register view model.</param>
    /// <returns>The login response view model.</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(List<LoginResponseViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authService.RegisterAsync(registerViewModel);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}
