namespace Explorer.BuildingBlocks.Core.Domain
{
    public abstract class JsonEntity: Entity
    {
        [Newtonsoft.Json.JsonIgnore]
        public string? JsonObject { get; protected set; }

        public abstract void ToJson();
        public abstract void FromJson();
    }
}
