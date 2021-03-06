﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FocusMark.SDK.Account
{
    public class DesktopLoginService : ILoginService
    {
        private const string oauthFlow = "code";
        private const string grant_type = "authorization_code";

        private readonly AccountConfiguration configuration;
        private readonly IHttpClientFactory clientFactory;

        public DesktopLoginService(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            IConfigurationSection focusmarkSection = configuration.GetSection("FocusMark");
            this.configuration = new AccountConfiguration();
            focusmarkSection.Bind(this.configuration);

            this.clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<ServiceResponse<JwtTokens>> Login()
        {
            if (!HttpListener.IsSupported)
            {
                throw new HttpListenerNotSupportedException();
            }

            var listener = new HttpListener();
            listener.Prefixes.Add($"{this.configuration.CallbackUrl}/");
            listener.Start();

            HttpListenerContext requestContext = await this.HandleAuthCallback(listener);
            HttpListenerRequest httpRequest = requestContext.Request;

            string authCode = httpRequest.QueryString["code"];
            JwtTokens tokens = await this.RequestJwtTokens(authCode);
            if (tokens == null)
            {
                await this.RespondWithFailedLogin(requestContext);
                listener.Stop();
                return null;
            }

            await this.RespondWithSuccessfulLogin(requestContext);
            listener.Stop();

            return new ServiceResponse<JwtTokens>(tokens);
        }

        private async Task<HttpListenerContext> HandleAuthCallback(HttpListener listener)
        {
            string[] requestedScopes = AuthorizationScopes.ToArray();
            string flattenedScopes = string.Join("+", requestedScopes);

            string url = $"{this.configuration.LoginUrl}?client_id={this.configuration.ClientId}&response_type={oauthFlow}&scope={flattenedScopes}&redirect_uri={configuration.CallbackUrl}";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else
            {
                throw new PlatformNotSupportedException();
            }

            HttpListenerContext context = await listener.GetContextAsync();
            return context;
        }

        private async Task<JwtTokens> RequestJwtTokens(string authCode)
        {
            HttpClient httpClient = this.clientFactory.CreateClient("Jwt Token Request");

            var bodyContent = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("code", authCode),
                new KeyValuePair<string, string>("grant_type", grant_type),
                new KeyValuePair<string, string>("client_id", this.configuration.ClientId),
                new KeyValuePair<string, string>("redirect_uri", this.configuration.CallbackUrl),
            };

            var body = new FormUrlEncodedContent(bodyContent);
            var authResponse = await httpClient.PostAsync($"{this.configuration.OAuthTokenUrl}", body);
            var tokenPayload = await authResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<JwtTokens>(tokenPayload);
        }

        private async Task RespondWithFailedLogin(HttpListenerContext requestContext)
        {
            byte[] failedBuffer = Encoding.UTF8.GetBytes(this.GetLogInFailedHtml());

            requestContext.Response.StatusCode = 500;
            requestContext.Response.ContentLength64 = failedBuffer.Length;

            await requestContext.Response.OutputStream.WriteAsync(failedBuffer, 0, failedBuffer.Length);
            requestContext.Response.OutputStream.Close();
        }

        private async Task RespondWithSuccessfulLogin(HttpListenerContext requestContext)
        {
            byte[] sucessBuffer = Encoding.UTF8.GetBytes(this.GetLogInSuccessHtml());

            requestContext.Response.StatusCode = 200;
            requestContext.Response.ContentLength64 = sucessBuffer.Length;

            await requestContext.Response.OutputStream.WriteAsync(sucessBuffer, 0, sucessBuffer.Length);
            requestContext.Response.OutputStream.Close();
        }

        private string GetLogInSuccessHtml()
        {
            return
@"<html>
    <head>
        <title>FocusMark Login</title>
    </head>
   <body>
       <div>
            <h1>FocusMark Login Completed.</h1>
            <p>You may close this browser tab.</p>
       </div>
   </body>
</html>";
        }

        private string GetLogInFailedHtml()
        {
            return
@"<html>
    <head>
        <title>FocusMark Login</title>
    </head>
   <body>
       <div>
            <h1>FocusMark Login Failed.</h1>
            <p>You may close this browser tab.</p>
       </div>
   </body>
</html>";
        }
    }
}
