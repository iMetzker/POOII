namespace PlataformaCursos.Models;

public class CursoGratuito : Curso
{
    private CursoGratuito() { }

    public CursoGratuito(string titulo) : base(titulo) { }

    public override bool EhPago => false;
    public override decimal Valor => 0m;
    public override string TipoCurso => "Gratuito";
}
