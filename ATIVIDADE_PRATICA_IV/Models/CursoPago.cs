namespace PlataformaCursos.Models;

public class CursoPago : Curso
{
    public decimal Preco { get; private set; }

    private CursoPago() { }

    public CursoPago(string titulo, decimal valor) : base(titulo)
    {
        if (valor <= 0)
            throw new ArgumentException("Um curso pago deve ter valor maior que zero.", nameof(valor));
        Preco = valor;
    }

    public override bool EhPago => true;
    public override decimal Valor => Preco;
    public override string TipoCurso => "Pago";
}
