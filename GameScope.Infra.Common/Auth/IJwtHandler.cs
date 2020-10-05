using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Infra.Common.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(int userId);
    }
}
