#region

using System.ComponentModel;

#endregion

namespace comrade.Domain.Enums
{
    public enum EnumOperador
    {
        [Description("<")] Menor = 1,
        [Description(">")] Maior = 2,
        [Description("=")] Igual = 3,
        [Description("!=")] Diferente = 4,
        [Description(">=")] MaiorOuIgual = 5,
        [Description("<=")] MenorOuIgual = 6
    }
}