namespace Interpretador.fonte.Interpretador
{

    public abstract class Instrucao
    {
        private static List<Variavel> _variaveis = new List<Variavel>();

        public static List<Variavel> Variaveis =>  _variaveis;
        public void AdicionarVariavel(Variavel variavel)
        {
            _variaveis.Add(variavel);
        }
        
        public static Variavel ObterVariavel(string nomeVariavel)
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

        public static List<Variavel> GetVariaveis()
        {
            return _variaveis;
        }
        
        public abstract void Executar();
    }

    public class Atribuicao : Instrucao
    {

        private Variavel _variavel;

        private object _valor;

        public Atribuicao(Variavel variavel, object valor)
        {
            _variavel = variavel;
            _valor = valor;
        }

        public override void Executar()
        {
            _variavel.Valor = _valor;
        }
    }

    public class Print : Instrucao
    {
        private string _mensagem;

        public Print(string mensagem)
        {

            _mensagem = mensagem;
        }

        public override void Executar()
        {

            Console.WriteLine(_mensagem);
        }
    }

}