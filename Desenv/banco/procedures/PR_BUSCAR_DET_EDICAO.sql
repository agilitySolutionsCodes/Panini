/****** Object:  StoredProcedure [dbo].[PR_BUSCAR_DET_EDICAO]    Script Date: 07/06/2012 10:27:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  07/02/2012      
-- Descrição:  Consultar dados de edição   
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================  

ALTER PROCEDURE [dbo].[PR_BUSCAR_DET_EDICAO]      
(     
 @P_CODIGO INTEGER,
 @P_EDICAO VARCHAR(7)
)      
AS      
BEGIN      
SET NOCOUNT ON    

	SELECT PC.descricao_pdfs, PC.cod_pdfs, PC.fase, PC.distribuicao, PC.divisao,
	PI.preco, PI.formato, PI.qtde_paginas, PI.periodicidade, PI.mes, PI.binding,
	PI.aprovacao_edit, PI.aprovacao_mkt, PI.cod_data 
	FROM PLAN_CABECALHO PC (NOLOCK)
	INNER JOIN PLAN_ITENS PI (NOLOCK)
		ON PI.cod_plan = PC.codigo
	WHERE PI.edicao = @P_EDICAO
	AND PC.codigo = @P_CODIGO
	
     
SET NOCOUNT OFF 
END 

GO


