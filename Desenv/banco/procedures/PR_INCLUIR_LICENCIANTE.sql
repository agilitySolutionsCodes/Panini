
/****** Object:  StoredProcedure [dbo].[PR_INCLUIR_LICENCIANTE]    Script Date: 07/06/2012 10:35:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Yule Souza. - Agility Solutions      
-- Data Criacao:  29/05/2012      
-- Descrição:  Inclusao / Alteração de Licenciantes
-- =========================================================================      

ALTER PROCEDURE [dbo].[PR_INCLUIR_LICENCIANTE]      
(  
@P_CODIGO INT = NULL,     
@P_NOME VARCHAR(40)
)      
AS      
BEGIN      
SET NOCOUNT ON 

	DECLARE @errornumber int,@errormessage varchar(max)   

	BEGIN TRY
	
		IF(SELECT COUNT(*) FROM LICENCIANTES (NOLOCK) WHERE codigo = @P_CODIGO) = 0 BEGIN
			INSERT INTO LICENCIANTES(nome) VALUES(@P_NOME)
		END
		ELSE BEGIN
			UPDATE LICENCIANTES SET nome = @P_NOME WHERE codigo = @P_CODIGO
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


