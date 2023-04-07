using BookStore.Model;
using BookStore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        public readonly IAccountRepo accountRepo;
        private readonly ILogger<AccountController> logger;    

        public AccountController(IAccountRepo _accountRepo , ILogger<AccountController> _logger)
        {
            accountRepo = _accountRepo;
            logger = _logger;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SinUp([FromBody] SignUpModel signUpModel)
        {
            logger.LogError("Error");
            logger.LogWarning("Warning");
            logger.LogTrace("Trace");

            var result = await accountRepo.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok($"SignedUp Succeeded");
            }
            else
            {
                List<IdentityError> errorList = result.Errors.ToList();
                var errors = string.Join(", ", errorList.Select(e => e.Description));

                return BadRequest(errors);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInModel signINModel)
        {
            logger.LogError("Error");
            logger.LogWarning("Warning");
            logger.LogTrace("Trace");

            var result = await accountRepo.LogInAsync(signINModel);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized("Incorrect Password or User Id");
            }
            return Ok(result);
        }
    }
}