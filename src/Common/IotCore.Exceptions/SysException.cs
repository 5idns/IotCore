using System;
using IotCore.Enumerations;

namespace IotCore.Exceptions
{
    public class SysException : Exception
    {
        public Status Status { get; set; }

        public ulong StatusCode => (ulong)Status;

        public SysException() : this(Status.Unknown)
        {
        }

        public SysException(ulong status) : this(status, string.Empty)
        {
        }
        public SysException(ulong status, string message) : this(status, message, new Exception(message))
        {
        }

        public SysException(ulong status, string message, Exception innerException) : base(message, innerException)
        {
            Status = (Status)status;
        }

        public SysException(Status status) : this(status, string.Empty)
        {
        }
        public SysException(Status status, string message) : this(status, message, new Exception(message))
        {
        }

        public SysException(Status status, string message, Exception innerException) : base(message, innerException)
        {
            Status = status;
        }
    }
}
