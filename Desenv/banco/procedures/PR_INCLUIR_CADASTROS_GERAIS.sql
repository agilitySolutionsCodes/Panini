/****** Object:  StoredProcedure [dbo].[PR_INCLUIR_CADASTROS_GERAIS]    Script Date: 07/06/2012 10:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  13/04/2012      
-- Descrição:  Inclusao / alteracao de cadastros gerais
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================      

ALTER PROCEDURE [dbo].[PR_INCLUIR_CADASTROS_GERAIS]      
(  
@P_CODIGO INT = NULL,     
@P_DESCRICAO VARCHAR(250),
@P_TIPO INTEGER
)      
AS      
BEGIN      
SET NOCOUNT ON 

	DECLARE @errornumber int,@errormessage varchar(max)
	DECLARE @DESCRICAO_TIPO AS VARCHAR(20)   

	BEGIN TRY
	
		--CAMPO INSERIDO NA TABELA COMO INFORMATIVO, PARA QUE OS CODIGOS NAO SE PERDESSEM
		IF @P_TIPO = 1 BEGIN
			SET @DESCRICAO_TIPO = 'ACABAMENTO'
		END
		IF @P_TIPO = 2 BEGIN
			SET @DESCRICAO_TIPO = 'CAPA'
		END
		IF @P_TIPO = 3 BEGIN
			SET @DESCRICAO_TIPO = 'MIOLO'
		END
		IF @P_TIPO = 4 BEGIN
			SET @DESCRICAO_TIPO = 'TIPOS DE SERVIÇO'
		END
	
		IF(SELECT COUNT(*) FROM CADASTROS_GERAIS (NOLOCK) WHERE codigo = @P_CODIGO AND tipo = @P_TIPO) = 0 BEGIN
			INSERT INTO CADASTROS_GERAIS(descricao, tipo, descricao_tipo) VALUES(@P_DESCRICAO, @P_TIPO, @DESCRICAO_TIPO)
		END
		ELSE BEGIN
			UPDATE CADASTROS_GERAIS SET descricao = @P_DESCRICAO WHERE codigo = @P_CODIGO AND tipo = @P_TIPO
		END
		
	END TRY
	
	BEGIN CATCH
		SELECT
			 @errornumber = ERROR_NUMBER()
			,@errormessage = ERROR_MESSAGE()

		RAISERROR(@errornumber, 16, 1, @errormessage)
	END CATCH
  
END      
SET NOCOUNT OFF 

GO


