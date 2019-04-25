using System.Collections.Generic;

namespace Mp.Protractors.Test.Core.DependencyResolution
{
    public interface IDependencyContainer
    {
        void Register(object item, params object[] dependencies);
        IEnumerable<object> ResolveAllDependencies(object item);
    }
}