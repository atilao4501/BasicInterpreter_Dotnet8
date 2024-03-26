namespace Interpretador.fonte.Analisador;

public class Lexer
{
    private readonly string _codigoFonte;
    private int _posicaoAtual;
    private List<Token> _tokens;

    public Lexer(string codigoFonte)
    {
        _codigoFonte = codigoFonte;
        _posicaoAtual = 0;
        _tokens = new List<Token>();

    }

    public List<Token> Analisar()
    {
       
        while(_posicaoAtual < _codigoFonte.Length)
        {
            while (_posicaoAtual < _codigoFonte.Length && char.IsWhiteSpace(_codigoFonte[_posicaoAtual]))
            {
                _posicaoAtual++;
            }

            if (_posicaoAtual >= _codigoFonte.Length)
            {
                break;
            }

            var token = ProximoToken();

            if(token != null) 
            {
            _tokens.Add(token);
            }
        }

        return _tokens;

    }

    private Token? ProximoToken()
    {
        while (Char.IsWhiteSpace(_codigoFonte[_posicaoAtual]))
        {
            _posicaoAtual++;
        }

        if (Char.IsLetter(_codigoFonte[_posicaoAtual]))
        {
            return new Token(Token.TipoToken.Identificador, LerIdentificador());
        }
        else if (Char.IsDigit(_codigoFonte[_posicaoAtual]))
        {
            return new Token(Token.TipoToken.Numero, LerNumero());
        }
        else if (_codigoFonte[_posicaoAtual] == '+')
        {
            _posicaoAtual++;
            return new Token(Token.TipoToken.OperadorSoma, String.Empty);
        }
        else if (_codigoFonte[_posicaoAtual] == '-')
        {
            _posicaoAtual++;
            return new Token(Token.TipoToken.OperadorSubtracao, String.Empty);
        }
        else if (_codigoFonte[_posicaoAtual] == '*')
        {
            _posicaoAtual++;
            return new Token(Token.TipoToken.OperadorMultiplicacao, String.Empty);  
        }
        else if (_codigoFonte[_posicaoAtual] == '/')
        {
            _posicaoAtual++;
            return new Token(Token.TipoToken.OperadorDivisao, String.Empty);
        }
        else
        {
            throw new Exception("Erro Léxico na posição " + _posicaoAtual);
        }

    }

    private string LerNumero()
    {
        var numero = "";

        

        while (char.IsDigit(_codigoFonte[_posicaoAtual]))
        {

            numero += _codigoFonte[_posicaoAtual];

            _posicaoAtual++;

            if (EstaNoFinal())
            {
                break;
            }
        }

        return numero;  
    }

    private string LerIdentificador()
    {
        var nome = "";
        
        while (char.IsLetterOrDigit(_codigoFonte[_posicaoAtual]))
        {
            nome += _codigoFonte[_posicaoAtual];

            _posicaoAtual++;
        }

        return nome;
    }

    private bool EstaNoFinal() => (_posicaoAtual >= (_codigoFonte.Length - 1));

}
