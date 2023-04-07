using BookStore.Model;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repository
{
    public interface IAccountRepo
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string> LogInAsync(SignInModel signInModel);
    }
}