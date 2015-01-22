

-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  25/09/2012      
-- Descrição:  Consultar dados de edição   
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================  

CREATE PROCEDURE [dbo].[PR_FILTRAR_PDFS]      
(     
 @P_DIVISAO VARCHAR(30) = NULL,
 @P_ANO VARCHAR(4) = NULL,
 @P_CANAL VARCHAR(20) = NULL
)      

AS      
BEGIN      
SET NOCOUNT ON 
	
	SELECT distinct cod_pdfs, descricao FROM PDFS (NOLOCK) 
    WHERE (ano = @P_ANO  OR @P_ANO IS NULL)
    AND (divisao = @P_DIVISAO  OR @P_DIVISAO IS NULL)
    AND (canal = @P_CANAL OR @P_CANAL IS NULL)
    ORDER BY descricao
     
SET NOCOUNT OFF 
END 






