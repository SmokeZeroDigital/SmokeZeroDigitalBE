namespace SmokeZeroDigitalProject.Common.Converter
{
    public class FlexibleDateTimeConverterFactory : JsonConverterFactory
    {
        private readonly string _format;
        private readonly bool _alwaysAssumeUtcOnRead;

        public FlexibleDateTimeConverterFactory(string format = "yyyy-MM-dd HH:mm:ss", bool alwaysAssumeUtcOnRead = false)
        {
            _format = format;
            _alwaysAssumeUtcOnRead = alwaysAssumeUtcOnRead;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(DateTime) || typeToConvert == typeof(DateTime?);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(DateTime))
                return new InternalFlexibleDateTimeConverter(_format, _alwaysAssumeUtcOnRead, false);

            if (typeToConvert == typeof(DateTime?))
                return new NullableFlexibleDateTimeConverter(_format, _alwaysAssumeUtcOnRead);

            throw new NotSupportedException($"Cannot convert {typeToConvert} to DateTime or DateTime?");
        }

        private class InternalFlexibleDateTimeConverter : JsonConverter<DateTime>
        {
            private readonly string _format;
            private readonly bool _alwaysAssumeUtcOnRead;

            public InternalFlexibleDateTimeConverter(string format, bool alwaysAssumeUtcOnRead, bool isNullable)
            {
                _format = format;
                _alwaysAssumeUtcOnRead = alwaysAssumeUtcOnRead;
            }

            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                    throw new JsonException("Cannot convert null to non-nullable DateTime.");

                if (reader.TokenType == JsonTokenType.String)
                {
                    var dateString = reader.GetString();
                    if (string.IsNullOrWhiteSpace(dateString))
                        throw new JsonException("Cannot convert empty string to DateTime.");

                    if (DateTime.TryParseExact(dateString, _format, CultureInfo.InvariantCulture,
                        _alwaysAssumeUtcOnRead ? DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal : DateTimeStyles.None,
                        out var result))
                        return result;

                    if (DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                        return _alwaysAssumeUtcOnRead ? DateTime.SpecifyKind(result, DateTimeKind.Utc) : result;

                    throw new JsonException($"Unable to parse date string '{dateString}' with format '{_format}'.");
                }

                try
                {
                    var value = reader.GetDateTime();
                    return _alwaysAssumeUtcOnRead ? DateTime.SpecifyKind(value, DateTimeKind.Utc) : value;
                }
                catch (FormatException)
                {
                    throw new JsonException($"Unexpected token type {reader.TokenType} or format for DateTime.");
                }
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToUniversalTime().ToString(_format, CultureInfo.InvariantCulture));
            }
        }

        private class NullableFlexibleDateTimeConverter : JsonConverter<DateTime?>
        {
            private readonly string _format;
            private readonly bool _alwaysAssumeUtcOnRead;

            public NullableFlexibleDateTimeConverter(string format, bool alwaysAssumeUtcOnRead)
            {
                _format = format;
                _alwaysAssumeUtcOnRead = alwaysAssumeUtcOnRead;
            }

            public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                    return null;

                if (reader.TokenType == JsonTokenType.String)
                {
                    var dateString = reader.GetString();
                    if (string.IsNullOrWhiteSpace(dateString))
                        return null;

                    if (DateTime.TryParseExact(dateString, _format, CultureInfo.InvariantCulture,
                        _alwaysAssumeUtcOnRead ? DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal : DateTimeStyles.None,
                        out var result))
                        return result;

                    if (DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                        return _alwaysAssumeUtcOnRead ? DateTime.SpecifyKind(result, DateTimeKind.Utc) : result;

                    throw new JsonException($"Unable to parse date string '{dateString}' with format '{_format}'.");
                }

                try
                {
                    var value = reader.GetDateTime();
                    return _alwaysAssumeUtcOnRead ? DateTime.SpecifyKind(value, DateTimeKind.Utc) : value;
                }
                catch (FormatException)
                {
                    throw new JsonException($"Unexpected token type {reader.TokenType} or format for DateTime.");
                }
            }

            public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
            {
                if (value == null)
                {
                    writer.WriteNullValue();
                    return;
                }
                writer.WriteStringValue(value.Value.ToUniversalTime().ToString(_format, CultureInfo.InvariantCulture));
            }
        }
    }
}
