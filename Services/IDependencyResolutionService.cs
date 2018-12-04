using System.Collections.Generic;

namespace Mp.Protractors.Test.Services
{
    public interface IDependencyResolutionService
    {
        IEnumerable<string> ResolveDependencies(string nodeName);
    }
}