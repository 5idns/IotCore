using Microsoft.AspNetCore.Mvc;

namespace IotCore.ServiceDiscovery.Consul
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController:ControllerBase
    {
        /// <summary>
        /// 服务检查
        /// </summary>
        /// <returns></returns>
        public IActionResult Get() => Ok("OK");
    }
}
