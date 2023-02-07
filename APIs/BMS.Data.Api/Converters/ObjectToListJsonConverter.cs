using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BMS.Data.Api.Converters
{
    /// <summary>
    /// Converter intended for usage on JSON fields with the following structure:
    /// "foo": {
    ///     "bar1": {
    ///         ...
    ///     },
    ///     "bar2": {
    ///         ...
    ///     }
    ///     ...
    /// }
    /// 
    /// The above structure will be deserialized as a List<T>, with each element being the contents of bar1, bar2 and so on.
    /// </summary>
    public class ObjectToListJsonConverter : JsonConverter
    {
        private readonly Type[] _types;

        public ObjectToListJsonConverter() { }

        public ObjectToListJsonConverter(params Type[] types)
        {
            _types = types;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return string.Empty;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                return serializer.Deserialize(reader, objectType);
            }
            else
            {
                JObject obj = JObject.Load(reader);
                if (obj.HasValues)
                {
                    var genericType = objectType.GenericTypeArguments[0];
                    dynamic values = Activator.CreateInstance(objectType);
                    foreach (var child in obj.Children())
                    {
                        if (child != null)
                        {
                            dynamic value = Activator.CreateInstance(genericType);
                            serializer.Populate(((JProperty)child).Value.CreateReader(), value);
                            values.Add(value);
                        }
                    }

                    return values;
                }

                return string.Empty;
            }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(List<>)));
        }
    }
}
