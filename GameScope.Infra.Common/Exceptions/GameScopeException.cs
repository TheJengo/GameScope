using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Infra.Common.Exceptions
{
    public class GameScopeException : Exception
    {
        public string Code { get; set; }

        public GameScopeException()
        {
        }

        public GameScopeException(string code)
        {
            Code = code;
        }

        public GameScopeException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        public GameScopeException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public GameScopeException(Exception innerException, string message, params object[] args) : this(innerException, string.Empty, message, args)
        {
        }

        public GameScopeException(Exception innerException, string code, string message, params object[] args) : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
