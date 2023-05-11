using $ext_safeprojectname$.SharedKernel.Util;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using NLog;
using System;

namespace $ext_safeprojectname$.Infrastructure.NoSql
{
    public class MongoContext
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly ILogger _logger;

        public MongoContext()
        {
            _logger = LogManager.GetCurrentClassLogger();

            BsonSerializer.RegisterSerializer(typeof(DateTime), new DepsMongoDBDateTimeSerializer());

            var mongoConnectionUrl = new MongoUrl(AmbienteUtil.GetValue("CONNECTION_STRING_MONGODB"));

            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);

            mongoClientSettings.ClusterConfigurator = cb =>
            {
                cb.Subscribe<CommandStartedEvent>(e =>
                {
                    #if DEBUG
                    _logger.Debug($"{e.CommandName} - {e.Command}");
                    #endif
                });
            };

            _client = new MongoClient(mongoClientSettings);
            _database = _client.GetDatabase(AmbienteUtil.GetValue("MONGODB_DATABASE_NAME"));
        }

        public IMongoClient Client => _client;

        public IMongoDatabase DatabaseStorage => _database;
    }

    public class DepsMongoDBDateTimeSerializer : DateTimeSerializer
    {
        public override DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return base.Deserialize(context, args).ToLocalTime();
        }
    }
}
