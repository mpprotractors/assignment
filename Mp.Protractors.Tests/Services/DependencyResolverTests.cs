using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mp.Protractors.BLL.IServices;
using Mp.Protractors.BLL.Services;
using Mp.Protractors.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mp.Protractors.Tests.Services
{
    [TestClass]
    public class DependencyResolverTests
    {
        private IDependencyResolverService _fakeService;

        #region TestData

        private readonly string _rawInputStringValid = @"A -> BC
                                            B -> CE
                                            C -> G
                                            D -> AF
                                            E -> F
                                            F -> H";

        private readonly string _rawInputStringInvalid = @"A -> BC
                                            B -> 
                                            C -> */
                                            D -> @@@
                                            E -> F
                                            F -> H";

        private readonly List<List<char>> _resolvedDependencies = new List<List<char>>()
        {
            new List<char>() { 'B','C','E','F','G','H' },
            new List<char>() { 'C','E','F','G','H' },
            new List<char>() { 'G' },
            new List<char>() { 'A', 'B','C','E','F','G','H' },
            new List<char>() { 'F', 'H' },
            new List<char>() { 'H' },
        };


        private readonly List<DependencyItem> _resolvedInputValid = new List<DependencyItem>()
        {
            new DependencyItem() { Name = 'A', RawDependencyList = "BC"},
            new DependencyItem() { Name = 'B', RawDependencyList = "CE"},
            new DependencyItem() { Name = 'C', RawDependencyList = "G"},
            new DependencyItem() { Name = 'D', RawDependencyList = "AF"},
            new DependencyItem() { Name = 'E', RawDependencyList = "F"},
            new DependencyItem() { Name = 'F', RawDependencyList = "H"},
        };

        private readonly List<DependencyItem> _resolvedInputInvalid = new List<DependencyItem>()
        {
            new DependencyItem() { Name = 'A', RawDependencyList = "BC"},
            new DependencyItem() { Name = 'B', RawDependencyList = "CE"},
            new DependencyItem() { Name = 'C', RawDependencyList = "A"}, // Should cause a circular dependency.
            new DependencyItem() { Name = 'D', RawDependencyList = "AF"},
            new DependencyItem() { Name = 'E', RawDependencyList = "F"},
            new DependencyItem() { Name = 'F', RawDependencyList = "H"},
        };

        #endregion

        [TestInitialize]
        public void Initialize()
        {
            _fakeService = new Mock<DependencyResolverService>().Object;
        }

        [TestMethod]
        public void DependencyResolverService_ParseRawInput_ShouldReturnListOfDependencies()
        {
            var result = _fakeService.ParseRawInput(_rawInputStringValid);

            Assert.IsTrue(result.OrderBy(x => x.Name).Select(x => x.Name).SequenceEqual(_resolvedInputInvalid.Select(x => x.Name)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DependencyResolverService_ParseRawInput_ShouldThrowUp()
        {
            _fakeService.ParseRawInput(_rawInputStringInvalid);
        }

        [TestMethod]
        public void DependencyResolverService_ResolveDependencies_ShouldResolveDependenciesForTarget()
        {
            var target = _resolvedInputValid.First();

            var resolvedDependencies = new List<char>() { 'B', 'C', 'E', 'F', 'G', 'H' };
            _fakeService.ResolveTargetDependencies(target, _resolvedInputValid);


            Assert.IsTrue(target.ParsedDependencyList.OrderBy(x => x).SequenceEqual(resolvedDependencies.OrderBy(x => x)));
        }


        [TestMethod]
        public void DependencyResolverService_ResolveDependencies_ShouldResolveDependencies()
        {
            _fakeService.ResolveDependencies(_resolvedInputValid);

            for (int i = 0; i < _resolvedDependencies.Count; i++)
            {
                Assert.IsTrue(_resolvedInputValid[i].ParsedDependencyList.OrderBy(x => x).SequenceEqual(_resolvedDependencies[i].OrderBy(x => x)));
            }
        }

        [TestMethod]
        public void DependencyResolverService_ResolveDependencies_ShouldResolveCircularDependency()
        {
            var target = _resolvedInputInvalid[2];

            var resolvedDependencies = new List<char>() { 'B', 'C', 'E', 'F', 'A', 'H' };

            _fakeService.ResolveTargetDependencies(target, _resolvedInputInvalid);

            Assert.IsTrue(target.ParsedDependencyList.OrderBy(x => x).SequenceEqual(resolvedDependencies.OrderBy(x => x)));
        }
    }
}
