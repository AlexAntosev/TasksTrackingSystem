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
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private ApplicationRoleManager _roleManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public AccountController(ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            ApplicationRoleManager roleManager,
            IUserService userService,
            IMapper mapper)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
            _userService = userService;
            _mapper = mapper;
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

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Current")]
        public async Task<IHttpActionResult> GetCurrentUserAsync()
        {
            AuthenticationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            string currentUserId = user?.Id;

            if (currentUserId == null)
            {
                return Unauthorized();
            }

            User currentAppUser = await _userService.GetUserByAuthenticationIdAsync(currentUserId);

            return Ok(currentAppUser);
        }

        [HttpPost]
        [Route("SignOut")]
        public IHttpActionResult SignOut()
        {
            var authenticationManager = HttpContext.Current.Request.GetOwinContext().Authentication;
            authenticationManager.SignOut();

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SignIn")]
        public async Task<IHttpActionResult> SignInAsync(SignInModel signInModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Sign in form is incorrect.");
            }

            SignInStatus result =
                await SignInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password, false, false);

            if (result != SignInStatus.Success)
            {
                return Unauthorized();
            }

            AuthenticationUser authenticationUser =
                UserManager.Users.FirstOrDefault(u => u.UserName == signInModel.UserName);

            User user = await _userService.GetUserByAuthenticationIdAsync(authenticationUser.Id);

            UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
            string token = await CreateJWT(authenticationUser);

            return Ok(new { user = userDTO, token });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SignUp")]
        public async Task<IHttpActionResult> SignUpAsync(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Sign up form is incorrect.");
            }

            var authenticationUser = new AuthenticationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(authenticationUser, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest("Cannot create user with current email or password.");
            }

            UserDTO userDTO = new UserDTO()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Position = model.Position,
                AuthenticationUserId = authenticationUser.Id
            };

            User createdUser = await _userService.CreateUserAsync(userDTO);
            UserDTO createdUserDTO = _mapper.Map<User, UserDTO>(createdUser);

            string token = await CreateJWT(authenticationUser);

            return Ok(new { user = createdUserDTO, token });
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
            var expiresAt = issuedAt.AddMinutes(30);

            IList<Claim> claims = await UserManager.GetClaimsAsync(authenticationUser.Id);

            if (authenticationUser.Roles.Count > 0)
            {
                IList<string> roles = await UserManager.GetRolesAsync(authenticationUser.Id);

                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, authenticationUser.Id));

            var token = new JwtSecurityToken(issuer,
                audience,
                claims,
                issuedAt,
                expiresAt,
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async System.Threading.Tasks.Task EnsureRoleAsync(string roleName)
        {
            bool adminRoleExists = await RoleManager.RoleExistsAsync(roleName);
            if (!adminRoleExists)
            {
                var adminRole = new IdentityRole(roleName);
                await _roleManager.CreateAsync(adminRole);
            }
            else
            {
                await _roleManager.FindByNameAsync(roleName);
            }
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
