using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecureServer.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private bool IsLogin { get; set; } = false;
        ClaimsPrincipal LoggedInUser = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!IsLogin)
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal( new ClaimsIdentity())));

            return Task.FromResult(new AuthenticationState(LoggedInUser));
        }

        public void Login(string eposta, string parola)
        {
            if (!(eposta == "celalergun@gmail.com" && parola == "123456"))
                return;

            IsLogin = true;
            ClaimsIdentity identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Celal Ergün"),
                }, "My fancy authentication type");

            ClaimsPrincipal user = new ClaimsPrincipal(identity);
            LoggedInUser = user;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void Logout()
        {
            IsLogin = false;
            LoggedInUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
