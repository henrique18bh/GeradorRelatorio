CREATE DATABASE BaseExemplo
GO

USE BaseExemplo
GO

CREATE TABLE dbo.HistorioRelatorios(
	Id  int Identity,
	Nome varchar(30) NOT NULL,
	DataGeracao datetime NOT NULL,
	ConsultaRealizada varchar(2000) NOT NULL,
	CONSTRAINT PK_HistorioRelatorios PRIMARY KEY (Id)
)
GO


INSERT INTO dbo.Cotacoes
           (Sigla
           ,NomeMoeda
           ,UltimaCotacao
           ,Valor)
     VALUES
           ('USD'
           ,'Dólar norte-americano'
           ,'2017-05-26 16:59'
           ,3.2647)

INSERT INTO dbo.Cotacoes
           (Sigla
           ,NomeMoeda
           ,UltimaCotacao
           ,Valor)
     VALUES
           ('EUR'
           ,'Euro'
           ,'2017-05-26 16:59'
           ,3.6451)

INSERT INTO dbo.Cotacoes
           (Sigla
           ,NomeMoeda
           ,UltimaCotacao
           ,Valor)
     VALUES
           ('LIB'
           ,'Libra esterlina'
           ,'2017-05-26 16:59'
           ,4.1709)