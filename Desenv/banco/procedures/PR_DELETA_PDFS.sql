/****** Object:  StoredProcedure [dbo].[PR_DELETA_PDFS]    Script Date: 07/06/2012 10:28:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Yule Souza. - Agility Solutions      
-- Data Criacao:  31/05/2012     
-- Descrição:  Exclusão de PDFS
-- Número	   Data		    Usuário      Descrição
-- #001#	   04/06/2012	Yule Souza	 Primeira Versão
-- =========================================================================     
ALTER PROCEDURE [dbo].[PR_DELETA_PDFS]  
(
  --@P_CodPdfs VARCHAR(20),
  @P_CodPdfs INT,
  @Retorno BIT = NULL OUTPUT,
  @MsgRetorno VARCHAR(90) = NULL OUTPUT
)

AS
BEGIN
	SET NOCOUNT ON
	
	SET @Retorno = 'False'
	SET @MsgRetorno = ''
	
	IF (SELECT COUNT(*) FROM PDFS WHERE @P_CodPdfs = codigo AND @P_CodPdfs IS NOT NULL) = 0
	BEGIN
		SET @Retorno = 'False'
		SET @MsgRetorno = 'Não foi possível excluir o Registro na tabela PDFS'
	END	
	ELSE BEGIN
		DELETE FROM PDFS WHERE @P_CodPdfs = codigo
		SET @Retorno = 'True'
	END
END
SET NOCOUNT OFF





GO


