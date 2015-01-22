/****** Object:  StoredProcedure [dbo].[PR_LISTA_TIPO]    Script Date: 07/06/2012 10:37:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =========================================================================      
-- Autor:   Yule Souza. - Agility Solutions      
-- Data Criacao:  31/05/2012     
-- Descri��o:  Lista Tipo de Publica��es Campo Canal Tabela PDFS
-- N�mero	   Data		    Usu�rio      Descri��o
-- #001#	   28/06/2012	Yule Souza	 Primeira Vers�o
-- =========================================================================     
ALTER PROCEDURE [dbo].[PR_LISTA_TIPO]  
(
  @P_Ano VARCHAR(4),
  @P_Divisao VARCHAR(30) = NULL
)
AS
BEGIN

	SET NOCOUNT ON;

    SELECT DISTINCT canal FROM PDFS
	WHERE 
	ano = @P_Ano AND 
	divisao = @P_Divisao OR @P_Divisao IS NULL
END


GO


