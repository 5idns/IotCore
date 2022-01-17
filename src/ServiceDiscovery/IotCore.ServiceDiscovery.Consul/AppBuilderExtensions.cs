using Consul;
using IotCore.Common.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;

namespace IotCore.ServiceDiscovery.Consul
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime, ServiceEntity serviceEntity)
        {
            //请求注册的 Consul 地址
            var consulClint = new ConsulClient(c =>
            {
                c.Address = new Uri(serviceEntity.ConsulUrl);
                if (!string.IsNullOrWhiteSpace(serviceEntity.ConsulToken))
                {
                    c.Token = serviceEntity.ConsulToken;
                }
            });

            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://{serviceEntity.IP}:{serviceEntity.Port}/api/health",//健康检查地址
                Timeout = TimeSpan.FromSeconds(5)
            };

            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = Guid.NewGuid().ToString(),
                Name = serviceEntity.ServiceName,
                Address = serviceEntity.IP,
                Port = serviceEntity.Port,
                Tags = new[] { $"urlperfix-/{serviceEntity.ServiceName}" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            //服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            //consulClint.Agent.ServiceRegister(registration).Wait();

            lifetime.ApplicationStarted.Register(async () =>
            {
                //服务启动成功时注册
                await consulClint.Agent.ServiceRegister(registration);
            });

            lifetime.ApplicationStopping.Register(async() =>
            {
                //服务停止时取消注册
                await consulClint.Agent.ServiceDeregister(registration.ID);
            });

            var k = new KVPair("Test");
            k.Value = System.Text.Encoding.UTF8.GetBytes(consulClint.Serialize());
            consulClint.KV.Put(k).Wait();

            return app;
        }
    }
}
