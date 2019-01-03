using Microsoft.Owin.Security.OAuth;
using MusicRepository;
using MusicServices.APITools.Interfaces;
using MusicServices.APITools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Client.Providers
{
    public class MusicAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserService _userService;

        public MusicAuthorizationServerProvider(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            DomainModel.User domainUser = (await _userService
                                          .GetAll())
                                          .FirstOrDefault(user => user.Email == context.UserName && user.Password == context.Password);

            if (domainUser == null)
            {
                context.SetError("invalid_grant", "The email or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Email, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, domainUser.RoleType.ToString()));

            context.Validated(identity);
        }
    }
}