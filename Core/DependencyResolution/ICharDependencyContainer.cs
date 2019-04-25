using System.Collections.Generic;

namespace Mp.Protractors.Test.Core.DependencyResolution
{
    public interface ICharDependencyContainer
    {
        IReadOnlyList<Descriptor> GetDescriptors();
        IReadOnlyList<Descriptor> GetDescriptorsIncludingTransitiveDependencies();
        void Register(Descriptor descriptor);
        void Register(char item, params char[] dependencies);
    }
}