using System.Threading.Tasks;
using comrade.Application.Extensions;
using Xunit;

namespace comrade.UnitTests.Tests.UtilTests
{
    public class StringExtensionToKebabCaseTests
    {
        [Fact]
        public void StringExtension_ToKebabCase()
        {
            var teste = "Last in Line";
            var objetivo = "last-in-line";

            var restult = teste.ToKebabCase();

            Assert.NotEmpty(restult);
            Assert.Equal(restult,objetivo);
        }
    }
}