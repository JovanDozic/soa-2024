using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.RegularExpressions;
using Explorer.BuildingBlocks.Core.Domain;
using Newtonsoft.Json;

namespace Explorer.BuildingBlocks.Infrastructure.Database
{
    public class DbEntity<TEntity> : Entity
        where TEntity : Entity
    {
        public string JsonObject { get; protected set; }

        public DbEntity() {}

        public DbEntity(TEntity entity)
        {
            JsonObject = ToJson(entity);
        }
        private string ToJson(TEntity entity)
        {
            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            return JsonConvert.SerializeObject(entity, settings);
        }
        public TEntity FromJson()
        {
            return JsonConvert.DeserializeObject<TEntity>(JsonObject) ?? throw new Newtonsoft.Json.JsonException("Exception! Cannot deserialize object!");
        }
    }
}
