using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Core
{
    public class UserSession : IUserSession
    {
        public string Token { get; set; }

        public bool IsAuthenticated => !string.IsNullOrEmpty(Token);
    }
}
