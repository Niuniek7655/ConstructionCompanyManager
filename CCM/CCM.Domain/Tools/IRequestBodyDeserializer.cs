using System.IO;

namespace CCM.Domain.Tools
{
    public interface IRequestBodyDeserializer
    {
        T DeserializerRequest<T>(Stream stream);
    }
}
