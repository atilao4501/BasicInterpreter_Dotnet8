namespace Interpretador.fonte.Interpretador
{

    public abstract class Instrucao
    {
        public abstract void Executar();
    }

    public class Atribuicao : Instrucao
    {

        private Variavel _variavel;

        private List<Variavel> _variaveis;

        public Atribuicao(Variavel variavel, List<Variavel> variaveis)
        {
            _variavel = variavel;
            _variaveis = variaveis;
        }

        public override void Executar()
        {
            _variaveis.Add(_variavel);
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