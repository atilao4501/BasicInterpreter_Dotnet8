namespace Interpretador.fonte.Interpretador;

public class Instrucao
{
    public TipoInstrucao Tipo { get; set; }

    public Instrucao(TipoInstrucao tipo)
    {
        Tipo = tipo;
    }
    public enum TipoInstrucao
    {
        Atribuicao,
        ExpressaoAritmetica,
        ComandoIf
    }
}