using NUnit.Framework;
using rate_api.Helpers;

namespace Tests
{
    [TestFixture]
    public class DateHelperTests
    {
        [Test]
        public void ParseIsoDate_ShouldReturnDateTimeObject()
        {
            var dateToBeParsed = "2018-03-24T10:20:48Z";
            var parsedDate = DateHelper.ParseIsoDate(dateToBeParsed);
            Assert.IsNotNull(parsedDate);
        }

        [Test]
        public void ParseIsoDate_ShouldReturnNull()
        {
            var dateToBeParsed = "Junk";
            var parsedDate = DateHelper.ParseIsoDate(dateToBeParsed);
            Assert.IsNull(parsedDate);
        }
    }
}