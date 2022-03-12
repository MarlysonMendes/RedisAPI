using RedisAPI.Data;
using RedisAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CacheService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IRedisPlatformRepository _repository;

        public PlatformsController(IRedisPlatformRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Platform>> GetPlatforms()
        {
            return Ok(_repository.GetAllPlatforms());
        }

        [HttpGet("{id}", Name="GetPlatformById")]
        public ActionResult<IEnumerable<Platform>> GetPlatformById(string id)
        {
            
            var platform = _repository.GetPlatformById(id);
            
            if (platform != null)
            {
                return Ok(platform);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult <Platform> CreatePlatform(Platform platform)
        {
            _repository.CreatePlatform(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new {Id = platform.Id}, platform);
        }
    }
}