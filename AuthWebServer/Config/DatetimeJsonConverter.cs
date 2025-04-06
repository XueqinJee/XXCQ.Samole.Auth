﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuthWebServer.Config {
    public class DatetimeJsonConverter : JsonConverter<DateTime> {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            if (reader.TokenType == JsonTokenType.String) {
                if (DateTime.TryParse(reader.GetString(), out DateTime dateTime)) return dateTime;
            }
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
