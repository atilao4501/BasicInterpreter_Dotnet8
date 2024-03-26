using Interpretador.fonte.Interpretador;
using System.Text.RegularExpressions;

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

    private Token Atual()
    {
        return _tokens[_posicaoAtual];
    }

    private Token Anterior()
    {
        return _tokens[_posicaoAtual - 1];
    }

    private int ParseSomaSubtracao()
    {
        var resultado = ParseMultiplicacaoDivisao();

        while (Corresponde(Token.TipoToken.OperadorSoma) || Corresponde(Token.TipoToken.OperadorSubtracao))
        {
            Token operador = Anterior();
            int direita = ParseMultiplicacaoDivisao();
            if (operador.Tipo == Token.TipoToken.OperadorSoma) 
            { 
                resultado += direita; 
            }

            else if (operador.Tipo == Token.TipoToken.OperadorSubtracao) 
            { 
                resultado -= direita; 
            }
                
        }

        return resultado;
    }

    private int ParseMultiplicacaoDivisao()
    {
        int resultado = ParseNumero();

        while (Corresponde(Token.TipoToken.OperadorMultiplicacao) || Corresponde(Token.TipoToken.OperadorDivisao))
        {
            Token operador = Anterior();
            int direita = ParseNumero();

            if (operador.Tipo == Token.TipoToken.OperadorMultiplicacao)
            {
                resultado *= direita;
            }

            if (operador.Tipo == Token.TipoToken.OperadorDivisao)
            {
                resultado /= direita;
            }
        }
        return resultado;
    }

    private int ParseNumero()
    {
        if (Corresponde(Token.TipoToken.Numero))
        {
            return int.Parse(Anterior().Valor);
        }

        throw new Exception("Erro de sintaxe: esperava um número");
    }


    private bool Corresponde(Token.TipoToken tipo)
    {
        if (EstaNoFinal()) { return false; }

        if (_tokens[_posicaoAtual].Tipo != tipo) return false;

        _posicaoAtual++;
        return true;
    }

    private bool EstaNoFinal()
    {
        return _posicaoAtual >= _tokens.Count;
    }
}