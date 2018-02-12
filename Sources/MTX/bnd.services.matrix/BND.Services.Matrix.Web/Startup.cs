using System.Configuration;

using IdentityServer3.AccessTokenValidation;

using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BND.Services.Matrix.Web.Startup))]

namespace BND.Services.Matrix.Web
{
    /// <summary>
    /// The OWIN startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            if (bool.Parse(ConfigurationManager.AppSettings.Get(Constants.AuthSecurityEnabled)))
            {
                app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = ConfigurationManager.AppSettings.Get(Constants.AuthServerUrl),
                    ValidationMode = ValidationMode.ValidationEndpoint,

                    RequiredScopes = new[] { ConfigurationManager.AppSettings.Get(Constants.AuthMatrixScope) }
                });
            }
        }
    }
}
