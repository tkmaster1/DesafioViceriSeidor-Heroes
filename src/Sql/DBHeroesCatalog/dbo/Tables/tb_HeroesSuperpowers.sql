/* 
=================================================================================================================
Table: HeroesSuperpowers (tb_HeroesSuperpowers)
Description: Tabela de relacionamento N:N entre heróis e superpoderes.
Columns:
    - HeroId        (INT) → Identificador do herói (FK → tb_Heroes.Id)
    - SuperpowerId  (INT) → Identificador do superpoder (FK → tb_Superpowers.Id)
=================================================================================================================
*/

CREATE TABLE [dbo].[tb_HeroesSuperpowers]
(
    [HeroId]       [INT] NOT NULL,
    [SuperpowerId] [INT] NOT NULL,
    CONSTRAINT [PK_tb_HeroesSuperpowers]
        PRIMARY KEY CLUSTERED ([HeroId], [SuperpowerId])
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
              IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
              ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tb_HeroesSuperpowers] WITH CHECK
ADD CONSTRAINT [FK_tb_HeroesSuperpowers_tb_Heroes_HeroId]
FOREIGN KEY ([HeroId])
REFERENCES [dbo].[tb_Heroes] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[tb_HeroesSuperpowers]
CHECK CONSTRAINT [FK_tb_HeroesSuperpowers_tb_Heroes_HeroId]
GO

ALTER TABLE [dbo].[tb_HeroesSuperpowers] WITH CHECK
ADD CONSTRAINT [FK_tb_HeroesSuperpowers_tb_Superpowers_SuperpowerId]
FOREIGN KEY ([SuperpowerId])
REFERENCES [dbo].[tb_Superpowers] ([Id])
GO

ALTER TABLE [dbo].[tb_HeroesSuperpowers]
CHECK CONSTRAINT [FK_tb_HeroesSuperpowers_tb_Superpowers_SuperpowerId]
GO

