using Microsoft.EntityFrameworkCore;
using PlataformaCursos.Data;
using PlataformaCursos.Models;
using PlataformaCursos.Pagamentos;

namespace PlataformaCursos.Services;

public class Plataforma : IDisposable
{
    private readonly PlataformaContext _db;

    public Plataforma(PlataformaContext db) => _db = db;

    public Aluno CadastrarAluno(string nome, string email)
    {
        var aluno = new Aluno(nome, email);
        _db.Alunos.Add(aluno);
        _db.SaveChanges();
        return aluno;
    }

    public T CadastrarCurso<T>(T curso) where T : Curso
    {
        _db.Cursos.Add(curso);
        _db.SaveChanges();
        return curso;
    }

    public void AdicionarAula(Curso curso, Aula aula)
    {
        curso.AdicionarAula(aula);
        _db.SaveChanges();
    }

    public Matricula Matricular(Aluno aluno, Curso curso, Pagamento? pagamento = null)
    {
        if (aluno.Matriculas.Any(m => m.CursoId == curso.Id))
            throw new InvalidOperationException($"{aluno.Nome} já está matriculado em '{curso.Titulo}'.");

        var matricula = new Matricula(aluno, curso, pagamento);
        _db.Matriculas.Add(matricula);
        _db.SaveChanges();
        return matricula;
    }

    public void ConcluirAula(Matricula matricula, Aula aula)
    {
        matricula.ConcluirAula(aula);
        _db.SaveChanges();
    }

    public IReadOnlyList<Aluno> ListarAlunos()
        => _db.Alunos.Include(a => a.Matriculas).AsNoTracking().ToList();

    public IReadOnlyList<Curso> ListarCursos()
        => _db.Cursos.Include(c => c.Aulas).AsNoTracking().ToList();

    public IReadOnlyList<Matricula> ListarMatriculas()
        => _db.Matriculas
              .Include(m => m.Aluno)
              .Include(m => m.Curso).ThenInclude(c => c.Aulas)
              .Include(m => m.Progressos)
              .Include(m => m.Pagamento)
              .AsNoTracking()
              .ToList();

    public Aluno? BuscarAluno(int id)
        => _db.Alunos.Include(a => a.Matriculas).FirstOrDefault(a => a.Id == id);

    public Curso? BuscarCurso(int id)
        => _db.Cursos.Include(c => c.Aulas).FirstOrDefault(c => c.Id == id);

    public Matricula? BuscarMatricula(int id)
        => _db.Matriculas
              .Include(m => m.Aluno)
              .Include(m => m.Curso).ThenInclude(c => c.Aulas)
              .Include(m => m.Progressos)
              .Include(m => m.Pagamento)
              .FirstOrDefault(m => m.Id == id);

    public void Dispose() => _db.Dispose();
}
