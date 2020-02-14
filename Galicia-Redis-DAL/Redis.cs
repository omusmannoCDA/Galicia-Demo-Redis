using StackExchange.Redis;
using System;

namespace Galicia_Redis_DAL
{
    public class Redis
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        static Redis()
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" }
            };

            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();
    }
}
