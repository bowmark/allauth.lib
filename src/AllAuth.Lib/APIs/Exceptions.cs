using System;

namespace AllAuth.Lib.APIs
{
    public class RequestException : Exception
    {
        public string ErrorCode { get; protected set; }
        public RequestException(string message) : base(message) { }
        public RequestException(string code, string message) : base(message) { ErrorCode = code; }
    }

    public class NetworkErrorException : RequestException
    {
        public NetworkErrorException() : base("Network error") { }
        public NetworkErrorException(string message) : base(message) { }
    }

    public class BadRequestException : RequestException
    {
        public BadRequestException() : base("Bad request") { }
        public BadRequestException(string message) : base(message) { }
    }

    public class UnauthorizedException : RequestException
    {
        public UnauthorizedException() : base("Unauthorized") { }
        public UnauthorizedException(string message) : base(message) { }
    }

    public class NotFoundException : RequestException
    {
        public NotFoundException() : base("Not found") { }
        public NotFoundException(string message) : base(message) { }
    }

    public class ConflictException : RequestException
    {
        public ConflictException() : base("Conflict") { }
        public ConflictException(string message) : base(message) { }
        public ConflictException(string code, string message) : base(code, message) { }
    }
}
