

-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  28/02/2012      
-- Descrição:  Consultar dados de planejamento cadastrado  
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================  

ALTER PROCEDURE [dbo].[PR_LISTAR_PLANEJAMENTO]      
(     
 @P_CODPLAN INTEGER,
 @P_EDICAO VARCHAR(7)
)      
AS      
BEGIN      
SET NOCOUNT ON 

	SELECT 
	PC.cod_pdfs, 
	PC.descricao_pdfs, 
	PC.licenciante, 
	PC.divisao, 
	PC.fase, 
	PC.distribuicao, 
	PC.tipo, 
	PC.ano, 
	PC.cod_panini,
	PC.cod_fornecedor, 
	CONVERT(DATETIME,PC.data_lcto) data_lcto, 
	PC.cod_pdfs_album, 
	PC.cod_pdfs_envelope, 
	PC.cod_panini_album, 
	PC.cod_panini_envelope,
	PI.preco, 
	ISNULL(PI.custo,0) custo, 
	PI.periodicidade, 
	PI.binding, PI.capa, 
	PI.miolo, 
	PI.qtde_paginas, 
	PI.formato, 
	PI.colecao, 
	ISNULL(PI.assinaturas,0) assinaturas,
	PI.qtde_capa, 
	PI.qtde_miolo, 
	PI.imagem, 
	PI.acabamento, 
	PI.brinde, 
	PI.existe_brinde, 
	PI.shrink, 
	PI.manuseio,
	ISNULL(PI.preco_pagina,0) preco_pagina, 
	PCO.editorial, 
	PCO.env_mercosul, 
	PCO.formato_cromo, 
	PCO.formato_envelope, 
	PCO.li_capa_cortesia, 
	PCO.li_capa_venda,
	PCO.li_encarte_col, 
	PCO.li_encarte_esp, 
	PCO.li_formato, 
	PCO.li_papel_miolo, 
	PCO.li_poster, 
	PCO.li_qtde_pag,
	PCO.pais, 
	PCO.qtde_cromo_especial, 
	PCO.qtde_cromo_normal, 
	PCO.total_cromos, 
	PCO.preco_cromo, 
	PCO.preco_envelope, 
	PCO.cromo_por_env, 
	PCO.env_por_pacote, 
	PCO.env_por_caixa, 
	PCO.qtde_album_pacote
	FROM PLAN_CABECALHO PC (NOLOCK)
	INNER JOIN PLAN_ITENS PI (NOLOCK)
	ON PI.cod_plan = PC.codigo
	LEFT JOIN PLAN_COLECIONAVEIS PCO (NOLOCK)
	ON PCO.cod_plan = PI.cod_plan
	WHERE PC.codigo = @P_CODPLAN AND PI.edicao = @P_EDICAO
     
SET NOCOUNT OFF 
END 




GO


