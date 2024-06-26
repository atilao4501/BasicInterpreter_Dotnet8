namespace Interpretador.fonte;

public class Token
{
    public enum TipoToken
    {
        Identificador,
        Numero,
        OperadorSoma,
        OperadorSubtracao,
        OperadorMultiplicacao,
        OperadorDivisao,
        OperadorAtribuicao,
        PRINT,
        LET,
        String,
        NumLinha
    }

    public TipoToken Tipo { get; set; }
    public string? Valor { get; set; }

    public Token(TipoToken tipo, string valor)
    {
        Tipo = tipo;
        Valor = valor;
    }

}