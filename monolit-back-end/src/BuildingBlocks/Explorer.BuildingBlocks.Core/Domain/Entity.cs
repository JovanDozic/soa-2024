using System.Text.Json.Serialization;

namespace Explorer.BuildingBlocks.Core.Domain;

public abstract class Entity
{
    [JsonInclude]
    public long Id { get; init; }

    public override bool Equals(object? obj)
    {
        return obj is Entity entity && Id.Equals(entity.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}