using BookStore.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Repository
{

    public class AccountRepo : IAccountRepo
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserManager<ApplicationUser> userManager { get; set; }
        public IConfiguration configuration { get; }

        public AccountRepo
            (UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            IConfiguration _configuration)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            configuration = _configuration;
        }
        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email,
            };
            return await userManager.CreateAsync(user, signUpModel.Password);
        }

        public async Task<string> LogInAsync(SignInModel signInModel)
        {
            var res = await signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);
            if (!res.Succeeded)
            {
                return null;
            }
            var authclaim = new List<Claim>
            {
                new Claim(ClaimTypes.Name , signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var authkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:validissuer"],
                audience: configuration["JWT:validAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authclaim,
                signingCredentials: new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
