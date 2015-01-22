/****** Object:  StoredProcedure [dbo].[PR_GERAR_RELATORIO]    Script Date: 07/06/2012 10:31:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  09/02/2012      
-- Descrição:  Gera relatorio do planejamento  
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================  
    
ALTER PROCEDURE [dbo].[PR_GERAR_RELATORIO]      
(     
 @P_ANO INTEGER = NULL,
 @P_DIVISAO VARCHAR(30) = NULL,
 @P_TIPO VARCHAR(1) = NULL,
 @P_CODPLAN INT = NULL
)      
AS      
BEGIN      
SET NOCOUNT ON    

	SELECT codigo, ano, cod_pdfs, descricao_pdfs, fase, distribuicao, edicao_jan, edicao_fev, edicao_mar, edicao_abr,
	edicao_mai, edicao_jun, edicao_jul, edicao_ago, edicao_set, edicao_out, edicao_nov, edicao_dez,
	CASE aprovado_jan WHEN 0 THEN 'redBackground' ELSE '' END  aprovado_jan, 
	CASE aprovado_fev WHEN 0 THEN 'redBackground' ELSE '' END  aprovado_fev, 
	CASE aprovado_mar WHEN 0 THEN 'redBackground' ELSE '' END  aprovado_mar, 
	CASE aprovado_abr WHEN 0 THEN 'redBackground' ELSE '' END  aprovado_abr, 
	CASE aprovado_mai WHEN 0 THEN 'redBackground' ELSE '' END  aprovado_mai, 
	CASE aprovado_jun WHEN 0 THEN 'redBackground' ELSE '' END  aprovado_jun, 
	CASE aprovado_jul WHEN 0 THEN 'redBackground' ELSE '' END  aprovado_jul, 
	CASE aprovado_ago WHEN 0 THEN 'redBackground' ELSE '' END  aprovado_ago, 
	CASE aprovado_set WHEN 0 THEN 'redBackground' ELSE '' END  aprovado_set, 
	CASE aprovado_out WHEN 0 THEN 'redBackground' ELSE '' END  aprovado_out, 
	CASE aprovado_nov WHEN 0 THEN 'redBackground' ELSE '' END  aprovado_nov, 
	CASE aprovado_dez WHEN 0 THEN 'redBackground' ELSE '' END  aprovado_dez
	FROM PLAN_CABECALHO(NOLOCK) 
	WHERE (ano = @P_ANO  OR @P_ANO IS NULL) 
	AND (divisao = @P_DIVISAO  OR @P_DIVISAO IS NULL) 
	AND (tipo = @P_TIPO  OR @P_TIPO IS NULL)
	AND (codigo = @P_CODPLAN  OR @P_CODPLAN IS NULL)
	ORDER BY descricao_pdfs
	
     
SET NOCOUNT OFF 
END 

GO


