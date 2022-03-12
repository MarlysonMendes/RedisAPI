using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data
{
    public interface IRedisPlatformRepository
    {
        void CreatePlatform(Platform plat);
        Platform? GetPlatformById(string id);
        IEnumerable<Platform?>? GetAllPlatforms();
    }
}