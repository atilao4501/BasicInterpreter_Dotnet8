using Interpretador.fonte.Interpretador;
using System.Text.RegularExpressions;

namespace Interpretador.fonte.Analisador;

public class Parser
{
    private readonly List<Variavel> _variaveis = new List<Variavel>();
    private readonly List<Token> _tokens;
    private int _posicaoAtual;
    private string _codigoFonte;


    public Parser(string codigoFonte)
    {       
        _posicaoAtual = 0;
        _codigoFonte = codigoFonte;
        Lexer lexer = new Lexer(_codigoFonte);
        _tokens = lexer.Analisar();
    }

    public List<Instrucao> ParseCodigoBasic()
    {

        List<Instrucao> instrucoes = new List<Instrucao>();

        for (int i = 0; i < _tokens.Count; i++)
        {
            Token tokenAtual = _tokens[i];

            if (tokenAtual.Tipo == Token.TipoToken.LET)
            {
                if (i + 1 < _tokens.Count && _tokens[i + 1].Tipo == Token.TipoToken.Identificador)
                {
                    string nomeVariavel = Identificador(_tokens[i + 1].Valor);
                    i += 2;

                    if (i < _tokens.Count && _tokens[i].Tipo == Token.TipoToken.OperadorAtribuicao)
                    {
                        i++;

                        object valor = ObterValorAtribuido(_tokens[i]);

                        Variavel variavel = new(nomeVariavel, valor);

                        Atribuicao atribuicao = new(variavel, valor);

                        _variaveis.Add(variavel);

                        instrucoes.Add(atribuicao);
                    }
                    else
                    {
                        throw new Exception("Erro de sintaxe: esperava um operador de atribuição");
                    }

                }
                else
                {
                    throw new Exception("Erro de sintaxe: esperava um identificador após a instrução LET.");
                }

            }
            else if (tokenAtual.Tipo == Token.TipoToken.PRINT)
            {
                if (i + 1 < _tokens.Count && _tokens[i + 1].Tipo == Token.TipoToken.String)
                {
                    string mensagem = _tokens[i + 1].Valor;
                    i++;

                    Instrucao print = new Print(mensagem);
                    instrucoes.Add(print);
                }
                else if(i + 1 < _tokens.Count && _tokens[i + 1].Tipo == Token.TipoToken.Identificador)
                {
                    var variavel = ObterVariavel(_tokens[i + 1].Valor);

                    i++;

                    Instrucao print = new Print(variavel.Valor.ToString());
                    instrucoes.Add(print);
                }

                else
                {
                    throw new Exception("Erro de sintaxe: esperava uma string após a instrução PRINT.");

                }

            }
            else if (tokenAtual.Tipo == Token.TipoToken.OperadorSoma || tokenAtual.Tipo == Token.TipoToken.OperadorSubtracao || tokenAtual.Tipo == Token.TipoToken.OperadorMultiplicacao || tokenAtual.Tipo == Token.TipoToken.OperadorDivisao)
            {
                if (i - 1 >= 0 && i + 1 < _tokens.Count)
                {

                    var teste = _tokens[i - 1];
                    int resultado = ParseExpressao(i - 1);
                    i += 2;

                    string nomeVariavel = _tokens[i - 2].Valor;

                    Variavel variavel = new Variavel(nomeVariavel, resultado);

                    //instrucoes.Add(new Atribuicao(variavel, resultado));
                }
            }          

        }

        return instrucoes;

    }

    public int ParseExpressao(int indiceInicio)
    {
        _posicaoAtual = indiceInicio;
        return ParseSomaSubtracao();
    }

    private Token Atual()
    {
        if(_posicaoAtual < _tokens.Count)
        {
            return _tokens[_posicaoAtual];
        }
        throw new IndexOutOfRangeException("Não há mais tokens disponíveis");
    }

    private Token Anterior()
    {
        if (_posicaoAtual - 1 >= 0)
        {
            return _tokens[_posicaoAtual - 1];
        }
        throw new IndexOutOfRangeException("Não há token anterior disponível.");
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
        }if (Token.TipoToken.Identificador == _tokens[_posicaoAtual].Tipo)
        {
            var num = ObterVariavel(_tokens[_posicaoAtual].Valor);

            //var numero = ObterValorAtribuido(_tokens[_posicaoAtual]);

            return int.Parse(num.Valor.ToString());
        }      

        throw new Exception("Erro de sintaxe: esperava um número");
    }


    private bool Corresponde(Token.TipoToken tipo)
    {
        if (!EstaNoFinal() && _tokens[_posicaoAtual].Tipo == tipo)
        {
            _posicaoAtual++;
            return true;
        }
        return false;
    }

    private bool EstaNoFinal()
    {
        return (_posicaoAtual >= _tokens.Count);
    }

    public string Identificador(string valorToken)
    {
        string identificadorSemEspacos = valorToken.Trim();

        string identificadorEmMinusculas = identificadorSemEspacos.ToLower();

        if (!Regex.IsMatch(identificadorEmMinusculas, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
        {
            throw new Exception("Erro de sintaxe: identificador inválido");
        }

        return identificadorEmMinusculas;
    }

    public object ObterValorAtribuido(Token token)
    {
        if (token.Tipo == Token.TipoToken.Numero)
        {
            return int.Parse(token.Valor);
        }

        else if (token.Tipo == Token.TipoToken.Identificador)
        {
            return token.Valor;
        }

        else
        {
            throw new Exception("Tipo de token inválido para valor atribuiido");
        }
    }

    public Variavel ObterVariavel(string nomeVariavel)
    {
        foreach (var variavel in _variaveis)
        {
            if (variavel.Nome == nomeVariavel)
            {
                return variavel;
            }
        }

        throw new Exception("Variável não encontrada " + nomeVariavel);
    }

    public void PreencherVariaveis()
    {
        foreach(var token in _tokens)
        {
            if(token.Tipo == Token.TipoToken.LET)
            {
                _variaveis.Add(new Variavel(token.Valor,token.Valor));
            }
        }
    }
}