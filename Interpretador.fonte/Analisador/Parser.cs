using Interpretador.fonte.Interpretador;

namespace Interpretador.fonte.Analisador;

public class Parser
{
    private readonly List<Token> _tokens = new List<Token>();
    private int _posicaoAtual;


    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _posicaoAtual = 0;
    }

    public int ParseExpressao()
    {
        return ParseSomaSubtracao();
    }

    private int ParseSomaSubtracao()
    {
        var resultado = ParseMultiplicacaoDivisao();
        
        while (Match(Token.TipoToken.OperadorSoma) || Match(Token.TipoToken.OperadorSubtracao))
        {
            Token operador = Previous();
            int direita = ParseMultiplicacaoDivisao();
            if (operador.Tipo == Token.TipoToken.OperadorSoma)
                resultado += direita;
            else if (operador.Tipo == Token.TipoToken.OperadorSubtracao)
                resultado -= direita;
        }
    }
    
    private int ParseNumero()
    {
        if (Match(Token.TipoToken.Numero))
            return int.Parse(Previous().Lexema);

        throw new Exception("Erro de sintaxe: esperava um número.");
    }

    // Métodos auxiliares para manipulação dos tokens
    private Token Peek()
    {
        return _tokens[_posicaoAtual];
    }

    private Token Previous()
    {
        return _tokens[_posicaoAtual - 1];
    }

    private bool Match(Token.TipoToken tipo)
    {
        if (IsAtEnd()) return false;
        if (_tokens[_posicaoAtual].Tipo != tipo) return false;
        _posicaoAtual++;
        return true;
    }

    private bool IsAtEnd()
    {
        return _posicaoAtual >= _tokens.Count;
    }
}  