using AutoMapper;
using NUnit.Framework;
using ReleaseNotesGenerator.Mappings;

namespace ReleaseNotesGenerator.Tests
{
    [TestFixture]
    public class AutomapperTests
    {
        [Test]
        public void AssertMappingConfigurationsAreValid()
        {                       
            Mapper.Initialize(m => m.AddProfile(new MappingProfile()));
            Mapper.AssertConfigurationIsValid();            
        }
    }
}