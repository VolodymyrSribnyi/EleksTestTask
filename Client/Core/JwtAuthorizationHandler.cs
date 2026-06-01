using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Core
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private readonly IUserSession _userSession;
        public JwtAuthorizationHandler(IUserSession userSession)
        {
            _userSession = userSession;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(_userSession.IsAuthenticated)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _userSession.Token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
