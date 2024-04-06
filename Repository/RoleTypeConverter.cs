using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class RoleTypeConverter : JsonConverter<RoleType>
{
    public override RoleType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string but found {reader.TokenType}.");
        }

        var stringValue = reader.GetString();
        if (Enum.TryParse<RoleType>(stringValue, ignoreCase: true, out var roleType))
        {
            return roleType;
        }

        throw new JsonException($"Invalid value '{stringValue}' for enum type {typeof(RoleType)}.");
    }

    public override void Write(Utf8JsonWriter writer, RoleType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
