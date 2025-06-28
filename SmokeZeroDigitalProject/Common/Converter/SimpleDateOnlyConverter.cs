using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SmokeZeroDigitalProject.Common.Converter
{
    public class SimpleDateOnlyConverter : JsonConverter<DateTime>
    {
        private const string Format = "yyyy-MM-dd"; 

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                throw new JsonException("Cannot convert null to non-nullable DateTime.");

            if (reader.TokenType == JsonTokenType.String)
            {
                var dateString = reader.GetString();
                if (string.IsNullOrWhiteSpace(dateString))
                    throw new JsonException("Cannot convert empty string to DateTime.");

                if (DateTime.TryParseExact(dateString, Format, CultureInfo.InvariantCulture,
                                           DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                                           out var result))
                    return result;

                throw new JsonException($"Unable to parse date string '{dateString}' with format '{Format}'.");
            }
            throw new JsonException($"Unexpected token type {reader.TokenType} for DateTime.");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString(Format, CultureInfo.InvariantCulture));
        }
    }

    public class SimpleNullableDateOnlyConverter : JsonConverter<DateTime?>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            if (reader.TokenType == JsonTokenType.String)
            {
                var dateString = reader.GetString();
                if (string.IsNullOrWhiteSpace(dateString))
                    return null;

                if (DateTime.TryParseExact(dateString, Format, CultureInfo.InvariantCulture,
                                           DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                                           out var result))
                    return result;

                throw new JsonException($"Unable to parse date string '{dateString}' with format '{Format}'.");
            }
            throw new JsonException($"Unexpected token type {reader.TokenType} for DateTime?");
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToUniversalTime().ToString(Format, CultureInfo.InvariantCulture));
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }

    public class SimpleIsoDateTimeConverter : JsonConverter<DateTime>
    {
        private const string Format = "yyyy-MM-ddTHH:mm:ss.fffZ"; // ISO 8601 UTC

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                throw new JsonException("Cannot convert null to non-nullable DateTime.");
            var value = reader.GetDateTime();
            return DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString(Format));
        }
    }

    public class SimpleNullableIsoDateTimeConverter : JsonConverter<DateTime?>
    {
        private const string Format = "yyyy-MM-ddTHH:mm:ss.fffZ"; // ISO 8601 UTC

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;
            var value = reader.GetDateTime();
            return DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToUniversalTime().ToString(Format));
            else
                writer.WriteNullValue();
        }
    }
}