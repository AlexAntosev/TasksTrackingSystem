using BLL.Infrastructure;
using DAL.EF;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Text;
using System.Web.Configuration;
using WebAPI.Infrastructure;

namespace WebAPI
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            //Cors
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(CompanyContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            WebConfigurationManager.AppSettings["jwt:aud"] = "http://localhost:4200";
            string audience = WebConfigurationManager.AppSettings["jwt:aud"];

            WebConfigurationManager.AppSettings["jwt:iss"] = "https://localhost:60542";
            string issuer = WebConfigurationManager.AppSettings["jwt:iss"];

            //string key = "some randomly generated cryptographically good number";
            WebConfigurationManager.AppSettings["jwt:hash_key"] = RandomGenerator.Next(Int32.MaxValue - 1, Int32.MaxValue).ToString();
            string key = WebConfigurationManager.AppSettings["jwt:hash_key"];

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(key);
            var secret = Convert.ToBase64String(bytes);
            var now = DateTime.UtcNow;
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secret));
            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature);
            
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = signingCredentials.Key,

                        ValidIssuer = issuer,
                        ValidateIssuer = true,

                        ValidAudience = audience,
                        ValidateAudience = true,

                        ValidateLifetime = true
                    },
                }
            );
        }
    }
}
