using CCM.Domain.Tools;
using Newtonsoft.Json;
using System.IO;
using System;
using Microsoft.Extensions.Logging;
using CCM.Constants;
using Microsoft.Extensions.Options;

namespace CCM.Model.Tools
{
    public class RequestBodyDeserializer : IRequestBodyDeserializer
    {
        private readonly ILogger<RequestBodyDeserializer> _logger;
        private readonly RequestBodyDeserializerData _data;
        public RequestBodyDeserializer(ILogger<RequestBodyDeserializer> logger, IOptions<RequestBodyDeserializerData> settings)
        {
            _logger = logger;
            _data = settings.Value;
        }

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
                throw new Exception(_data.DeserializeError);
            }
            return result;
        }
    }
}
