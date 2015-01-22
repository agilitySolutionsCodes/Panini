/****** Object:  StoredProcedure [dbo].[PR_INCLUIR_DATA]    Script Date: 07/06/2012 10:34:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  13/02/2012      
-- Descrição:  Inclusao / alteracao de datas  
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================      

ALTER PROCEDURE [dbo].[PR_INCLUIR_DATA]      
(  
@P_CODIGO INT = NULL,     
@P_LICENCIANTE VARCHAR(30),         
@P_NOME VARCHAR(50),
@P_DT_PROD_EDIT INT,
@P_DT_APROV_LICENC INT,
@P_DT_APROV_PROVAS INT,
@P_DT_PROD_GRAFICA INT,
@P_DT_ENTR_DISTRIB INT,
@P_DT_ENTR_ASS INT
)      
AS      
BEGIN      
SET NOCOUNT ON 

	DECLARE @errornumber int,@errormessage varchar(max)   

	BEGIN TRY
	
		IF(SELECT COUNT(*) FROM DATAS (NOLOCK) WHERE codigo = @P_CODIGO) = 0 BEGIN
			INSERT INTO DATAS(licenciante, nome, dt_producao, dt_aprov_licenc, dt_aprov_provas, dt_grafica, dt_entr_distr, dt_entr_ass)
			VALUES(@P_LICENCIANTE, @P_NOME, @P_DT_PROD_EDIT, @P_DT_APROV_LICENC, @P_DT_APROV_PROVAS, @P_DT_PROD_GRAFICA, @P_DT_ENTR_DISTRIB, @P_DT_ENTR_ASS)
		END
		ELSE BEGIN
			UPDATE DATAS SET dt_producao = @P_DT_PROD_EDIT,	dt_aprov_licenc = @P_DT_APROV_LICENC, 
			dt_aprov_provas = @P_DT_APROV_PROVAS, dt_grafica = @P_DT_PROD_GRAFICA, dt_entr_distr = @P_DT_ENTR_DISTRIB,  
			dt_entr_ass = @P_DT_ENTR_ASS
			WHERE codigo = @P_CODIGO
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


