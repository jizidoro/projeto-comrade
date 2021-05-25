using System.Threading.Tasks;
using comrade.Application.Extensions;
using comrade.Application.Imports.ImportFunctions;
using comrade.UnitTests.Mocks;
using Xunit;

namespace comrade.UnitTests.Tests.ImportTests
{
    public class ReadExcelFileDomTests
    {
        private readonly ObterIFormFileMock _obterIFormFileMock = new();

        [Fact]
        public async Task ReadExcelFileDomTest()
        {
            var arquivo = await _obterIFormFileMock.Execute();

            ReadExcelFileDom.Execute(arquivo);
            
        }
    }
}