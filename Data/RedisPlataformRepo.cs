using System.Text.Json;
using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data
{

    public class RedisPlatformRepository : IRedisPlatformRepository
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisPlatformRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public void CreatePlatform(Platform plat)
        {
            if (plat == null)
                throw new System.ArgumentNullException(nameof(plat));

            var db = _redis.GetDatabase();
            var serialPlat = JsonSerializer.Serialize(plat);
            
            //db.StringSet(plat.Id, serialPlat);  
            //db.SetAdd("PlatformSet", serialPlat);
            
            db.HashSet("PlatformHash", new HashEntry[] 
            { new HashEntry(plat.Id, serialPlat) });

        }

  public IEnumerable<Platform?>? GetAllPlatforms()
        {
            var db = _redis.GetDatabase();

            var completeSet = db.HashGetAll("PlatformHash");
            
            if (completeSet.Length > 0)
            {
                var obj = Array.ConvertAll(completeSet, val => 
                    JsonSerializer.Deserialize<Platform>(val.Value)).ToList();
                return obj;   
            }
            
            return null;
        }

        public Platform? GetPlatformById(string id)
        {
            var db = _redis.GetDatabase();

            //var plat = db.StringGet(id);

            var plat = db.HashGet("PlatformHash", id);

            if (!string.IsNullOrEmpty(plat))
            {
                return JsonSerializer.Deserialize<Platform>(plat);
            }
            return null;
        }

    }

}