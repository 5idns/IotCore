using System;
using System.Runtime.Serialization;
using IotCore.Common.Enumeration;

namespace IotCore.Common.Entities
{
    /// <summary>
    /// 通用结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class CommonResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public Status Status { get; set; }

        /// <summary>
        /// 状态描述信息
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        public CommonResult() : this(Status.Unknown, string.Empty)
        {
        }

        public CommonResult(Status status) : this(status, string.Empty)
        {
        }

        public CommonResult(Status status, string message)
        {
            Status = status;
            Message = message;
        }
    }

    /// <summary>
    /// 通用结果
    /// </summary>
    public class CommonResult<TData> : CommonResult where TData : class
    {
        /// <summary>
        /// 数据
        /// </summary>
        [DataMember]
        public TData Data { get; set; }

        public CommonResult() : this(Status.Unknown, string.Empty, null)
        {
        }

        public CommonResult(Status status) : this(status, string.Empty, null)
        {
        }

        public CommonResult(Status status, string message) : this(status, message, null)
        {
        }

        public CommonResult(Status status, string message, TData data) : base(status, message)
        {
            Data = data;
        }
    }
}
