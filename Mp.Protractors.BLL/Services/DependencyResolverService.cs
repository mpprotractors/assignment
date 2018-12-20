using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mp.Protractors.BLL.IServices;
using Mp.Protractors.Entities.Models;

namespace Mp.Protractors.BLL.Services
{
    public class DependencyResolverService : IDependencyResolverService
    {
        private const string DELIMETER_TOKEN = "->";

        private readonly Regex _listParseExp = new Regex($@"\b\w\s?{DELIMETER_TOKEN}\s?\w{"{1,}"}");
        private readonly Regex _itemParseExp = new Regex($@"^(\w)\s?{DELIMETER_TOKEN}\s?(\w{"{1,}"})$");

        /// <summary>
        /// Needed to find out how many items are defined in the original input. 
        /// If parsed items contain less items than in the sanity check we should prevent further execution.
        /// </summary>
        private readonly Regex _sanityCheckExp = new Regex($@"\w\s?{DELIMETER_TOKEN}");

        public List<DependencyItem> ParseRawInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("Input is empty");

            var parsedInput = _listParseExp.Matches(input);

            var countOfDependencies = _sanityCheckExp.Matches(input);

            if (parsedInput.Count != countOfDependencies.Count)
                throw new ArgumentException("Input contains items that have invalid dependency list.");

            var result = new List<DependencyItem>();

            foreach (Match item in parsedInput)
            {
                var itemParts = _itemParseExp.Match(item.Value);

                // I feel like this sanity check should be there 
                // even though using regex 'ensures' that bad input won't pass through
                if (itemParts.Groups.Count != 3)
                    throw new ArgumentException("One of the items in the list has no dependencies. This shouldn't happen.");

                result.Add(new DependencyItem() { Name = itemParts.Groups[1].Value[0], RawDependencyList = itemParts.Groups[2].Value });
            }

            return result;
        }

        public void ResolveDependencies(List<DependencyItem> items)
        {
            foreach (var target in items)
            {
                ResolveTargetDependencies(target, items);
            }
        }

        public void ResolveTargetDependencies(DependencyItem target, List<DependencyItem> dependencyList)
        {
            List<char> result = new List<char>();

            foreach (var item in target.RawDependencyList)
            {
                if (!result.Contains(item))
                    result.Add(item);

                if (!dependencyList.Any(x => x.Name == item))
                    continue;

                ResolveDependencies(dependencyList.First(x => x.Name == item), dependencyList, result);
            }

            target.ParsedDependencyList = String.Concat(result);
        }

        private void ResolveDependencies(DependencyItem target, List<DependencyItem> mainList, List<char> resolvedDependencies)
        {
            if (!resolvedDependencies.Contains(target.Name))
                resolvedDependencies.Add(target.Name);

            if (target.RawDependencyList.All(x => resolvedDependencies.Contains(x)))
                return;

            foreach (var item in target.RawDependencyList)
            {
                if (!mainList.Any(x => x.Name == item))
                {
                    if (!resolvedDependencies.Contains(item))
                        resolvedDependencies.Add(item);
                    continue;
                }
                    
                ResolveDependencies(mainList.First(x => x.Name == item), mainList, resolvedDependencies);
            }
        }
    }
}
