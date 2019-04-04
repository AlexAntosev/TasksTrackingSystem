using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Results;
using Task = System.Threading.Tasks.Task;

namespace WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private UserManager<AuthenticationUser> _userManager;
        private SignInManager<AuthenticationUser, string> _signInManager;
        private IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<AuthenticationUser> userManager,
            IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public AccountController(UserManager<AuthenticationUser> userManager,
            SignInManager<AuthenticationUser, string> signInManager,
            IUserService userService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _configuration = configuration;
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("SignIn")]
        public async Task<IHttpActionResult> SignInAsync(SignInModel signInModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SignInStatus result =
                await _signInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password, false, false);

            if (result != SignInStatus.Success)
            {
                return Unauthorized();
            }

            AuthenticationUser authenticationUser =
                _userManager.Users.FirstOrDefault(u => u.UserName == signInModel.UserName);
            User user = await _userService.GetUserByAuthenticationIdAsync(authenticationUser.Id);

            UserDTO userDTO = BLL.Mapper.AutoMapperConfig.Mapper.Map<User, UserDTO>(user);
            string token = await CreateJWT(authenticationUser);

            return Ok(new {userDTO, token});
        }


        // POST api/Account/SignUp
        [AllowAnonymous]
        [HttpPost]
        [Route("SignUp")]
        public async Task<IHttpActionResult> SignUpAsync(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authenticationUser = new AuthenticationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await _userManager.CreateAsync(authenticationUser, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.ToString());
            }

            UserDTO userDTO = new UserDTO()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Position = model.Position,
                ApplicationUserId = authenticationUser.Id
            };

            await _userService.CreateUserAsync(userDTO);

            string token = await CreateJWT(authenticationUser);

            return Ok(new {user = authenticationUser, token});
        }

        private async Task<string> CreateJWT(AuthenticationUser authenticationUser)
        {
            long unixNowSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();
            long expirationTime = unixNowSeconds + 10 * 3600;

            var claims = new List<Claim>
            {
                // https://openid.net/specs/openid-connect-core-1_0.html#IDToken
                new Claim(JwtRegisteredClaimNames.Sub, authenticationUser.Id),
                //new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"]),
                //new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iat, unixNowSeconds.ToString()),
                //new Claim(JwtRegisteredClaimNames.Exp, expirationTime.ToString()),
                // new Claim(ClaimTypes.Role, user.)
            };

            Task<IList<string>> rolesTask = _userManager.GetRolesAsync(authenticationUser.Id);
            Task<IList<Claim>> userClaimsTask = _userManager.GetClaimsAsync(authenticationUser.Id);

            await System.Threading.Tasks.Task.WhenAll(rolesTask, userClaimsTask);

            foreach (string role in rolesTask.Result)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.AddRange(userClaimsTask.Result);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }
    }
}
