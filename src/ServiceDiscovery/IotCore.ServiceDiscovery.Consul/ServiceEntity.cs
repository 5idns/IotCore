namespace IotCore.ServiceDiscovery.Consul
{
    public class ServiceEntity
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// Consul地址
        /// </summary>
        public string ConsulUrl { get; set; }

        /// <summary>
        /// Consul ACL Token
        /// </summary>
        public string ConsulToken { get; set; }
    }
}
