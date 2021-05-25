#region

using System.Collections;
using System.Collections.Generic;
using comrade.Application.Dtos;

#endregion

namespace comrade.UnitTests.Tests.AutenticacaoTests.TestDatas
{
    internal class AutenticacaoDtoTestData : IEnumerable<object[]>
    {
        #region TestData

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                200, new AutenticacaoDto
                {
                    Chave = "1",
                    Senha = "123456"
                }
            };
            yield return new object[]
            {
                400, new AutenticacaoDto
                {
                    Chave = "",
                    Senha = "123456"
                }
            };
            yield return new object[]
            {
                1001, new AutenticacaoDto
                {
                    Chave = "3",
                    Senha = ""
                }
            };
            yield return new object[]
            {
                1001, new AutenticacaoDto
                {
                    Chave = "4",
                    Senha = "1234567"
                }
            };
        }

        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}