/****** Object:  StoredProcedure [dbo].[PR_INCLUIR_FORNECEDOR]    Script Date: 07/06/2012 10:35:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  13/04/2012      
-- Descrição:  Inclusao / alteracao de fornecedores
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================      

ALTER PROCEDURE [dbo].[PR_INCLUIR_FORNECEDOR]      
(  
@P_CODIGO INT = NULL,     
@P_NOME VARCHAR(40),
@P_EMAIL VARCHAR (40)
)      
AS      
BEGIN      
SET NOCOUNT ON 

	DECLARE @errornumber int,@errormessage varchar(max)   

	BEGIN TRY
	
		IF(SELECT COUNT(*) FROM FORNECEDORES (NOLOCK) WHERE codigo = @P_CODIGO) = 0 BEGIN
			INSERT INTO FORNECEDORES(nome, email) VALUES(@P_NOME, @P_EMAIL)
		END
		ELSE BEGIN
			UPDATE FORNECEDORES SET nome = @P_NOME, email = @P_EMAIL WHERE codigo = @P_CODIGO
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


