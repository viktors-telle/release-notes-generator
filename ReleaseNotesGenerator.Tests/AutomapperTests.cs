using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReleaseNotesGenerator.Mappings;

namespace ReleaseNotesGenerator.Tests
{
    [TestClass]
    public class AutomapperTests
    {
        [TestMethod]
        public void AssertMappingConfigurationsAreValid()
        {                       
            Mapper.Initialize(m => m.AddProfile(new MappingProfile()));
            Mapper.AssertConfigurationIsValid();            
        }
    }
}