using System;

namespace RN.Application.Common.Exceptions
{
    public class LockedRequestException : Exception
    {
        public LockedRequestException() : base() { }
        public LockedRequestException(string message) : base(message) { }
    }
}
