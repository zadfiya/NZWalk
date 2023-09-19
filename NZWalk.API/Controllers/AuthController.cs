using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Model.DTO.Auth;
using NZWalk.API.Repository;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegsiterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username,
            };
           var idenetityResult =  await userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            if(idenetityResult.Succeeded) {

                if(registerRequestDTO.Roles.Any()) {
                idenetityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if(idenetityResult.Succeeded)
                    {
                        return Ok("User is Created! We");
                    }
                }
            }
            return BadRequest("Something Went wrong");
           
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Username);
            if (user == null)
            {
                return BadRequest("User Not Found!! Please Sign Up");
            }
            else
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if(checkPassword)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if(roles!=null&&roles.Any())
                    {
                        var token = tokenRepository.CreateJWTToken(user, roles.ToList());
                        return Ok( token);

                    }
                    
                }
                return BadRequest("Password is incorrect!!");
            }

        }


    }

    
}
