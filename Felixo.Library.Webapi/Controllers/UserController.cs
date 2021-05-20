using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Felixo.Library.Entities.Models;
using Felixo.Library.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Felixo.Library.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUnit _unit;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserController(IUnit unit, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager)
        {
            _unit = unit;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<object> Login([FromBody]LoginModel loginUser)
        {
            if (loginUser == null)
                return null;

            var user = _unit.IdentityUserRepository.GetUserWithRole(loginUser.Email);
            var roles =user.Roles.Select(p=>new { id = p.Role.Id, name = p.Role.Name });
            var isPasswordCorrect = await _signInManager.CheckPasswordSignInAsync( user.IdentityUser, loginUser.Password, false);
            if (isPasswordCorrect.Succeeded)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("Felixo.Library.Webapi");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Id", user.IdentityUser.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.IdentityUser.UserName.ToString()),
                        new Claim(ClaimTypes.Email, user.IdentityUser.Email.ToString()),
                        new Claim(ClaimTypes.Role,JsonConvert.SerializeObject(roles))
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                var x = new StringContent(tokenHandler.WriteToken(token));
                return await Task.FromResult(new { jwt = tokenHandler.WriteToken(token) });
            }
            else
            {
                return await Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));

            }
        }

        [Route("GetUsers")]
        [HttpGet]
        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await _unit.IdentityUserRepository.Get();
        }

        [HttpPost]
        [Route("InsertUser")]
        public async Task<ActionResult> InsertUser([FromBody]dynamic userData)
        {
            var user = new ApplicationUser
            {
                Email = userData.Email ,
                UserName = userData.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, userData.Password );
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            return Ok(new { Username = user.UserName });
        }
        [HttpGet]
        [Route("InsertRole")]
        public async Task<ActionResult> InsertRole(string roleName)
        {
            var role = new ApplicationRole
            {
                Name = roleName
            };
            await _roleManager.CreateAsync(role);

            return Ok(new { Username = role.UserRoles});
        }
    }
}