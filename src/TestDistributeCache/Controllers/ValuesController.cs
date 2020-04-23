using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace TestDistributeCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IDistributedCache _distributedCache;
        public ValuesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var cacheKey = "Test";
            var cachedValue = await _distributedCache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedValue))
            {
                return Ok(cachedValue);
            }
            else
            {
                var now = DateTime.Now;
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = now.AddSeconds(3)
                };
                await _distributedCache.SetStringAsync(cacheKey, "This is cached Value.", options);
            }
                
            return Ok("This is not cached value.");
        }
    }
}