using Xunit;
using Mp.Protractors.Test.Core;
using System.Collections.Generic;
using Mp.Protractors.Test.DTOs;

namespace Mp.Protractors.Test.Tests
{
    public class SolverTests
    {
        public ISolver _solver;

        public SolverTests()
        {
            _solver = new Solver();
        }

        [Fact]
        public void RequirementsTest()
        {
            var facts = new List<FactDTO>()
            {
                new FactDTO("A", new List<string>{"B", "C"}),
                new FactDTO("B", new List<string>{"C", "E"}),
                new FactDTO("C", new List<string>{"G"}),
                new FactDTO("D", new List<string>{"A", "F"}),
                new FactDTO("E", new List<string>{"F"}),
                new FactDTO("F", new List<string>{"H"}),
            };

            var expectedResult = new List<FactDTO>()
            {
                new FactDTO("A", new List<string>{"B", "C", "G", "E", "F", "H"}),
                new FactDTO("B", new List<string>{"C", "G", "E", "F", "H"}),
                new FactDTO("C", new List<string>{"G"}),
                new FactDTO("D", new List<string>{"A", "B", "C", "G", "E", "F", "H"}),
                new FactDTO("E", new List<string>{"F", "H"}),
                new FactDTO("F", new List<string>{"H"}),
            };

            var result = _solver.Solve(facts);

            Assert.True(result.Success);
            Assert.Equal(expectedResult.Count, result.Result.Count);

            for (var i = 0; i < result.Result.Count; i++)
            {
                Assert.Equal(expectedResult[i].Item, result.Result[i].Item);

                for (var j = 0; j < expectedResult[i].Dependencies.Count; j++)
                {
                    Assert.Equal(expectedResult[i].Dependencies[j], result.Result[i].Dependencies[j]);
                }
            }
        }

        [Fact]
        public void MinimalTest()
        {
            var facts = new List<FactDTO>()
            {
                new FactDTO("A", new List<string>{"B", "C"}),
                new FactDTO("B", new List<string>{"C", "E"}),
                new FactDTO("C", new List<string>{"G"}),
            };

            var expectedResult = new List<FactDTO>()
            {
                new FactDTO("A", new List<string>{"B", "C", "G", "E"}),
                new FactDTO("B", new List<string>{"C", "G", "E"}),
                new FactDTO("C", new List<string>{"G"}),
            };

            var result = _solver.Solve(facts);

            Assert.True(result.Success);
            Assert.Equal(expectedResult.Count, result.Result.Count);

            for (var i = 0; i < result.Result.Count; i++)
            {
                Assert.Equal(expectedResult[i].Item, result.Result[i].Item);

                for (var j = 0; j < expectedResult[i].Dependencies.Count; j++)
                {
                    Assert.Equal(expectedResult[i].Dependencies[j], result.Result[i].Dependencies[j]);
                }
            }
        }

        [Fact]
        public void NoIndependentDependenciesTest()
        {
            var facts = new List<FactDTO>()
            {
                new FactDTO("A", new List<string>{"B", "C"}),
                new FactDTO("B", new List<string>{"C", "A"}),
                new FactDTO("C", new List<string>{"B"}),
            };

            var result = _solver.Solve(facts);

            Assert.False(result.Success);
        }

        [Fact]
        public void CircularReferenceTest()
        {
            var facts = new List<FactDTO>()
            {
                new FactDTO("A", new List<string>{"B", "C"}),
                new FactDTO("B", new List<string>{"C", "A"}),
            };

            var result = _solver.Solve(facts);

            Assert.False(result.Success);
        }
    }
}