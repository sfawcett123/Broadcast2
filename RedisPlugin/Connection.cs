using StackExchange.Redis;
using System.Security.Cryptography.X509Certificates;


namespace RedisPlugin
{
    internal class Connection : IDisposable
    {
        private const int REDIS_TIMEOUT = 5000; // 5 seconds

        private ConnectionMultiplexer? _redis;
        private IDatabase? db = null;
        public Connection(string server , int port)
        {
            if (_redis == null)
            {
                _redis = ConnectionMultiplexer.Connect($"{server}:{port},ConnectTimeout={REDIS_TIMEOUT}");
            }
            if (db == null)
            {
                db = _redis.GetDatabase();
            }
        }

        public void Dispose()
        {
            if (_redis != null)
            {
                _redis.Close();
                _redis.Dispose();
                _redis = null;
            }
            db = null;
        }
        public bool IsConnected()
        {
            return _redis != null && _redis.IsConnected;
        }
        public void write(string key, string value)
        {
            if (db != null)
            {
                db.StringSet(key, value);
            }
        }  
        public string? read(string key)
        {
            if (db != null)
            {
                return db.StringGet(key);
            }
            return null;
        }

    }
}
