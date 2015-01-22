/****** Object:  StoredProcedure [dbo].[PR_LISTAR_MERCADO]    Script Date: 07/06/2012 10:38:56 ******/
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
    
ALTER PROCEDURE [dbo].[PR_LISTAR_MERCADO]      
(     
 @P_CODIGO INTEGER,
 @P_EDICAO VARCHAR(7)
)      
AS      
BEGIN      
SET NOCOUNT ON 

	SELECT PC.cod_pdfs, PC.divisao, PC.ano, PC.tipo, PC.descricao_pdfs, PC.fase, PC.edicao_jan, PC.edicao_fev, PC.edicao_mar, PC.edicao_abr,
	CASE PC.distribuicao WHEN 'N' THEN 'Nacional' WHEN 'S' THEN 'Setorial' END  distribuicao, CONVERT(DATETIME,PC.data_lcto) data_lcto,
	PC.edicao_mai, PC.edicao_jun, PC.edicao_jul, PC.edicao_ago, PC.edicao_set, PC.edicao_out, PC.edicao_nov, PC.edicao_dez,
	PI.preco, PI.periodicidade, PI.binding, PI.qtde_paginas, PI.formato, PI.cod_data, PI.tipo_ocorrencia_mkt,
	PI.obs_ocorrencia_mkt, PI.tipo_ocorrencia_edit, PI.obs_ocorrencia_edit, PI.mes,
	CONVERT(DATETIME,M.dt_prevista) dt_prevista, 
	CONVERT(DATETIME,M.dt_real) dt_real, diferenca, M.tipo_data 
	FROM PLAN_CABECALHO PC (NOLOCK)
	INNER JOIN PLAN_ITENS PI (NOLOCK)
	ON PI.cod_plan = PC.codigo
	LEFT JOIN MERCADO M (NOLOCK)
	ON M.cod_plan = PI.cod_plan AND M.edicao = PI.edicao
	WHERE PC.codigo = @P_CODIGO
	AND PI.edicao = @P_EDICAO
     
SET NOCOUNT OFF 
END 

GO


