using Microsoft.EntityFrameworkCore;
using PlataformaCursos.Models;
using PlataformaCursos.Pagamentos;

namespace PlataformaCursos.Data;

public class PlataformaContext : DbContext
{
    private static string ConnectionString =>
        Environment.GetEnvironmentVariable("CURSOS_CONNSTRING")
        ?? "Server=(localdb)\\MSSQLLocalDB;Database=PlataformaCursosDb;"
         + "Trusted_Connection=True;TrustServerCertificate=True;";

    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Aula> Aulas => Set<Aula>();
    public DbSet<Matricula> Matriculas => Set<Matricula>();
    public DbSet<ProgressoAula> Progressos => Set<ProgressoAula>();
    public DbSet<Pagamento> Pagamentos => Set<Pagamento>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(e =>
        {
            e.HasKey(a => a.Id);
            e.Property(a => a.Nome).IsRequired().HasMaxLength(150);
            e.Property(a => a.Email).IsRequired().HasMaxLength(150);
            e.HasMany(a => a.Matriculas)
                .WithOne(m => m.Aluno)
                .HasForeignKey(m => m.AlunoId);
        });

        modelBuilder.Entity<Curso>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Titulo).IsRequired().HasMaxLength(200);
            e.Ignore(c => c.EhPago);
            e.Ignore(c => c.Valor);
            e.Ignore(c => c.TipoCurso);
            e.Ignore(c => c.TotalAulas);
            e.HasDiscriminator<string>("TipoCursoDb")
                .HasValue<CursoGratuito>("Gratuito")
                .HasValue<CursoPago>("Pago");
            e.HasMany(c => c.Aulas)
                .WithOne(a => a.Curso)
                .HasForeignKey(a => a.CursoId);
        });
        modelBuilder.Entity<CursoPago>().Property(c => c.Preco).HasPrecision(18, 2);

        modelBuilder.Entity<Aula>(e =>
        {
            e.HasKey(a => a.Id);
            e.Property(a => a.Titulo).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Matricula>(e =>
        {
            e.HasKey(m => m.Id);
            e.Ignore(m => m.AulasConcluidas);
            e.Ignore(m => m.Progresso);
            e.Ignore(m => m.Concluido);
            e.HasMany(m => m.Progressos)
                .WithOne()
                .HasForeignKey(p => p.MatriculaId);
            e.HasOne(m => m.Pagamento)
                .WithOne()
                .HasForeignKey<Matricula>(m => m.PagamentoId);
        });

        modelBuilder.Entity<ProgressoAula>(e =>
        {
            e.HasKey(p => p.Id);
            e.HasOne(p => p.Aula)
                .WithMany()
                .HasForeignKey(p => p.AulaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Pagamento>(e =>
        {
            e.HasKey(p => p.Id);
            e.Ignore(p => p.Forma);
            e.Property(p => p.Valor).HasPrecision(18, 2);
            e.HasDiscriminator<string>("FormaPagamentoDb")
                .HasValue<PagamentoCartao>("Cartao")
                .HasValue<PagamentoPix>("Pix");
        });
        modelBuilder.Entity<PagamentoCartao>().Property(p => p.NumeroCartao).HasMaxLength(20);
        modelBuilder.Entity<PagamentoPix>().Property(p => p.ChavePix).HasMaxLength(150);
    }
}
