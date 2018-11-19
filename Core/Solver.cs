using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Mp.Protractors.Test.DTOs;
using System.Linq;

namespace  Mp.Protractors.Test.Core
{
    public class Solver : ISolver
    {
        public Solver ()
        {
            
        }

        public Solution Solve(IList<FactDTO> facts)
        {
            var result = new Solution();
            var factsList = new List<Fact>();
            var baseFacts = facts.Select(x => new Fact(x.Item, true));
            factsList.AddRange(baseFacts);           

            // put all dependencies to facts list
            foreach (var fact in facts)
            {
                foreach (var dependency in fact.Dependencies)
                {
                    if (!factsList.Any(x => x.Item == dependency)) 
                    {
                        var isFact = factsList.Any(x => x.Item == dependency);
                        factsList.Add(new Fact(dependency, isFact));
                    }
                }
            }

            if (!factsList.Any(x => !x.IsFact))
            {
                result.Success = false;
                result.Errors.Add(new ErrorDTO("", new List<string>(){ "No independent dependencies" }));

                return result;
            }

            // merge base facts with dependencies
            foreach (var fact in facts)
            {
                var factInList = factsList.First(x => x.Item == fact.Item);

                foreach (var dependency in fact.Dependencies)
                {
                    factInList.Dependencies.Add(factsList.First(x => x.Item == dependency));
                }
            }

            // Checking ciruclar reference conditions
            foreach (var fact in factsList)
            {
                foreach (var dependency in fact.Dependencies)
                {
                    if (dependency.Dependencies.Any(x => x.Item == fact.Item))
                    {
                        result.Errors.Add(new ErrorDTO(fact.Item, new List<string>(){ $"Circular reference to [{dependency.Item}] detected" }));
                    }
                }
            }

            if (result.Errors.Count > 0)
            {
                result.Success = false;

                return result;
            }

            // solve for each fact
            foreach (var fact in facts)
            {
                var solution = new List<Fact>();
                var factInList = factsList.First(x => x.Item == fact.Item);

                // Faster impl (for strings)
                // var solvedFact = SolveInternal(factInList);

                // Removing duplicate dependencies with Distinct
                // result.Result.Add(new FactDTO(factInList.Item, solvedFact.Select(x => x.Item).Distinct().ToList()));

                // Logically more correct impl
                var usedFacts = new List<Fact>();
                var solvedFact = SolveInternalWithTracking(factInList, usedFacts);

                result.Result.Add(new FactDTO(factInList.Item, solvedFact.Select(x => x.Item).ToList()));
            }

            return result;
        }

        private List<Fact> SolveInternal (Fact fact) 
        {
            var result = new List<Fact>();

            foreach (var dependency in fact.Dependencies)
            {
                if (!dependency.Solved)
                {
                    // need to solve at least one fact
                    if (dependency.Dependencies.Count(x => x.IsFact == true) > 0)
                    {
                        var lowerDependencies = SolveInternal(dependency);
                        dependency.Solution.Add(dependency);
                        dependency.Solution.AddRange(lowerDependencies);
                        dependency.Solved = true;

                        result.AddRange(dependency.Solution);
                    }
                    else 
                    {
                        dependency.Solved = true;
                        dependency.Solution.Add(dependency);
                        dependency.Solution.AddRange(dependency.Dependencies);
                        
                        result.AddRange(dependency.Solution);
                    }
                }
                else
                {
                    result.AddRange(dependency.Solution);
                }
                
            }

            return result;
        }

        private List<Fact> SolveInternalWithTracking (Fact fact, List<Fact> usedFacts) 
        {
            var result = new List<Fact>();

            foreach (var dependency in fact.Dependencies)
            {
                if (!dependency.Solved)
                {
                    // need to solve at least one fact
                    if (dependency.Dependencies.Count(x => x.IsFact == true) > 0)
                    {
                        var lowerDependencies = SolveInternal(dependency);
                        dependency.Solution.Add(dependency);
                        dependency.Solution.AddRange(lowerDependencies);
                        dependency.Solved = true;

                        var joined = dependency.Solution.Except(usedFacts).ToList();
     
                        result.AddRange(joined);                 
                        usedFacts.AddRange(joined);
                    }
                    else 
                    {
                        dependency.Solved = true;
                        dependency.Solution.Add(dependency);
                        dependency.Solution.AddRange(dependency.Dependencies);

                        var joined = dependency.Solution.Except(usedFacts).ToList();
                        
                        result.AddRange(joined);                 
                        usedFacts.AddRange(joined);
                    }
                }
                else
                {
                    var joined = dependency.Solution.Except(usedFacts).ToList();
                    result.AddRange(joined);
                    usedFacts.AddRange(joined);
                }
                
            }

            return result;
        }
    }
}