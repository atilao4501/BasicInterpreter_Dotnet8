namespace Interpretador.fonte.Interpretador;

public class TabelaDeSimbolos
{
    private readonly Dictionary<string, Variavel> _variaveis = new Dictionary<string, Variavel>();

    public void AdicionarVariavel(string nome, Variavel valor)
    {
        if (_variaveis.ContainsKey(nome))
        {
            throw new Exception("ERRO: variável já existente");
        }

        _variaveis[nome] = valor;
    }

    public Variavel ObterVariavel(string nome)
    {
        if (!_variaveis.ContainsKey(nome))
        {
            throw new Exception("Variável não declarada" + nome);
            
        }
        return _variaveis[nome];
    }
}