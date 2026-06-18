namespace PlataformaCursos.Interfaces;

public interface IPagamento
{
    bool Processar(decimal valor);

    string GerarComprovante();
}
