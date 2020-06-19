using CCM.Domain.Tools;
using Newtonsoft.Json;
using System.IO;
using System;
using Microsoft.Extensions.Logging;

namespace CCM.Model.Tools
{
    public class RequestBodyDeserializer : IRequestBodyDeserializer
    {
        private readonly ILogger<RequestBodyDeserializer> _logger;
        public RequestBodyDeserializer(ILogger<RequestBodyDeserializer> logger)
        {
            _logger = logger;
        }

        private const string deserializeError = "Incorrect type of serialization";
        public T DeserializerRequest<T>(Stream stream)
        {
            string stringContent = null;
            using (StreamReader reader = new StreamReader(stream))
            {
                stringContent = reader.ReadToEnd();
            }
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            T result = default(T);
            try
            {
                result = JsonConvert.DeserializeObject<T>(stringContent, settings);
            }
            catch(JsonSerializationException ex)
            {
                _logger.LogInformation(ex.Message);
                throw new Exception(deserializeError);
            }
            return result;
        }
    }
}
