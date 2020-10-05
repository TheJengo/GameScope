using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Infra.Common.Auth
{
    public class JsonWebToken
    {
        public string Token { get; set; }
        public long Expiration { get; set; }
    }
}
