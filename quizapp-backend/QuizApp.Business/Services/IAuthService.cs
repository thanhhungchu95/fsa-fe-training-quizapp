namespace QuizApp.Business;

/// <summary>
/// Represents the interface for authentication service.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Authenticates a user with the provided login credentials.
    /// </summary>
    /// <param name="loginViewModel">The login view model containing the user's login credentials.</param>
    /// <returns>A task that represents the asynchronous login operation. The task result contains the login response view model.</returns>
    Task<LoginResponseViewModel> LoginAsync(LoginViewModel loginViewModel);

    /// <summary>
    /// Registers a new user with the provided registration details.
    /// </summary>
    /// <param name="registerViewModel">The register view model containing the user's registration details.</param>
    /// <returns>A task that represents the asynchronous registration operation. The task result contains the login response view model.</returns>
    Task<LoginResponseViewModel> RegisterAsync(RegisterViewModel registerViewModel);
}
