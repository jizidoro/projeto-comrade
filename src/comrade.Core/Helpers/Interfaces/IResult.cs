namespace comrade.Core.Helpers.Interfaces
{
    public interface IResult
    {
        bool Sucesso { get; set; }
        int Codigo { get; set; }
        string Mensagem { get; set; }
    }
}