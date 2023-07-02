using BookManager.Data;
using BookManager.Models;
using BookManager.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookManager.Repository.Class
{
    public class AccountRepository : IAccountRepostory
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> SignInAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);
            if (!result.Succeeded)
            {
                return string.Empty;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:ISK"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new ApplicationUser
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                PhoneNumber = signUpModel.PhoneNumber,
                UserName = signUpModel.Email
            };
            var result = await _userManager.CreateAsync(user, signUpModel.Password);
            return result;
        }
        //public async Task<string> GetById(string id)
        //{
        //    var studentName =  .Where(student => student.Name.Contains("an")).Select(student => student.Name);
        //    foreach (var student in studentName)
        //    {
        //        Console.WriteLine("Name: " + student);
        //    }

        //    return string.Empty;
        //}

        public async Task<ApplicationUser> GetByIdAsync(string email)
        {
            var searchEmail = await _userManager.FindByEmailAsync(email);
            return searchEmail;
        }

        //public async Task<string> GetByNameAsync(string email)
        //{
        //    var searchEmail = await _userManager.FindByEmailAsync(email);
        //    var firstName = searchEmail.FirstName.ToString();
        //    var lastName = searchEmail.LastName.ToString();
        //    var userName = firstName + " " + lastName;
        //    return userName;
        //}
    }
}
