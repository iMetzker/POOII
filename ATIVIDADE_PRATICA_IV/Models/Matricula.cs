using PlataformaCursos.Pagamentos;

namespace PlataformaCursos.Models;

public class Matricula
{
    public int Id { get; private set; }

    public int AlunoId { get; private set; }
    public Aluno Aluno { get; private set; } = null!;

    public int CursoId { get; private set; }
    public Curso Curso { get; private set; } = null!;

    public DateTime DataMatricula { get; private set; }

    public int? PagamentoId { get; private set; }
    public Pagamento? Pagamento { get; private set; }

    private readonly List<ProgressoAula> _progressos = new();
    public IReadOnlyList<ProgressoAula> Progressos => _progressos.AsReadOnly();

    private Matricula() { }

    public Matricula(Aluno aluno, Curso curso, Pagamento? pagamento = null)
    {
        ArgumentNullException.ThrowIfNull(aluno);
        ArgumentNullException.ThrowIfNull(curso);

        if (curso.EhPago)
        {
            if (pagamento is null || !pagamento.Aprovado)
                throw new InvalidOperationException(
                    $"O curso '{curso.Titulo}' é pago e requer pagamento aprovado para a matrícula.");
        }

        Aluno = aluno;
        Curso = curso;
        Pagamento = pagamento;
        DataMatricula = DateTime.Now;
    }

    public void ConcluirAula(Aula aula)
    {
        ArgumentNullException.ThrowIfNull(aula);

        if (!Curso.Aulas.Contains(aula))
            throw new InvalidOperationException("Esta aula não pertence ao curso da matrícula.");

        if (_progressos.All(p => p.AulaId != aula.Id))
            _progressos.Add(new ProgressoAula(aula));
    }

    public int AulasConcluidas => _progressos.Count;

    public double Progresso
        => Curso.TotalAulas == 0 ? 0 : (double)_progressos.Count / Curso.TotalAulas * 100;

    public bool Concluido => Curso.TotalAulas > 0 && _progressos.Count == Curso.TotalAulas;

    public override string ToString()
        => $"Matrícula [{Id}] {Aluno.Nome} → {Curso.Titulo} | "
           + $"Progresso: {Progresso:F0}% ({AulasConcluidas}/{Curso.TotalAulas})"
           + (Concluido ? " ✔ CONCLUÍDO" : "");
}
