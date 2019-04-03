using BLL.Infrastructure;
using BLL.Services;
using DAL.EF;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using BLL.Interfaces;
using Microsoft.Owin.Security.Provider;

namespace WebAPI
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864

        public void ConfigureAuth(IAppBuilder app)
        {
            //Cors
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(CompanyContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";

            UserService us = (UserService)System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver.GetService(
                typeof(IUserService));

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, us),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }

    public interface SomeInterface
    {
        string Name { get; set; }

        string SomeMethod();
    }

    public class SomeClass : SomeInterface
    {
        public SomeClass(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string SomeMethod()
        {
            return "Hello Oleg";
        }
    }


    public class MAin
    {
        public void main()
        {
            Console.WriteLine("Enter array length");
            int length = Convert.ToInt32(Console.ReadLine());

            SomeInterface[] array = new SomeInterface[length];

            int i = 0;
            while (i < length)
            {
                Console.WriteLine("Enter name");
                string name = Console.ReadLine();
                array[i] = new SomeClass(name);
            }

            foreach (var value in array)
            {
                Console.Write("Name:" + value.Name);
                Console.Write("MethodResult:" + value.SomeMethod());
            }
        }
    }
    
}
