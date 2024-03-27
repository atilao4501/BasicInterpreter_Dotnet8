using Interpretador.fonte.Analisador;

namespace Interpretador.fonte.Interpretador
{
    public class Interpreta
    {
        private List<Instrucao> _instrucoes;


        public Interpreta(List<Instrucao> instrucaos)
        {
            _instrucoes = instrucaos;
        }

        public void Executar()
        {

            foreach(var instrucao in _instrucoes)
            {
                instrucao.Executar();
            }

        }
    }
}


