using Interpretador.fonte.Analisador;
using Interpretador.fonte.Interpretador;

namespace Interpretador.fonte;

public class Program
{
    static void Main(string[] args)
    {
        string codigoFonte = @"
                LET x = 10
                LET y = x + 2 * 3
                PRINT ""A soma de x e y �: ""
                PRINT y + x
            ";

        Parser parser = new Parser(codigoFonte);

        List<Instrucao> instrucoes = parser.ParseCodigoBasic();

        Interpreta interpretador = new Interpreta(instrucoes);

        interpretador.Executar();

        Console.ReadLine();
        
    }

}
