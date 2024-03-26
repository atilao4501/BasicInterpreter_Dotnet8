using Interpretador.fonte.Interpretador;

namespace Interpretador.fonte.Analisador;

public class Parser
{
    private readonly List<Token> _tokens;
    private int _posicaoAtual = 0;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }

    public Instrucao Parsear()
    {
        if (_posicaoAtual >= _tokens.Count)
        {
            throw new Exception("Fim inesperado do código");
        }

        var token = _tokens[_posicaoAtual];

        Instrucao instrucao = null;

        switch (token.Tipo)
        {
            case Token.TipoToken.Identificador:
                instrucao = AnalisarIdentificador();
                break;
            case Token.TipoToken.Numero:
                instrucao = AnalisarNumero();
                break;
            case Token.TipoToken.OperadorDivisao:
            case Token.TipoToken.OperadorSoma:
            case Token.TipoToken.OperadorMultiplicacao:
            case Token.TipoToken.OperadorSubtracao:
                instrucao = AnalisarExpressaoAritmetica();
                break;
            default:
                throw new Exception("Token Inválido: " + token.Tipo);
                
        }

        _posicaoAtual++;
        
        return instrucao;
    }

    private Instrucao AnalisarIdentificador()
    {
        var nome = _tokens[_posicaoAtual].Valor;
    }private Instrucao AnalisarNumero()
    {
        
    }private Instrucao AnalisarExpressaoAritmetica()
    {
        
    }
}