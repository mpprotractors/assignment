using Mp.Protractors.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mp.Protractors.BLL.IServices
{
    public interface IDependencyResolverService
    {
        /// <summary>
        /// Resolves dependencies for a given target from a list of dependencies.
        /// </summary>
        /// <param name="dependencyList">List of items against which the dependency resolving will run.</param>
        /// <param name="target">The target that whose dependencies will be resolved.</param>
        /// <returns></returns>
        void ResolveTargetDependencies(DependencyItem target, List<DependencyItem> dependencyList);

        /// <summary>
        /// Returns List of unresolved dependency items from a raw string input.
        /// </summary>
        /// <param name="input">Dependency list as a string</param>
        /// <returns></returns>
        List<DependencyItem> ParseRawInput(string input);

        void ResolveDependencies(List<DependencyItem> items);
    }
}
