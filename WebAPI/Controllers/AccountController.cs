using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public AccountController(ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IUserService userService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _userService = userService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Request.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

            //SignInStatus result =
            //    await SignInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password, false, false);

            //if (result != SignInStatus.Success)
            //{
            //    return Unauthorized();
            //}

            Request.GetOwinContext().Authentication.SignIn();

            AuthenticationUser authenticationUser =
                UserManager.Users.FirstOrDefault(u => u.UserName == signInModel.UserName);
            User user = await _userService.GetUserByAuthenticationIdAsync(authenticationUser.Id);

            UserDTO userDTO = BLL.Mapper.AutoMapperConfig.Mapper.Map<User, UserDTO>(user);
            string token = await CreateJWT(authenticationUser);

            return Ok(new {userDTO, token});
        }

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

            IdentityResult result = await UserManager.CreateAsync(authenticationUser, model.Password);

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
                AuthenticationUserId = authenticationUser.Id
            };

            await _userService.CreateUserAsync(userDTO);

            string token = await CreateJWT(authenticationUser);

            return Ok(new {user = authenticationUser, token});
        }

        private async Task<string> CreateJWT(AuthenticationUser authenticationUser)
        {
            string audience = WebConfigurationManager.AppSettings["jwt:aud"];
            string issuer = WebConfigurationManager.AppSettings["jwt:iss"];
            string key = WebConfigurationManager.AppSettings["jwt:hash_key"];

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(key);
            var secret = Convert.ToBase64String(bytes);

            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.Default.GetBytes(secret));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature);

            var issuedAt = DateTime.Now.ToUniversalTime();
            var expiresAt = issuedAt.AddMinutes(5);

            IList<Claim> claims = await UserManager.GetClaimsAsync(authenticationUser.Id);

            if (authenticationUser.Roles.Count > 0)
            {
                IList<string> roles = await UserManager.GetRolesAsync(authenticationUser.Id);

                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var token = new JwtSecurityToken(issuer,
                audience,
                claims,
                issuedAt,
                expiresAt,
                signingCredentials);

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
