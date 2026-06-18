/* =====================================================================
   Importanteee: Meu código está criando o banco automaticamente na 1ª
   execução com Ensurecreated.
   Este script sql é um complemento para ter acesso ao banco manualmente. Se rodar este script ANTES de abrir
   o programa, a aplicação apenas usará o banco já existente, no problems.
   ===================================================================== */

IF DB_ID('PlataformaCursosDb') IS NULL
    CREATE DATABASE [PlataformaCursosDb];
GO

USE [PlataformaCursosDb];
GO

DROP TABLE IF EXISTS [Progressos];
DROP TABLE IF EXISTS [Matriculas];
DROP TABLE IF EXISTS [Aulas];
DROP TABLE IF EXISTS [Pagamentos];
DROP TABLE IF EXISTS [Cursos];
DROP TABLE IF EXISTS [Alunos];
GO

CREATE TABLE [Alunos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(150) NOT NULL,
    [Email] nvarchar(150) NOT NULL,
    CONSTRAINT [PK_Alunos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Cursos] (
    [Id] int NOT NULL IDENTITY,
    [Titulo] nvarchar(200) NOT NULL,
    [TipoCursoDb] nvarchar(8) NOT NULL,
    [Preco] decimal(18,2) NULL,
    CONSTRAINT [PK_Cursos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pagamentos] (
    [Id] int NOT NULL IDENTITY,
    [Valor] decimal(18,2) NOT NULL,
    [DataPagamento] datetime2 NULL,
    [Aprovado] bit NOT NULL,
    [FormaPagamentoDb] nvarchar(13) NOT NULL,
    [NumeroCartao] nvarchar(20) NULL,
    [Parcelas] int NULL,
    [ChavePix] nvarchar(150) NULL,
    [CodigoTransacao] nvarchar(max) NULL,
    CONSTRAINT [PK_Pagamentos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Aulas] (
    [Id] int NOT NULL IDENTITY,
    [Titulo] nvarchar(200) NOT NULL,
    [DuracaoMinutos] int NOT NULL,
    [CursoId] int NOT NULL,
    CONSTRAINT [PK_Aulas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Aulas_Cursos_CursoId] FOREIGN KEY ([CursoId]) REFERENCES [Cursos] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Matriculas] (
    [Id] int NOT NULL IDENTITY,
    [AlunoId] int NOT NULL,
    [CursoId] int NOT NULL,
    [DataMatricula] datetime2 NOT NULL,
    [PagamentoId] int NULL,
    CONSTRAINT [PK_Matriculas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Matriculas_Alunos_AlunoId] FOREIGN KEY ([AlunoId]) REFERENCES [Alunos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Matriculas_Cursos_CursoId] FOREIGN KEY ([CursoId]) REFERENCES [Cursos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Matriculas_Pagamentos_PagamentoId] FOREIGN KEY ([PagamentoId]) REFERENCES [Pagamentos] ([Id])
);
GO

CREATE TABLE [Progressos] (
    [Id] int NOT NULL IDENTITY,
    [MatriculaId] int NOT NULL,
    [AulaId] int NOT NULL,
    [ConcluidaEm] datetime2 NOT NULL,
    CONSTRAINT [PK_Progressos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Progressos_Aulas_AulaId] FOREIGN KEY ([AulaId]) REFERENCES [Aulas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Progressos_Matriculas_MatriculaId] FOREIGN KEY ([MatriculaId]) REFERENCES [Matriculas] ([Id]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_Aulas_CursoId] ON [Aulas] ([CursoId]);
GO
CREATE INDEX [IX_Matriculas_AlunoId] ON [Matriculas] ([AlunoId]);
GO
CREATE INDEX [IX_Matriculas_CursoId] ON [Matriculas] ([CursoId]);
GO
CREATE UNIQUE INDEX [IX_Matriculas_PagamentoId] ON [Matriculas] ([PagamentoId]) WHERE [PagamentoId] IS NOT NULL;
GO
CREATE INDEX [IX_Progressos_AulaId] ON [Progressos] ([AulaId]);
GO
CREATE INDEX [IX_Progressos_MatriculaId] ON [Progressos] ([MatriculaId]);
GO


INSERT INTO [Alunos] ([Nome], [Email]) VALUES
    (N'Ana Souza',  N'ana@email.com'),
    (N'Bruno Lima', N'bruno@email.com');
GO

INSERT INTO [Cursos] ([Titulo], [TipoCursoDb], [Preco]) VALUES
    (N'C# do Zero ao Avançado', N'Pago',     199.90),
    (N'Git e GitHub Essencial', N'Gratuito', NULL);
GO

INSERT INTO [Aulas] ([Titulo], [DuracaoMinutos], [CursoId]) VALUES
    (N'Introdução e ambiente', 20, (SELECT [Id] FROM [Cursos] WHERE [Titulo] = N'C# do Zero ao Avançado')),
    (N'Tipos e variáveis',     35, (SELECT [Id] FROM [Cursos] WHERE [Titulo] = N'C# do Zero ao Avançado')),
    (N'POO na prática',        50, (SELECT [Id] FROM [Cursos] WHERE [Titulo] = N'C# do Zero ao Avançado')),
    (N'Commits e branches',    25, (SELECT [Id] FROM [Cursos] WHERE [Titulo] = N'Git e GitHub Essencial')),
    (N'Pull requests',         30, (SELECT [Id] FROM [Cursos] WHERE [Titulo] = N'Git e GitHub Essencial'));
GO

SELECT * FROM [Alunos];
SELECT * FROM [Cursos];
SELECT * FROM [Aulas];
GO
