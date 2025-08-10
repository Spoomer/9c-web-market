using System.Text.Json;
using System.Text.Json.Serialization;
using Nekoyume.Model.Stat;

namespace NineCWebMarket.Frontend.Models.Mimir.Stats;

/// <summary>
/// <see cref="Nekoyume.Model.Stat.StatMap"/>.
/// <see cref="Nekoyume.Model.Stat.StatsMap"/>'s serialize and deserialize logic is same with <see cref="Nekoyume.Model.Stat.StatMap"/>.
/// </summary>
public record StatMap
{
    public Dictionary<StatType, DecimalStat> Value { get; set; } = [];
}

public class StatMapJsonConverter : JsonConverter<StatMap>
{
    private readonly JsonConverter<StatMap> _valueConverter;
    private readonly JsonConverter<DecimalStat> _decimalStatConverter;

    public StatMapJsonConverter(JsonSerializerOptions options)
    {
        _valueConverter = (JsonConverter<StatMap>)options
            .GetConverter(typeof(StatMap));

        _decimalStatConverter = (JsonConverter<DecimalStat>)options
            .GetConverter(typeof(DecimalStat));
    }

    public override StatMap? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        var dictionary = new Dictionary<StatType, DecimalStat>();
        reader.Read();
        if (reader.TokenType == JsonTokenType.EndObject)
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new JsonException();
        }

        string propertyName = reader.GetString() ?? "";

        if (!propertyName.Equals("Value", StringComparison.InvariantCultureIgnoreCase))
        {
            throw new JsonException();
        }

        reader.Read();
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                reader.Read();
            }
            
            if (reader.TokenType == JsonTokenType.EndObject )
            {
                return new StatMap() { Value = dictionary };
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            reader.Read();
            var keyValue = reader.GetString();
            if (!Enum.TryParse(keyValue, ignoreCase: false, out StatType key) &&
                !Enum.TryParse(keyValue, ignoreCase: true, out key))
            {
                throw new JsonException($"Unable to convert \"{propertyName}\" to Enum \"{nameof(StatType)}\".");
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            reader.Read();
            var value = ReadDecimalStat(ref reader); //_decimalStatConverter.Read(ref reader, typeof(DecimalStat), options);
            if (value == null)
            {
                throw new JsonException();
            }

            reader.Read();

            dictionary.Add(key, value);
        }

        return new StatMap() { Value = dictionary };
    }

    private static DecimalStat ReadDecimalStat(ref Utf8JsonReader reader)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }
        Dictionary<string, object> properties = new ();
        reader.Read();

        while (reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }
            string propertyName = reader.GetString()!;
            reader.Read();
            if (propertyName.Equals(nameof(DecimalStat.StatType), StringComparison.InvariantCultureIgnoreCase))
            {
                var value = reader.GetString();
                if (!Enum.TryParse(value, ignoreCase: false, out StatType statType) &&
                    !Enum.TryParse(value, ignoreCase: true, out statType))
                {
                    throw new JsonException($"Unable to convert \"{propertyName}\" to Enum \"{nameof(StatType)}\".");
                }

                properties.Add(nameof(DecimalStat.StatType), statType);
            }
            else if (propertyName.Equals(nameof(DecimalStat.AdditionalValue), StringComparison.InvariantCultureIgnoreCase))
            {
                var value = reader.GetDecimal();
                properties.Add(nameof(DecimalStat.AdditionalValue), value);
            }
            else
            {
                var value = reader.GetDecimal();
                properties.Add(nameof(DecimalStat.BaseValue), value);
            }
            reader.Read();

        }

        return new DecimalStat
        {
            StatType = (StatType)properties[nameof(DecimalStat.StatType)],
            BaseValue = (decimal)properties[nameof(DecimalStat.BaseValue)],
            AdditionalValue = (decimal)properties[nameof(DecimalStat.AdditionalValue)]
        };
    }

    // "statsMap": {
    //     "value": [
    //     {
    //         "key": "HP",
    //         "value": {
    //             "additionalValue": 112807,
    //             "baseValue": 172307,
    //             "statType": "HP"
    //         }
    //     },
    //     {
    //         "key": "ATK",
    //         "value": {
    //             "additionalValue": 17911,
    //             "baseValue": 0,
    //             "statType": "ATK"
    //         }
    //     }
    //     ]
    // }
    public override void Write(Utf8JsonWriter writer, StatMap statMap, JsonSerializerOptions options)
    {
        _valueConverter.Write(writer, statMap, options);
    }
}