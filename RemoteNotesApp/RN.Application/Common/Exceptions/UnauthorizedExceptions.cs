using System;

namespace RN.Application.Common.Exceptions
{
    public class UnauthorizedExceptions : Exception
    {
        public UnauthorizedExceptions() : base("User don't auth") { }
        public UnauthorizedExceptions(string message) : base(message) { }
    }
}
