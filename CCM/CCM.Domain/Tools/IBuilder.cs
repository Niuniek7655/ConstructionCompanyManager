using System;

namespace CCM.Domain.Tools
{
    public interface IBuilder
    {
        T Build<T>() where T : class;
    }
}
