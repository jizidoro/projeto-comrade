#region

using comrade.Application.Extensions;
using Xunit;

#endregion

namespace comrade.UnitTests.Tests.UtilTests
{
    public class StringExtensionToSnakeCaseTests
    {
        [Fact]
        public void StringExtension_ToSnakeCase()
        {
            var teste = "Last in Line";
            var objetivo = "last_in_line";

            var restult = teste.ToSnakeCase();

            Assert.NotEmpty(restult);
            Assert.Equal(restult, objetivo);
        }
    }
}