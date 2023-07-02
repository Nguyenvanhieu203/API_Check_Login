using Amazon.Runtime.Internal.Util;
using BookManager.Data;
using BookManager.Models;
using BookManager.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepostory accRepo;

        public AccountController(IAccountRepostory repo)
        {
            accRepo = repo;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await accRepo.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();
        }

        [HttpPost("SignIn")]
        public async Task<Custommessage> SignIn(SignInModel signInModel)
        {
            var result = await accRepo.SignInAsync(signInModel);
            var result1 = new Custommessage();
            var user = await accRepo.GetByIdAsync(signInModel.Email);
            if (string.IsNullOrEmpty(result))
            {
                //result1.status = BadRequest().ToString();
                throw new Exception();
                //return string.Empty;
            }
            result1.token = result;
            result1.email = user.Email;
            result1.Id = user.Id;
            result1.name = user.FirstName + " " + user.LastName;
            //result1.name = await accRepo.GetByNameAsync(signInModel.Email);
            return result1;
        }
    }
}
