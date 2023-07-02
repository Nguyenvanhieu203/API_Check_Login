
using BookManager.Data;
using BookManager.Models;
using Microsoft.AspNetCore.Identity;

namespace BookManager.Repository.Interface
{
    public interface IAccountRepostory
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        public Task<string> SignInAsync(SignInModel signInModel);
        public Task<ApplicationUser> GetByIdAsync(string id);
        //public Task<string> GetByNameAsync(string email);
    }
}
