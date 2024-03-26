namespace Interpretador.fonte.Interpretador;

public class Variavel
{
    public string Nome { get; }
    public object Valor { get; set; }

    public Variavel(string nome, object valor)
    {
        Nome = nome;
        Valor = valor;
    }

}