namespace PlataformaCursos.Models;

public class ProgressoAula
{
    public int Id { get; private set; }

    public int MatriculaId { get; private set; }

    public int AulaId { get; private set; }
    public Aula? Aula { get; private set; }

    public DateTime ConcluidaEm { get; private set; }

    private ProgressoAula() { }

    public ProgressoAula(Aula aula)
    {
        ArgumentNullException.ThrowIfNull(aula);
        Aula = aula;
        AulaId = aula.Id;
        ConcluidaEm = DateTime.Now;
    }
}
