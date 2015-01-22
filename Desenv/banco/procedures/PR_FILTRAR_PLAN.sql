

-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  07/02/2012      
-- Descrição:  Consultar dados de edição   
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================  

ALTER PROCEDURE [dbo].[PR_FILTRAR_PLAN]      
(     
 @P_DIVISAO VARCHAR(30) = NULL,
 @P_ANO VARCHAR(4) = NULL,
 @P_TIPO VARCHAR(1) = NULL
)      

AS      
BEGIN      
SET NOCOUNT ON 

	
		SELECT codigo, descricao_pdfs as descricao FROM PLAN_CABECALHO (NOLOCK) 
		WHERE (divisao = @P_DIVISAO  OR @P_DIVISAO IS NULL)
		AND (ano = @P_ANO  OR @P_ANO IS NULL)
		AND (tipo = @P_TIPO  OR @P_TIPO IS NULL)
		ORDER BY codigo
     
SET NOCOUNT OFF 
END 





GO


