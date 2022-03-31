using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers;

/// <summary>
/// Account API controller.
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="signInManager">Sign in manager.</param>
    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult> Register([FromQuery] UserCredentials credentials)
    {
        var user = new User
        {
            UserName = credentials.UserName,
        };
        var registerResult = await userManager.CreateAsync(user, credentials.Password);

        if (!registerResult.Succeeded)
        {
            return BadRequest(registerResult.Errors);
        }

        return Ok("Registered.");
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult> Authorize([FromQuery]UserCredentials credentials)
    {
        var signInResult = await signInManager.PasswordSignInAsync(credentials.UserName, credentials.Password, isPersistent: false, lockoutOnFailure: false);
        if (!signInResult.Succeeded)
        {
            return BadRequest("Failed to authorize. Check your credentials.");
        }
        return Ok("Authorized.");
    }

    [Authorize]
    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok("Logged out.");
    }
}