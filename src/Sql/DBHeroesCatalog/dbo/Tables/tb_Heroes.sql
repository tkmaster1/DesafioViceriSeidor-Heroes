/* 
=================================================================================================================
Tabela: Heroes (tb_Heroes)
Descrição: Armazena os dados cadastrais dos super-heróis.
Campos:
    - Id               (INT)            → Identificador único do herói (chave primária, identity)
    - Name             (VARCHAR(120))   → Nome real do herói
    - HeroName         (VARCHAR(120))   → Nome do herói (único)
    - BirthDate        (DATETIME)       → Data de nascimento
    - Height           (FLOAT)          → Altura do herói
    - Weight           (FLOAT)          → Peso do herói
    - DateCreate       (DATETIME)       → Data de criação do registro
    - DateChange       (DATETIME)       → Data da última alteração    
    - Status           (BIT)            → Indica se o super-heróis está Ativo (1) ou Inativo (0)

=================================================================================================================
*/

CREATE TABLE [dbo].[tb_Heroes]
(
    [Id]             [INT] IDENTITY(1,1) NOT NULL,
    [Name]           [VARCHAR](120)      NOT NULL,
    [HeroName]       [VARCHAR](120)      NOT NULL,
    [BirthDate]      [DATETIME]          NOT NULL,
    [Height]         [FLOAT]             NOT NULL,
    [Weight]         [FLOAT]             NOT NULL,
    [DateCreate]     [DATETIME]          NOT NULL,
	[DateChange]     [DATETIME]          NULL,	
	[Status]         [BIT]               NOT NULL,
    CONSTRAINT [PK_tb_Heroes]
        PRIMARY KEY CLUSTERED ([Id] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
              IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
              ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
    CONSTRAINT [UQ_tb_Heroes_HeroName] UNIQUE ([HeroName])
) ON [PRIMARY]
GO
