using Galicia_Redis_DAL;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text;

namespace Galicia_Demo_Redis.Controllers
{
    [Route("api")]
    [ApiController]
    public class GaliciaController : ControllerBase
    {

        IDatabase _redis = Redis.RedisCache;


        #region Post

        [HttpPost]
        public void Set(string Key, string Value)
        {
            _redis.HashSet(Key, "Datos", Value);
            _redis.Multiplexer.GetSubscriber().Publish(Key, Value);
        }


        #endregion

        #region Redis 

        [HttpGet]
        [Route("GetRedis")]
        public string Get(string key)
        {
            try { return Encoding.ASCII.GetString(_redis.HashGet(key, Encoding.ASCII.GetBytes("Datos"))); }
            catch { return "No se encontro el objeto"; }
        }

        [HttpPost("updates/{id}/{value}")]
        public IActionResult Post(string id, string value)
        {
            Set(id, value);
            return Ok("Se creo correctamente la Key " + id);
        }

        #endregion
    }
}