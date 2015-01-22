/****** Object:  StoredProcedure [dbo].[PR_DELETA_LICENCIANTE]    Script Date: 07/06/2012 10:28:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Yule Souza. - Agility Solutions      
-- Data Criacao:  31/05/2012     
-- Descrição:  Verifica se Licenciante Existe na tabela PDFS para exclusão do mesmo
-- Número	   Data		    Usuário      Descrição
-- #001#	   31/05/2012	Yule Souza	 Primeira Versão
-- =========================================================================     
ALTER PROCEDURE [dbo].[PR_DELETA_LICENCIANTE]  
(
  @P_CodLicenciante INT,
  @Retorno BIT = NULL OUTPUT,
  @MsgRetorno VARCHAR(60) = NULL OUTPUT
)

AS
BEGIN
	SET NOCOUNT ON
	
	SET @Retorno = 'False'
	SET @MsgRetorno = ''

	DELETE FROM PDFS WHERE codigo = @P_CodLicenciante
	
	IF(SELECT COUNT(*) FROM PDFS WHERE cod_licenciante = @P_CodLicenciante) = 0 
		BEGIN
			DELETE FROM LICENCIANTES WHERE @P_CodLicenciante = codigo
			SET @Retorno = 'True'
		END
	ELSE BEGIN
		SET @Retorno = 'False'
		SET @MsgRetorno = 'Não foi possível excluir o licenciante na tabela PDFS'
	END
	
END
SET NOCOUNT OFF


GO


