namespace Interpretador.fonte.Interpretador
{

    public abstract class Instrucao
    {
        public abstract void Executar();
    }

    public class Atribuicao : Instrucao
    {

        private Variavel _variavel;

        private int _valor;

        public Atribuicao(Variavel variavel, int valor)
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