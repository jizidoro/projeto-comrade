﻿#region

using comrade.Application.Extensions;
using Xunit;

#endregion

namespace comrade.UnitTests.Tests.UtilTests
{
    public class StringExtensionToCamelCaseTests
    {
        [Fact]
        public void StringExtension_ToCamelCase()
        {
            var teste = "Last in Line";
            var objetivo = "lastInLine";

            var restult = teste.ToCamelCase();

            Assert.NotEmpty(restult);
            Assert.Equal(restult, objetivo);
        }
    }
}