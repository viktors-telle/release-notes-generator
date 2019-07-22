using AutoMapper;
using AutoMapper.Configuration;
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
            var mce = new MapperConfigurationExpression();

            mce.AddProfile(new MappingProfile());

            var mc = new MapperConfiguration(mce);

            mc.AssertConfigurationIsValid();
        }
    }
}