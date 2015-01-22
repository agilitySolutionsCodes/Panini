

-- =========================================================================      
-- Autor:   Yule Souza. - Agility Solutions      
-- Data Criacao:  07/02/2012      
-- Descrição:  Consultar dados de edição   
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================  

ALTER PROCEDURE [dbo].[PR_FILTRAR_PLAN_NOVO]      
(     
 @P_ANO VARCHAR(4) = NULL,
 @P_DIVISAO VARCHAR(30) = NULL,
 @P_TIPO VARCHAR(1) = NULL,
 @P_STATUS VARCHAR(1) = NULL
)      

AS      
BEGIN      
SET NOCOUNT ON 

		SELECT DISTINCT
		PC.codigo, 
		PC.descricao_pdfs as descricao,
		PC.tipo,
		PLI.aprovacao_edit 
		FROM PLAN_CABECALHO PC (NOLOCK)
		INNER JOIN PLAN_ITENS PLI 
		
		ON  (PC.codigo = PLI.cod_plan)
		AND (PC.ano = @P_ANO or @P_ANO IS NULL)
		AND (PC.divisao = @P_DIVISAO  OR @P_DIVISAO IS NULL)
		AND (PLI.aprovacao_edit = @P_STATUS OR @P_STATUS IS NULL OR (@P_STATUS = 0 AND PLI.aprovacao_edit <> 1))
		AND (PC.tipo = @P_TIPO  OR @P_TIPO IS NULL)
		AND PLI.aprovacao_mkt = 1
		ORDER BY PC.codigo
     
SET NOCOUNT OFF 
END 



GO


