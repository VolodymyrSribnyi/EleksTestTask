using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Core
{
    public interface IUserSession
    {
        string Token { get; set; }
        bool IsAuthenticated { get; }
    }
}
