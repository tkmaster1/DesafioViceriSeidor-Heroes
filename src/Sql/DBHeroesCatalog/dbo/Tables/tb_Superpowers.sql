/* 
=================================================================================================================
Tabela: Superpoderes (tb_Superpowers)
Descrição: Armazena os superpoderes disponíveis para associação aos heróis.
Campos:
    - Id           (INT)           → Identificador único do superpoder (chave primária, identity)
    - Description  (VARCHAR(250))  → Descrição do superpoder
    - DateCreate   (DATETIME)      → Data de criação do registro
    - DateChange   (DATETIME)      → Data da última alteração    
    - Status       (BIT)           → Indica se o superpoder está Ativo (1) ou Inativo (0)
=================================================================================================================
*/

CREATE TABLE [dbo].[tb_Superpowers]
(
    [Id]             [INT] IDENTITY(1,1) NOT NULL,
    [Description]    [VARCHAR](250)      NOT NULL,
    [DateCreate]     [DATETIME]          NOT NULL,
	[DateChange]     [DATETIME]          NULL,	
	[Status]         [BIT]               NOT NULL,
    CONSTRAINT [PK_tb_Superpowers] 
        PRIMARY KEY CLUSTERED ([Id] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
              IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
              ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
) ON [PRIMARY]
GO

