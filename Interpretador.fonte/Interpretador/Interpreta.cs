using Interpretador.fonte.Analisador;

namespace Interpretador.fonte.Interpretador
{
    public class Interpreta
    {
        private List<Token> _tokens;
        private Parser _parser;

        public Interpreta(List<Token> tokens)
        {
            _tokens = tokens;
            _parser = new Parser(_tokens);
        }

        public void Executar()
        {

            int resultado = _parser.ParseExpressao();
            Console.WriteLine(resultado);

        }
    }
}


