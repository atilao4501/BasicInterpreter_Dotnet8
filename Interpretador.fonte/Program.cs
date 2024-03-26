using Interpretador.fonte.Analisador;
using Interpretador.fonte.Interpretador;

namespace Interpretador.fonte;

public class Program
{
    static void Main(string[] args)
    {
        string codigoFonte = "10 * 7";

        Lexer lexer = new Lexer(codigoFonte);
        List<Token> tokens = lexer.Analisar();

        Interpreta interpretador = new Interpreta(tokens);
        interpretador.Executar();
        
    }
}