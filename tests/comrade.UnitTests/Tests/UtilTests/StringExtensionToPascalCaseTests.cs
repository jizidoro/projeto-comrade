using System.Threading.Tasks;
using comrade.Application.Extensions;
using Xunit;

namespace comrade.UnitTests.Tests.UtilTests
{
    public class StringExtensionToPascalCaseTests
    {
        [Fact]
        public void StringExtension_ToPascalCase()
        {
            var teste = "Last in Line";
            var objetivo = "LastInLine";

            var restult = teste.ToPascalCase();

            Assert.NotEmpty(restult);
            Assert.Equal(restult,objetivo);
        }
    }
}